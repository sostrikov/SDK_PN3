using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections.Generic;

using ParsecIntegrationClient.IntegrationWebService;

namespace ParsecIntegrationClient
{
    public class PersonForm : Form
    {
        private Guid _shownPersID = Guid.Empty;
        private Guid _editSessionID = Guid.Empty;
        private PersonWithPhoto _shownPerson = null;
        private bool _creation = false;
        private int _changesFlags = 0;
        private const int PhotoHasChanged = 1;
        private const int OrgUnitHasChanged = 2;
        private const int SomeOtherChanges = 4;

        public PersonForm()
        {
            InitializeComponent();
            txtOrgUnit.Tag = Guid.Empty;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            ShowPerson();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
            if (_creation || Guid.Empty.Equals(_editSessionID))
                return;
            // close session if it was open
            try
            {
                IntegrationService integServ = new IntegrationService();
                integServ.ClosePersonEditingSession(_editSessionID);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowError(ex);
            }
        }

        public void SetPerson(Guid persID)
        {
            _shownPersID = persID;
            txtOrgUnit.Tag = Guid.Empty;
            _shownPerson = null;
            _editSessionID = Guid.Empty;
            _creation = false;
            _changesFlags = 0;
            btnOk.Visible = false;
            btnEdit.Visible = true;
        }

        public void CreatePerson(Guid parentOrgUnitID)
        {
            _shownPersID = Guid.Empty;
            _shownPerson = null;
            _editSessionID = Guid.Empty;
            _creation = true;
            _changesFlags = 0;
            btnOk.Visible = true;
            btnEdit.Visible = false;
            ClearDetails();
            txtOrgUnit.Tag = parentOrgUnitID;
            txtOrgUnit.Text = ClientState.GetOrgUnitName(parentOrgUnitID) ?? string.Empty;
            tsAddIdentifier.Enabled = false;
            tsChangeIdentifier.Enabled = false;
            tsDeleteIdentifier.Enabled = false;
        }

        private void OnEdit()
        {
            // start new editing session
            try
            {
                IntegrationService integServ = new IntegrationService();
                GuidResult res = integServ.OpenPersonEditingSession(ClientState.SessionID, _shownPersID);
                if (res.Result != ClientState.Result_Success)
                {
                    CommonFunctions.ShowErrorMessage(res.ErrorMessage);
                    return;
                }
                _editSessionID = res.Value;
                tsAddIdentifier.Enabled = true;
                tsChangeIdentifier.Enabled = true;
                btnOk.Visible = true;
                btnEdit.Visible = false;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowError(ex);
            }
        }

