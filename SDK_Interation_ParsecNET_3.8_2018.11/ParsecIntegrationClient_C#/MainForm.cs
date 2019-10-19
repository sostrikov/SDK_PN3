using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

using ParsecIntegrationClient.IntegrationWebService;

namespace ParsecIntegrationClient
{
    public class MainForm : Form
    {
        private Guid _showOrgUnitID = Guid.Empty;
        private ToolStripMenuItem tsRefreshList;
		private ToolStripMenuItem toolStripMenuItem1;
		private ToolStripMenuItem получитьСостояниеToolStripMenuItem;
        private string _initialFormText = string.Empty;

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            _initialFormText = Text;
            tsUserStatus.Text = ClientState.CurrentUser;
            RefreshOrgUnitsHierarhy();
            ShowOrgUnitList(ClientState.RootOrgUnitID);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Exit();
            base.OnClosing(e);
        }

        private void Exit()
        {
            try
            {
                IntegrationService integServ = new IntegrationService();
                integServ.CloseSession(ClientState.SessionID);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowError(ex);
            }
        }

        private void RefreshOrgUnitsHierarhy()
        {
            try
            {
                IntegrationService integServ = new IntegrationService();
                OrgUnit[] ouHierarhy = integServ.GetOrgUnitsHierarhy(ClientState.SessionID);
                ClientState.SetOrgUnitHierarhy(ouHierarhy);

            }
            catch (Exception ex)
            {
            }
        }

        private void ShowOrgUnitList(Guid orgUnitID)
        {
            try
            {
                if (Guid.Empty.Equals(orgUnitID) || orgUnitID.Equals(_showOrgUnitID))
                    return;
                RefreshList(orgUnitID);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowError(ex);
            }
        }

