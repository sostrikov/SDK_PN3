using System;
using System.Windows.Forms;

using ParsecIntegrationClient.IntegrationWebService;

namespace ParsecIntegrationClient
{
    public class AccessGroupsListForm : Form
    {
        private string _initialFormText = null;

        public AccessGroupsListForm()
        {
            InitializeComponent();
        }

        public AccessGroup SelectedAccessGroup
        {
            get
            {
                if (lvList.SelectedItems == null || lvList.SelectedItems.Count == 0)
                    return null;
                return lvList.SelectedItems[lvList.SelectedItems.Count - 1].Tag as AccessGroup;
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            _initialFormText = Text;
            ShowList();
        }

        private void ShowList()
        {
            try
            {
                IntegrationService integServ = new IntegrationService();
                AccessGroup[] resList = integServ.GetAccessGroups(ClientState.SessionID);
                ClientState.SetAccessGroupList(resList);
                try
                {
                    lvList.BeginUpdate();
                    lvList.Items.Clear();
                    if (resList == null || resList.Length == 0)
                        return;
                    for (int i = 0; i < resList.Length; i++)
                    {
                        if (resList[i] == null)
                            continue;
                        ListViewItem lvItem = new ListViewItem(resList[i].NAME ?? string.Empty);
                        lvItem.ImageIndex = 0;
                        lvItem.Tag = resList[i];
                        lvList.Items.Add(lvItem);
                    }
                    for (int i = lvList.Columns.Count - 1; i >= 0; i--)
                        lvList.Columns[i].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                }
                finally
                {
                    lvList.EndUpdate();
                    Text = string.Format("{0} - в списке {1} элементов", _initialFormText ?? string.Empty, lvList.Items.Count);
                }
            }
            catch (Exception ex)
            {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccessGroupsListForm));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.lvList = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.ilSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.ilLargeIcons = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(419, 422);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Закрыть";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(338, 422);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Выбрать";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // lvList
            // 
            this.lvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName});
            this.lvList.FullRowSelect = true;
            this.lvList.GridLines = true;
            this.lvList.LargeImageList = this.ilLargeIcons;
            this.lvList.Location = new System.Drawing.Point(0, 0);
            this.lvList.Name = "lvList";
            this.lvList.Size = new System.Drawing.Size(494, 416);
            this.lvList.SmallImageList = this.ilSmallIcons;
            this.lvList.TabIndex = 5;
            this.lvList.UseCompatibleStateImageBehavior = false;
            this.lvList.View = System.Windows.Forms.View.Details;
            // 
            // colName
            // 
            this.colName.Text = "Название";
            // 
            // ilSmallIcons
            // 
            this.ilSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSmallIcons.ImageStream")));
            this.ilSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilSmallIcons.Images.SetKeyName(0, "lock.png");
            // 
            // ilLargeIcons
            // 
            this.ilLargeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilLargeIcons.ImageStream")));
            this.ilLargeIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilLargeIcons.Images.SetKeyName(0, "lock.png");
            // 
            // AccessGroupsListForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(498, 447);
            this.Controls.Add(this.lvList);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccessGroupsListForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Группы доступа";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ListView lvList;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ImageList ilSmallIcons;
        private System.Windows.Forms.ImageList ilLargeIcons;
    }
}