        private void OnOk()
        {
            _changesFlags = 4;
            try
            {
                // try to save orgUnit
                IntegrationService integServ = new IntegrationService();
                if (_creation)
                {
                    Person savingPerson = null;
                    if (pcbPhoto.Image == null)
                        savingPerson = new Person();
                    else
                    {
                        PersonWithPhoto personItemWithPhoto = new PersonWithPhoto();
                        personItemWithPhoto.PHOTO = CommonFunctions.GetPhotoArray(pcbPhoto.Image);
                        savingPerson = personItemWithPhoto;
                    }
                    savingPerson.LAST_NAME = txtLastName.Text;
                    savingPerson.FIRST_NAME = txtFirstName.Text;
                    savingPerson.MIDDLE_NAME = txtMiddleName.Text;
                    savingPerson.TAB_NUM = txtTabNum.Text;
                    savingPerson.ORG_ID = (Guid)txtOrgUnit.Tag;
                    GuidResult res = integServ.CreatePerson(ClientState.SessionID, savingPerson);
                    if (res.Result != ClientState.Result_Success)
                    {
                        CommonFunctions.ShowErrorMessage(res.ErrorMessage);
                        return;
                    }
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }
                if (_shownPerson == null)
                {
                    DialogResult = DialogResult.Cancel;
                    _editSessionID = Guid.Empty;
                    Close();
                    return;
                }
                if (!_shownPerson.ORG_ID.Equals(txtOrgUnit.Tag))
                    _changesFlags |= OrgUnitHasChanged;
                if (!string.Equals(txtLastName.Text, _shownPerson.LAST_NAME) || !string.Equals(txtFirstName.Text, _shownPerson.FIRST_NAME) || !string.Equals(txtMiddleName.Text, _shownPerson.MIDDLE_NAME) || !string.Equals(txtTabNum.Text, _shownPerson.TAB_NUM))
                    _changesFlags |= SomeOtherChanges;
                
                if (_changesFlags == 0)
                {
                    // no changes were made
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }
                BaseResult saveRes = null;
                if ((_changesFlags & (~OrgUnitHasChanged)) == 0)
                {
                    // just org unit has changed
                    saveRes = integServ.SetPersonOrgUnit(_editSessionID, (Guid)txtOrgUnit.Tag);
                }
                else if ((_changesFlags & (~PhotoHasChanged)) == 0)
                {
                    // just photo has changed
                    saveRes = integServ.SetPersonPhoto(_editSessionID, CommonFunctions.GetPhotoArray(pcbPhoto.Image));
                }
                else
                {
                    BasePerson savingItem = null;
                    if ((_changesFlags & PhotoHasChanged) != 0)
                    {
                        PersonWithPhoto buf = new PersonWithPhoto();
                        buf.ORG_ID = (Guid)txtOrgUnit.Tag;
                        buf.PHOTO = CommonFunctions.GetPhotoArray(pcbPhoto.Image);
                        savingItem = buf;
                    }
                    else if ((_changesFlags & OrgUnitHasChanged) != 0)
                    {
                        Person buf = new Person();
                        buf.ORG_ID = (Guid)txtOrgUnit.Tag;
                        savingItem = buf;
                    }
                    else
                        savingItem = new BasePerson();
                    savingItem.ID = _shownPerson.ID;
                    savingItem.LAST_NAME = txtLastName.Text;
                    savingItem.FIRST_NAME = txtFirstName.Text;
                    savingItem.MIDDLE_NAME = txtMiddleName.Text;
                    savingItem.TAB_NUM = txtTabNum.Text;
                    saveRes = integServ.SavePerson(_editSessionID, savingItem);
                }
                if (saveRes.Result != ClientState.Result_Success)
                {
                    CommonFunctions.ShowErrorMessage(saveRes.ErrorMessage);
                    return;
                }

                GuidResult rOpen = integServ.OpenPersonEditingSession(ClientState.SessionID, _shownPersID);
                if (rOpen.Result != ClientState.Result_Success)
                {
                    CommonFunctions.ShowErrorMessage(saveRes.ErrorMessage);
                    return;
                }
                saveRes = integServ.SetPersonExtraFieldValue(rOpen.Value, new Guid("19000B1B-C9B5-4A3E-B0AA-E241CDD4FAD2"), "updated: " + DateTime.Now.ToString());
                List<ExtraFieldValue> ex_vals = new List<ExtraFieldValue>();
                ExtraFieldValue v = new ExtraFieldValue();
                v.TEMPLATE_ID = new Guid("12967E13-7A04-45DB-9A81-6DD6E0A3D25F");
                v.VALUE = "updated: " + DateTime.Now.ToString();
                ex_vals.Add(v);
                v = new ExtraFieldValue();
                v.TEMPLATE_ID = new Guid("90567397-F0F8-4FB1-9E37-C5FEF2D43956");
                v.VALUE = DateTime.Now.Date;
                ex_vals.Add(v);

                saveRes = integServ.SetPersonExtraFieldValues(rOpen.Value, ex_vals.ToArray());
                if (saveRes.Result != ClientState.Result_Success)
                {
                    CommonFunctions.ShowErrorMessage(saveRes.ErrorMessage);
                    return;
                }


                _editSessionID = Guid.Empty;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowError(ex);
            }
        }