        private void RefreshList(Guid orgUnitID)
        {
            _showOrgUnitID = orgUnitID;
            IntegrationService integServ = new IntegrationService();
            BaseObject[] resList = integServ.GetOrgUnitSubItems(ClientState.SessionID, orgUnitID);
            try
            {
                Guid id = orgUnitID;
                List<string> orgUnitsList = new List<string>();
                orgUnitsList.Add(ClientState.GetOrgUnitName(id));
                while (!id.Equals(Guid.Empty) && !id.Equals(ClientState.GetParentOrgUnit(id)))
                {
                    id = ClientState.GetParentOrgUnit(id);
                    orgUnitsList.Add(ClientState.GetOrgUnitName(id));
                }
                orgUnitsList.Reverse();
                for (int i = orgUnitsList.Count - 1; i >= 0; i--)
                    if (orgUnitsList[i] == null)
                        orgUnitsList[i] = string.Empty;
                tsStatusString.Text = "Полный путь " + string.Join("\\", orgUnitsList.ToArray());
                lvPersons.BeginUpdate();
                lvPersons.Items.Clear();
                if (resList == null || resList.Length == 0)
                    return;
                for (int i = 0; i < resList.Length; i++)
                    InsertListItem(resList[i]);
                for (int i = lvPersons.Columns.Count - 1; i >= 0; i--)
                    lvPersons.Columns[i].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            finally
            {
                lvPersons.EndUpdate();
                Text = string.Format("{0} - в списке {1} элементов", _initialFormText ?? string.Empty, lvPersons.Items.Count);
            }
        }

        private void OnEnter(BaseObject item)
        {
            if (item == null)
                return;
            BasePerson personItem = item as BasePerson;
            if (personItem != null)
            {
                OnOpen(personItem);
                return;
            }
            BaseOrgUnit orgUnitItem = item as BaseOrgUnit;
            if (orgUnitItem == null)
                return;
            ShowOrgUnitList(orgUnitItem.ID);
        }

        private void OnBack()
        {
            ShowOrgUnitList(ClientState.GetParentOrgUnit(_showOrgUnitID));
        }

        private void OnDelete(BaseObject item)
        {
            if (item == null)
                return;
            BasePerson personItem = item as BasePerson;
            if (personItem != null)
            {
                if (CommonFunctions.AskYesNoQuestion(string.Format("Вы действительно хотите удалить сотрудника {0} {1} {2}?", personItem.LAST_NAME, personItem.FIRST_NAME, personItem.MIDDLE_NAME), "Удаление сотрудника") == DialogResult.Yes)
                {
                    try
                    {
                        IntegrationService integServ = new IntegrationService();
                        BaseResult res = integServ.DeletePerson(ClientState.SessionID, personItem.ID);
                        if (res.Result != ClientState.Result_Success)
                        {
                            CommonFunctions.ShowErrorMessage(res.ErrorMessage);
                            return;
                        }
                        RefreshOrgUnitsHierarhy();
                        RefreshList(_showOrgUnitID);
                    }
                    catch (Exception ex)
                    {
                        CommonFunctions.ShowError(ex);
                    }
                }
                return;
            }
            BaseOrgUnit orgUnitItem = item as BaseOrgUnit;
            if (orgUnitItem == null)
                return;
            if (CommonFunctions.AskYesNoQuestion(string.Format("Вы действительно хотите удалить подразделение {0}?", orgUnitItem.NAME), "Удаление подразделения") == DialogResult.Yes)
            {
                try
                {
                    IntegrationService integServ = new IntegrationService();
                    BaseResult res = integServ.DeleteOrgUnit(ClientState.SessionID, orgUnitItem.ID);
                    if (res.Result != ClientState.Result_Success)
                    {
                        CommonFunctions.ShowErrorMessage(res.ErrorMessage);
                        return;
                    }
                    RefreshOrgUnitsHierarhy();
                    RefreshList(_showOrgUnitID);
                }
                catch (Exception ex)
                {
                    CommonFunctions.ShowError(ex);
                }
            }
        }

        private void OnOpen(BaseObject item)
        {
            BasePerson personItem = item as BasePerson;
            if (personItem != null)
            {
                ShowPerson(personItem);
                return;
            }
            BaseOrgUnit orgUnitItem = item as BaseOrgUnit;
            if (orgUnitItem == null)
                return;
            ShowOrgUnit(orgUnitItem);
        }

        private void OnOrgUnitCreate()
        {
            try
            {
                using (OrgUnitForm frm = new OrgUnitForm())
                {
                    frm.CreateOrgUnit(_showOrgUnitID);
                    if (frm.ShowDialog(this) != DialogResult.OK)
                        return;
                    RefreshOrgUnitsHierarhy();
                    RefreshList(_showOrgUnitID);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void OnPersonCreate()
        {
            try
            {
                using (PersonForm frm = new PersonForm())
                {
                    frm.CreatePerson(_showOrgUnitID);
                    if (frm.ShowDialog(this) != DialogResult.OK)
                        return;
                    RefreshList(_showOrgUnitID);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ShowOrgUnit(BaseOrgUnit orgUnitItem)
        {
            try
            {
                using (OrgUnitForm frm = new OrgUnitForm())
                {
                    frm.SetOrgUnit(orgUnitItem.ID);
                    if (frm.ShowDialog(this) != DialogResult.OK)
                        return;
                }
                RefreshOrgUnitsHierarhy();
                RefreshList(_showOrgUnitID);
            }
            catch (Exception ex)
            {
            }
        }

        private void ShowPerson(BasePerson personItem)
        {
            try
            {
                using (PersonForm frm = new PersonForm())
                {
                    frm.SetPerson(personItem.ID);
                    if (frm.ShowDialog(this) != DialogResult.OK)
                        return;
                    RefreshList(_showOrgUnitID);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void ShowEventsForm()
        {
            try
            {
                using (EventsForm frm = new EventsForm())
                    frm.ShowDialog(this);
            }
            catch (Exception ex)
            {
            }
        }

        private void InsertListItem(BaseObject item)
        {
            if (item == null)
                return;
            ListViewItem lvItem = null;
            if (item is BaseOrgUnit)
            {
                BaseOrgUnit orgUnitItem = (BaseOrgUnit)item;
                lvItem = new ListViewItem();
                lvItem.Text = orgUnitItem.NAME ?? string.Empty;
                lvItem.SubItems.Add(orgUnitItem.DESC ?? string.Empty);
                lvItem.ImageIndex = 0;
            }
            else if (item is BasePerson)
            {
                BasePerson personItem = (BasePerson)item;
                lvItem = new ListViewItem();
                lvItem.Text = personItem.LAST_NAME ?? string.Empty;
                lvItem.SubItems.Add(personItem.FIRST_NAME ?? string.Empty);
                lvItem.SubItems.Add(personItem.MIDDLE_NAME ?? string.Empty);
                lvItem.SubItems.Add(personItem.TAB_NUM ?? string.Empty);
                lvItem.ImageIndex = 1;
            }
            if (lvItem == null)
                return;
            lvItem.Tag = item;
            lvPersons.Items.Add(lvItem);
        }

        private void tlChangeUser_Click(object sender, EventArgs e)
        {

        }

        private void tsExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tsRefreshList_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshOrgUnitsHierarhy();
                RefreshList(_showOrgUnitID);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowError(ex);
            }
        }

        private void tsEnter_Click(object sender, EventArgs e)
        {
            if (lvPersons.SelectedItems == null || lvPersons.SelectedItems.Count == 0)
                return;
            OnEnter(lvPersons.SelectedItems[lvPersons.SelectedItems.Count - 1].Tag as BaseObject);
        }

        private void tsBack_Click(object sender, EventArgs e)
        {
            OnBack();
        }

        private void tsOpenCard_Click(object sender, EventArgs e)
        {
            if (lvPersons.SelectedItems == null || lvPersons.SelectedItems.Count == 0)
                return;
            OnOpen(lvPersons.SelectedItems[lvPersons.SelectedItems.Count - 1].Tag as BaseObject);
        }

        private void tsCreateOrgUnit_Click(object sender, EventArgs e)
        {
            OnOrgUnitCreate();
        }

        private void tsCreatePerson_Click(object sender, EventArgs e)
        {
            OnPersonCreate();
        }

        private void tsDelete_Click(object sender, EventArgs e)
        {
            if (lvPersons.SelectedItems == null || lvPersons.SelectedItems.Count == 0)
                return;
            OnDelete(lvPersons.SelectedItems[lvPersons.SelectedItems.Count - 1].Tag as BaseObject);
        }

        private void tsEvents_Click(object sender, EventArgs e)
        {
            ShowEventsForm();
        }

        private void lvPersons_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem lvItem = lvPersons.GetItemAt(e.X, e.Y);
            if (lvItem == null)
                return;
            OnEnter(lvItem.Tag as BaseObject);
        }

        private void lvPersons_KeyUp(object sender, KeyEventArgs e)
        {
            BaseObject item = (lvPersons.SelectedItems.Count == 0) ? null : lvPersons.SelectedItems[lvPersons.SelectedItems.Count - 1].Tag as BaseObject;
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    OnEnter(item);
                    break;

                case Keys.Back:
                    OnBack();
                    break;

                case Keys.Delete:
                    OnDelete(item);
                    break;
            }
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.tlFilePopup = new System.Windows.Forms.ToolStripMenuItem();
			this.tlChangeUser = new System.Windows.Forms.ToolStripMenuItem();
			this.tsExit = new System.Windows.Forms.ToolStripMenuItem();
			this.tlEditPopup = new System.Windows.Forms.ToolStripMenuItem();
			this.tsRefreshList = new System.Windows.Forms.ToolStripMenuItem();
			this.tsEnter = new System.Windows.Forms.ToolStripMenuItem();
			this.tsBack = new System.Windows.Forms.ToolStripMenuItem();
			this.tsOpenCard = new System.Windows.Forms.ToolStripMenuItem();
			this.tsCreateOrgUnit = new System.Windows.Forms.ToolStripMenuItem();
			this.tsCreatePerson = new System.Windows.Forms.ToolStripMenuItem();
			this.tsDelete = new System.Windows.Forms.ToolStripMenuItem();
			this.tlEventsPopup = new System.Windows.Forms.ToolStripMenuItem();
			this.tsEvents = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.получитьСостояниеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stStatus = new System.Windows.Forms.StatusStrip();
			this.tsUserStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsStatusString = new System.Windows.Forms.ToolStripStatusLabel();
			this.lvPersons = new System.Windows.Forms.ListView();
			this.colLastName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colFirstName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colMiddleName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colTabNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ilLargeIcons = new System.Windows.Forms.ImageList(this.components);
			this.ilSmallIcons = new System.Windows.Forms.ImageList(this.components);
			this.menuStrip1.SuspendLayout();
			this.stStatus.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlFilePopup,
            this.tlEditPopup,
            this.tlEventsPopup,
            this.toolStripMenuItem1});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(659, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// tlFilePopup
			// 
			this.tlFilePopup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlChangeUser,
            this.tsExit});
			this.tlFilePopup.Name = "tlFilePopup";
			this.tlFilePopup.Size = new System.Drawing.Size(48, 20);
			this.tlFilePopup.Text = "Файл";
			// 
			// tlChangeUser
			// 
			this.tlChangeUser.Name = "tlChangeUser";
			this.tlChangeUser.Size = new System.Drawing.Size(188, 22);
			this.tlChangeUser.Text = "Смена пользователя";
			this.tlChangeUser.Click += new System.EventHandler(this.tlChangeUser_Click);
			// 
			// tsExit
			// 
			this.tsExit.Name = "tsExit";
			this.tsExit.Size = new System.Drawing.Size(188, 22);
			this.tsExit.Text = "Выход";
			this.tsExit.Click += new System.EventHandler(this.tsExit_Click);
			// 
			// tlEditPopup
			// 
			this.tlEditPopup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRefreshList,
            this.tsEnter,
            this.tsBack,
            this.tsOpenCard,
            this.tsCreateOrgUnit,
            this.tsCreatePerson,
            this.tsDelete});
			this.tlEditPopup.Name = "tlEditPopup";
			this.tlEditPopup.Size = new System.Drawing.Size(108, 20);
			this.tlEditPopup.Text = "Редактирование";
			// 
			// tsRefreshList
			// 
			this.tsRefreshList.Name = "tsRefreshList";
			this.tsRefreshList.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
			this.tsRefreshList.Size = new System.Drawing.Size(243, 22);
			this.tsRefreshList.Text = "Обновить список";
			this.tsRefreshList.Click += new System.EventHandler(this.tsRefreshList_Click);
			// 
			// tsEnter
			// 
			this.tsEnter.Name = "tsEnter";
			this.tsEnter.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
			this.tsEnter.Size = new System.Drawing.Size(243, 22);
			this.tsEnter.Text = "Открыть";
			this.tsEnter.Click += new System.EventHandler(this.tsEnter_Click);
			// 
			// tsBack
			// 
			this.tsBack.Name = "tsBack";
			this.tsBack.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
			this.tsBack.Size = new System.Drawing.Size(243, 22);
			this.tsBack.Text = "Назад";
			this.tsBack.Click += new System.EventHandler(this.tsBack_Click);
			// 
			// tsOpenCard
			// 
			this.tsOpenCard.Name = "tsOpenCard";
			this.tsOpenCard.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
			this.tsOpenCard.Size = new System.Drawing.Size(243, 22);
			this.tsOpenCard.Text = "Открыть карточку";
			this.tsOpenCard.Click += new System.EventHandler(this.tsOpenCard_Click);
			// 
			// tsCreateOrgUnit
			// 
			this.tsCreateOrgUnit.Name = "tsCreateOrgUnit";
			this.tsCreateOrgUnit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
			this.tsCreateOrgUnit.Size = new System.Drawing.Size(243, 22);
			this.tsCreateOrgUnit.Text = "Создать подразделение";
			this.tsCreateOrgUnit.Click += new System.EventHandler(this.tsCreateOrgUnit_Click);
			// 
			// tsCreatePerson
			// 
			this.tsCreatePerson.Name = "tsCreatePerson";
			this.tsCreatePerson.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D5)));
			this.tsCreatePerson.Size = new System.Drawing.Size(243, 22);
			this.tsCreatePerson.Text = "Создать сотрудника";
			this.tsCreatePerson.Click += new System.EventHandler(this.tsCreatePerson_Click);
			// 
			// tsDelete
			// 
			this.tsDelete.Name = "tsDelete";
			this.tsDelete.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D6)));
			this.tsDelete.Size = new System.Drawing.Size(243, 22);
			this.tsDelete.Text = "Удалить";
			this.tsDelete.Click += new System.EventHandler(this.tsDelete_Click);
			// 
			// tlEventsPopup
			// 
			this.tlEventsPopup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsEvents});
			this.tlEventsPopup.Name = "tlEventsPopup";
			this.tlEventsPopup.Size = new System.Drawing.Size(68, 20);
			this.tlEventsPopup.Text = "События";
			// 
			// tsEvents
			// 
			this.tsEvents.Name = "tsEvents";
			this.tsEvents.Size = new System.Drawing.Size(124, 22);
			this.tsEvents.Text = "Показать";
			this.tsEvents.Click += new System.EventHandler(this.tsEvents_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.получитьСостояниеToolStripMenuItem});
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(100, 20);
			this.toolStripMenuItem1.Text = "Оборудование";
			// 
			// получитьСостояниеToolStripMenuItem
			// 
			this.получитьСостояниеToolStripMenuItem.Name = "получитьСостояниеToolStripMenuItem";
			this.получитьСостояниеToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
			this.получитьСостояниеToolStripMenuItem.Text = "Команды\\Состояние";
			this.получитьСостояниеToolStripMenuItem.Click += new System.EventHandler(this.получитьСостояниеToolStripMenuItem_Click);
			// 
			// stStatus
			// 
			this.stStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsUserStatus,
            this.tsStatusString});
			this.stStatus.Location = new System.Drawing.Point(0, 452);
			this.stStatus.Name = "stStatus";
			this.stStatus.Size = new System.Drawing.Size(659, 22);
			this.stStatus.TabIndex = 1;
			this.stStatus.Text = "statusStrip1";
			// 
			// tsUserStatus
			// 
			this.tsUserStatus.Name = "tsUserStatus";
			this.tsUserStatus.Size = new System.Drawing.Size(0, 17);
			// 
			// tsStatusString
			// 
			this.tsStatusString.Name = "tsStatusString";
			this.tsStatusString.Size = new System.Drawing.Size(0, 17);
			// 
			// lvPersons
			// 
			this.lvPersons.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLastName,
            this.colFirstName,
            this.colMiddleName,
            this.colTabNum});
			this.lvPersons.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvPersons.FullRowSelect = true;
			this.lvPersons.GridLines = true;
			this.lvPersons.HideSelection = false;
			this.lvPersons.LargeImageList = this.ilLargeIcons;
			this.lvPersons.Location = new System.Drawing.Point(0, 24);
			this.lvPersons.MultiSelect = false;
			this.lvPersons.Name = "lvPersons";
			this.lvPersons.Size = new System.Drawing.Size(659, 428);
			this.lvPersons.SmallImageList = this.ilSmallIcons;
			this.lvPersons.TabIndex = 2;
			this.lvPersons.UseCompatibleStateImageBehavior = false;
			this.lvPersons.View = System.Windows.Forms.View.Details;
			this.lvPersons.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvPersons_KeyUp);
			this.lvPersons.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvPersons_MouseDoubleClick);
			// 
			// colLastName
			// 
			this.colLastName.Text = "Фамилия";
			// 
			// colFirstName
			// 
			this.colFirstName.Text = "Имя";
			// 
			// colMiddleName
			// 
			this.colMiddleName.Text = "Отчество";
			// 
			// colTabNum
			// 
			this.colTabNum.Text = "Табельный номер";
			// 
			// ilLargeIcons
			// 
			this.ilLargeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilLargeIcons.ImageStream")));
			this.ilLargeIcons.TransparentColor = System.Drawing.Color.Transparent;
			this.ilLargeIcons.Images.SetKeyName(0, "folder.png");
			this.ilLargeIcons.Images.SetKeyName(1, "user.png");
			// 
			// ilSmallIcons
			// 
			this.ilSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSmallIcons.ImageStream")));
			this.ilSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
			this.ilSmallIcons.Images.SetKeyName(0, "folder.png");
			this.ilSmallIcons.Images.SetKeyName(1, "user.png");
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(659, 474);
			this.Controls.Add(this.lvPersons);
			this.Controls.Add(this.stStatus);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.ShowIcon = false;
			this.Text = "Клиент интеграции ParsecNET3";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.stStatus.ResumeLayout(false);
			this.stStatus.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tlFilePopup;
        private System.Windows.Forms.ToolStripMenuItem tlChangeUser;
        private System.Windows.Forms.ToolStripMenuItem tsExit;
        private System.Windows.Forms.ToolStripMenuItem tlEditPopup;
        private System.Windows.Forms.ToolStripMenuItem tsCreateOrgUnit;
        private System.Windows.Forms.ToolStripMenuItem tsCreatePerson;
        private System.Windows.Forms.ToolStripMenuItem tsDelete;
        private System.Windows.Forms.ToolStripMenuItem tlEventsPopup;
        private System.Windows.Forms.ToolStripMenuItem tsEvents;
        private System.Windows.Forms.StatusStrip stStatus;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusString;
        private System.Windows.Forms.ToolStripStatusLabel tsUserStatus;
        private System.Windows.Forms.ListView lvPersons;
        private System.Windows.Forms.ColumnHeader colLastName;
        private System.Windows.Forms.ColumnHeader colFirstName;
        private System.Windows.Forms.ColumnHeader colMiddleName;
        private System.Windows.Forms.ColumnHeader colTabNum;
        private System.Windows.Forms.ImageList ilSmallIcons;
        private System.Windows.Forms.ImageList ilLargeIcons;
        private System.Windows.Forms.ToolStripMenuItem tsOpenCard;
        private System.Windows.Forms.ToolStripMenuItem tsEnter;
        private System.Windows.Forms.ToolStripMenuItem tsBack;

		private void получитьСостояниеToolStripMenuItem_Click ( object sender, EventArgs e )
		{
			try
			{
				using ( var frm = new DeviceState() )
					frm.ShowDialog( this );
			}
			catch ( Exception ex )
			{
				MessageBox.Show( ex.ToString(), "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}

		private void InternalTest()
		{
			var iService = new IntegrationService();

			var person = new Person() { LAST_NAME = "Сусликов", FIRST_NAME = "Максим", ORG_ID = _showOrgUnitID };

			var gResult = iService.CreateVisitor( ClientState.SessionID, person );
			person.ID = gResult.Value;

			var accGroups = iService.GetAccessGroups( ClientState.SessionID );

			const string cardCode = "78451299";
			var identifier = new Identifier() { CODE = cardCode, ACCGROUP_ID = accGroups[ 0 ].ID, IS_PRIMARY = true, PERSON_ID = person.ID };
			gResult = iService.OpenPersonEditingSession( ClientState.SessionID, person.ID );
			var bResult = iService.AddPersonIdentifier( gResult.Value, identifier );

			iService.SetIdentifierPrivileges( ClientState.SessionID, cardCode, 512 );

			gResult = iService.OpenPersonEditingSession( ClientState.SessionID, person.ID );
			iService.BlockPerson( gResult.Value, 1 );
		}
	}
}