        private void OnAddIdentifier()
        {
            if (Guid.Empty.Equals(_editSessionID))
                return;
            try
            {
                using (IdentifierForm frm = new IdentifierForm())
                {
                    frm.CreateNewIdentifier(_editSessionID);
                    if (frm.ShowDialog(this) != DialogResult.OK)
                        return;
                    RefreshIdentifiersList();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowError(ex);
            }
        }

        private void OnChangeIdentifier(Identifier identItem)
        {
            if (identItem == null || Guid.Empty.Equals(_editSessionID))
                return;
            try
            {
                using (IdentifierForm frm = new IdentifierForm())
                {
                    frm.ChangePersonIdentifier(_editSessionID, identItem);
                    if (frm.ShowDialog(this) != DialogResult.OK)
                        return;
                    RefreshIdentifiersList();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowError(ex);
            }
        }

        private void OnDeleteIdentifier(Identifier identItem)
        {
            if (identItem == null)
                return;
            if (CommonFunctions.AskYesNoQuestion(string.Format("Вы действительно хотите удалить идентификатор {0}?", identItem.CODE ?? string.Empty), "Удаление идентификатора") != DialogResult.Yes)
                return;
            try
            {
                IntegrationService integServ = new IntegrationService();
                BaseResult res = integServ.DeleteIdentifier(ClientState.SessionID, identItem.CODE);
                if (res.Result != ClientState.Result_Success)
                {
                    CommonFunctions.ShowErrorMessage(res.ErrorMessage);
                    return;
                }
                RefreshIdentifiersList();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowError(ex);
            }
        }

        private void ShowPerson()
        {
            try
            {
                if (_shownPersID.Equals(Guid.Empty))
                    return;
                try
                {
                    SuspendLayout();
                    ClearDetails();
                    IntegrationService integServ = new IntegrationService();
                    PersonWithPhoto personItem = integServ.GetPerson(ClientState.SessionID, _shownPersID);
                    if (personItem == null)
                        return;
                    _shownPerson = personItem;
                    txtLastName.Text = personItem.LAST_NAME ?? string.Empty;
                    txtFirstName.Text = personItem.FIRST_NAME ?? string.Empty;
                    txtMiddleName.Text = personItem.MIDDLE_NAME ?? string.Empty;
                    txtTabNum.Text = personItem.TAB_NUM ?? string.Empty;
                    txtOrgUnit.Tag = personItem.ORG_ID;
                    txtOrgUnit.Text = ClientState.GetOrgUnitName(personItem.ORG_ID) ?? string.Empty;
                    byte[] photoArr = personItem.PHOTO;
                    if (photoArr == null || photoArr.Length == 0)
                        pcbPhoto.Image = null;
                    else
                    {
                        MemoryStream ms = new MemoryStream(photoArr);
                        pcbPhoto.Image = Image.FromStream(ms);
                    }
                    tsAddIdentifier.Enabled = false;
                    tsChangeIdentifier.Enabled = false;
                    tsDeleteIdentifier.Enabled = true;
                    RefreshIdentifiersList();
                }
                finally
                {
                    ResumeLayout();
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowError(ex);
            }
        }

        private void RefreshIdentifiersList()
        {
            try
            {
                lvIdentifiers.BeginUpdate();
                lvIdentifiers.Items.Clear();
                // get identifiers
                IntegrationService integServ = new IntegrationService();
                Identifier[] identifiersList = integServ.GetPersonIdentifiers(ClientState.SessionID, _shownPersID);
                if (identifiersList == null || identifiersList.Length == 0)
                    return;

                for (int i = 0; i < identifiersList.Length; i++)
                {
                    if (identifiersList[i] == null)
                        continue;
                    ListViewItem lvItem = new ListViewItem(identifiersList[i].CODE ?? string.Empty);
                    lvItem.ImageIndex = 0;
                    lvItem.Tag = identifiersList[i];
                    if (identifiersList[i].IS_PRIMARY)
                        lvItem.SubItems.Add("Да");
                    else
                        lvItem.SubItems.Add("Нет");
                    InitAccGroupsList();
                    lvItem.SubItems.Add(ClientState.GetAccGroupName(identifiersList[i].ACCGROUP_ID) ?? string.Empty);
                    IdentifierTemp tempItem = identifiersList[i] as IdentifierTemp;
                    if (tempItem == null)
                    {
                        lvIdentifiers.Items.Add(lvItem);
                        continue;
                    }
                    lvItem.SubItems.Add(tempItem.VALID_FROM.ToString());
                    lvItem.SubItems.Add(tempItem.VALID_TO.ToString());
                    lvIdentifiers.Items.Add(lvItem);
                }
                for (int i = lvIdentifiers.Columns.Count - 1; i >= 0; i--)
                    lvIdentifiers.Columns[i].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            finally
            {
                lvIdentifiers.EndUpdate();
            }
        }

        private void InitAccGroupsList()
        {
            if (ClientState.AccessGroupsListSet)
                return;
            try
            {
                IntegrationService integServ = new IntegrationService();
                ClientState.SetAccessGroupList(integServ.GetAccessGroups(ClientState.SessionID));
            }
            catch (Exception ex)
            {
            }
        }

        private void ClearDetails()
        {
            txtLastName.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtMiddleName.Text = string.Empty;
            txtTabNum.Text = string.Empty;
            txtOrgUnit.Text = string.Empty;
            pcbPhoto.Image = null;
        }

        private void ShowOrgUnitHierarhy()
        {
            using (OrgUnitHierarhyForm frm = new OrgUnitHierarhyForm())
            {
                if (frm.ShowDialog(this) != DialogResult.OK)
                    return;
                OrgUnit selectedObject = frm.SelectedOrgUnit;
                if (selectedObject == null || selectedObject.ID.Equals(txtOrgUnit.Tag))
                    return;
                txtOrgUnit.Tag = selectedObject.ID;
                txtOrgUnit.Text = string.Format("{0}", selectedObject.NAME ?? string.Empty);
            }
        }

        private void btnSelectOrgUnit_Click(object sender, EventArgs e)
        {
            ShowOrgUnitHierarhy();
        }

        private void tsChangePhoto_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = CommonFunctions.GetImageCodecsFilesFilter(ImageCodecInfo.GetImageDecoders(), true);
                ofd.FilterIndex = 1;
                ofd.Multiselect = false;
                ofd.RestoreDirectory = true;
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    pcbPhoto.Image = CommonFunctions.LoadFromFile(ofd.FileName);
                    _changesFlags |= PhotoHasChanged;
                }
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowError(ex);
            }
        }

        private void tsClearPhoto_Click(object sender, EventArgs e)
        {
            if (pcbPhoto.Image == null)
                return;
            pcbPhoto.Image = null;
            _changesFlags |= PhotoHasChanged;
        }

        private void tsAddIdentifier_Click(object sender, EventArgs e)
        {
            OnAddIdentifier();
        }

        private void tsChangeIdentifier_Click(object sender, EventArgs e)
        {
            if (lvIdentifiers.SelectedItems == null || lvIdentifiers.SelectedItems.Count == 0 || Guid.Empty.Equals(_editSessionID))
                return;
            OnChangeIdentifier(lvIdentifiers.SelectedItems[lvIdentifiers.SelectedItems.Count - 1].Tag as Identifier);
        }

        private void tsDeleteIdentifier_Click(object sender, EventArgs e)
        {
            if (lvIdentifiers.SelectedItems == null || lvIdentifiers.SelectedItems.Count == 0)
                return;
            OnDeleteIdentifier(lvIdentifiers.SelectedItems[lvIdentifiers.SelectedItems.Count - 1].Tag as Identifier);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            OnOk();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEdit();
        }

        private void lvIdentifiers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem lvItem = lvIdentifiers.GetItemAt(e.X, e.Y);
            if (lvItem == null)
                return;
            OnChangeIdentifier(lvItem.Tag as Identifier);
        }

        private void lvIdentifiers_KeyUp(object sender, KeyEventArgs e)
        {
            if (lvIdentifiers.SelectedItems == null || lvIdentifiers.SelectedItems.Count == 0)
                return;
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    OnChangeIdentifier(lvIdentifiers.SelectedItems[lvIdentifiers.SelectedItems.Count - 1].Tag as Identifier);
                    break;
                case Keys.Delete:
                    OnDeleteIdentifier(lvIdentifiers.SelectedItems[lvIdentifiers.SelectedItems.Count - 1].Tag as Identifier);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonForm));
            this.pcbPhoto = new System.Windows.Forms.PictureBox();
            this.cmnPhotoContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsChangePhoto = new System.Windows.Forms.ToolStripMenuItem();
            this.tsClearPhoto = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtMiddleName = new System.Windows.Forms.TextBox();
            this.txtTabNum = new System.Windows.Forms.TextBox();
            this.txtOrgUnit = new System.Windows.Forms.TextBox();
            this.btnSelectOrgUnit = new System.Windows.Forms.Button();
            this.lvIdentifiers = new System.Windows.Forms.ListView();
            this.colCode = new System.Windows.Forms.ColumnHeader();
            this.colIsPrimary = new System.Windows.Forms.ColumnHeader();
            this.colAccessGroup = new System.Windows.Forms.ColumnHeader();
            this.colFrom = new System.Windows.Forms.ColumnHeader();
            this.colTo = new System.Windows.Forms.ColumnHeader();
            this.cnmIdentifiers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsAddIdentifier = new System.Windows.Forms.ToolStripMenuItem();
            this.tsChangeIdentifier = new System.Windows.Forms.ToolStripMenuItem();
            this.tsDeleteIdentifier = new System.Windows.Forms.ToolStripMenuItem();
            this.ilLargeIcons = new System.Windows.Forms.ImageList(this.components);
            this.ilSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pcbPhoto)).BeginInit();
            this.cmnPhotoContext.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.cnmIdentifiers.SuspendLayout();
            this.SuspendLayout();
            // 
            // pcbPhoto
            // 
            this.pcbPhoto.ContextMenuStrip = this.cmnPhotoContext;
            this.pcbPhoto.Location = new System.Drawing.Point(0, 0);
            this.pcbPhoto.Name = "pcbPhoto";
            this.pcbPhoto.Size = new System.Drawing.Size(120, 160);
            this.pcbPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcbPhoto.TabIndex = 0;
            this.pcbPhoto.TabStop = false;
            // 
            // cmnPhotoContext
            // 
            this.cmnPhotoContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsChangePhoto,
            this.tsClearPhoto});
            this.cmnPhotoContext.Name = "cmnPhotoContext";
            this.cmnPhotoContext.Size = new System.Drawing.Size(130, 48);
            // 
            // tsChangePhoto
            // 
            this.tsChangePhoto.Name = "tsChangePhoto";
            this.tsChangePhoto.Size = new System.Drawing.Size(129, 22);
            this.tsChangePhoto.Text = "Поменять";
            this.tsChangePhoto.Click += new System.EventHandler(this.tsChangePhoto_Click);
            // 
            // tsClearPhoto
            // 
            this.tsClearPhoto.Name = "tsClearPhoto";
            this.tsClearPhoto.Size = new System.Drawing.Size(129, 22);
            this.tsClearPhoto.Text = "Очистить";
            this.tsClearPhoto.Click += new System.EventHandler(this.tsClearPhoto_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtLastName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtFirstName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtMiddleName, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtTabNum, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.txtOrgUnit, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.btnSelectOrgUnit, 2, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(126, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(345, 160);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Фамилия";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Имя";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Отчество";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Табельный номер";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Подразделение";
            // 
            // txtLastName
            // 
            this.txtLastName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txtLastName, 2);
            this.txtLastName.Location = new System.Drawing.Point(108, 3);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(234, 20);
            this.txtLastName.TabIndex = 5;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txtFirstName, 2);
            this.txtFirstName.Location = new System.Drawing.Point(108, 29);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(234, 20);
            this.txtFirstName.TabIndex = 6;
            // 
            // txtMiddleName
            // 
            this.txtMiddleName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txtMiddleName, 2);
            this.txtMiddleName.Location = new System.Drawing.Point(108, 55);
            this.txtMiddleName.Name = "txtMiddleName";
            this.txtMiddleName.Size = new System.Drawing.Size(234, 20);
            this.txtMiddleName.TabIndex = 7;
            // 
            // txtTabNum
            // 
            this.txtTabNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txtTabNum, 2);
            this.txtTabNum.Location = new System.Drawing.Point(108, 81);
            this.txtTabNum.Name = "txtTabNum";
            this.txtTabNum.Size = new System.Drawing.Size(234, 20);
            this.txtTabNum.TabIndex = 8;
            // 
            // txtOrgUnit
            // 
            this.txtOrgUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrgUnit.Location = new System.Drawing.Point(108, 108);
            this.txtOrgUnit.Name = "txtOrgUnit";
            this.txtOrgUnit.ReadOnly = true;
            this.txtOrgUnit.Size = new System.Drawing.Size(205, 20);
            this.txtOrgUnit.TabIndex = 9;
            // 
            // btnSelectOrgUnit
            // 
            this.btnSelectOrgUnit.Image = global::ParsecIntegrationClient.Properties.Resources.folder_find;
            this.btnSelectOrgUnit.Location = new System.Drawing.Point(319, 107);
            this.btnSelectOrgUnit.Name = "btnSelectOrgUnit";
            this.btnSelectOrgUnit.Size = new System.Drawing.Size(23, 23);
            this.btnSelectOrgUnit.TabIndex = 10;
            this.btnSelectOrgUnit.UseVisualStyleBackColor = true;
            this.btnSelectOrgUnit.Click += new System.EventHandler(this.btnSelectOrgUnit_Click);
            // 
            // lvIdentifiers
            // 
            this.lvIdentifiers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvIdentifiers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colCode,
            this.colIsPrimary,
            this.colAccessGroup,
            this.colFrom,
            this.colTo});
            this.lvIdentifiers.ContextMenuStrip = this.cnmIdentifiers;
            this.lvIdentifiers.FullRowSelect = true;
            this.lvIdentifiers.GridLines = true;
            this.lvIdentifiers.HideSelection = false;
            this.lvIdentifiers.LargeImageList = this.ilLargeIcons;
            this.lvIdentifiers.Location = new System.Drawing.Point(0, 166);
            this.lvIdentifiers.Name = "lvIdentifiers";
            this.lvIdentifiers.Size = new System.Drawing.Size(471, 174);
            this.lvIdentifiers.SmallImageList = this.ilSmallIcons;
            this.lvIdentifiers.TabIndex = 3;
            this.lvIdentifiers.UseCompatibleStateImageBehavior = false;
            this.lvIdentifiers.View = System.Windows.Forms.View.Details;
            this.lvIdentifiers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvIdentifiers_MouseDoubleClick);
            this.lvIdentifiers.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvIdentifiers_KeyUp);
            // 
            // colCode
            // 
            this.colCode.Text = "Код";
            // 
            // colIsPrimary
            // 
            this.colIsPrimary.Text = "Первичный";
            // 
            // colAccessGroup
            // 
            this.colAccessGroup.Text = "Группа доступа";
            // 
            // colFrom
            // 
            this.colFrom.Text = "С";
            // 
            // colTo
            // 
            this.colTo.Text = "По";
            // 
            // cnmIdentifiers
            // 
            this.cnmIdentifiers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAddIdentifier,
            this.tsChangeIdentifier,
            this.tsDeleteIdentifier});
            this.cnmIdentifiers.Name = "cnmIdentifiers";
            this.cnmIdentifiers.Size = new System.Drawing.Size(129, 70);
            // 
            // tsAddIdentifier
            // 
            this.tsAddIdentifier.Name = "tsAddIdentifier";
            this.tsAddIdentifier.Size = new System.Drawing.Size(128, 22);
            this.tsAddIdentifier.Text = "Добавить";
            this.tsAddIdentifier.Click += new System.EventHandler(this.tsAddIdentifier_Click);
            // 
            // tsChangeIdentifier
            // 
            this.tsChangeIdentifier.Name = "tsChangeIdentifier";
            this.tsChangeIdentifier.Size = new System.Drawing.Size(128, 22);
            this.tsChangeIdentifier.Text = "Изменить";
            this.tsChangeIdentifier.Click += new System.EventHandler(this.tsChangeIdentifier_Click);
            // 
            // tsDeleteIdentifier
            // 
            this.tsDeleteIdentifier.Name = "tsDeleteIdentifier";
            this.tsDeleteIdentifier.Size = new System.Drawing.Size(128, 22);
            this.tsDeleteIdentifier.Text = "Удалить";
            this.tsDeleteIdentifier.Click += new System.EventHandler(this.tsDeleteIdentifier_Click);
            // 
            // ilLargeIcons
            // 
            this.ilLargeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilLargeIcons.ImageStream")));
            this.ilLargeIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilLargeIcons.Images.SetKeyName(0, "key.png");
            // 
            // ilSmallIcons
            // 
            this.ilSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSmallIcons.ImageStream")));
            this.ilSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilSmallIcons.Images.SetKeyName(0, "key.png");
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(390, 346);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Закрыть";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(309, 346);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Сохранить";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(291, 345);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(93, 23);
            this.btnEdit.TabIndex = 6;
            this.btnEdit.Text = "Редактировать";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // PersonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(472, 370);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.lvIdentifiers);
            this.Controls.Add(this.pcbPhoto);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PersonForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Сотрудник";
            ((System.ComponentModel.ISupportInitialize)(this.pcbPhoto)).EndInit();
            this.cmnPhotoContext.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.cnmIdentifiers.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pcbPhoto;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtMiddleName;
        private System.Windows.Forms.TextBox txtTabNum;
        private System.Windows.Forms.TextBox txtOrgUnit;
        private System.Windows.Forms.Button btnSelectOrgUnit;
        private System.Windows.Forms.ContextMenuStrip cmnPhotoContext;
        private System.Windows.Forms.ToolStripMenuItem tsChangePhoto;
        private System.Windows.Forms.ToolStripMenuItem tsClearPhoto;
        private System.Windows.Forms.ListView lvIdentifiers;
        private System.Windows.Forms.ColumnHeader colCode;
        private System.Windows.Forms.ColumnHeader colIsPrimary;
        private System.Windows.Forms.ColumnHeader colAccessGroup;
        private System.Windows.Forms.ColumnHeader colFrom;
        private System.Windows.Forms.ColumnHeader colTo;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ImageList ilSmallIcons;
        private System.Windows.Forms.ImageList ilLargeIcons;
        private System.Windows.Forms.ContextMenuStrip cnmIdentifiers;
        private System.Windows.Forms.ToolStripMenuItem tsAddIdentifier;
        private System.Windows.Forms.ToolStripMenuItem tsChangeIdentifier;
        private System.Windows.Forms.ToolStripMenuItem tsDeleteIdentifier;
        private System.Windows.Forms.Button btnEdit;
    }
}

