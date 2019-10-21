using System;
using System.Windows.Forms;

using ParsecIntegrationClient.IntegrationWebService;

namespace ParsecIntegrationClient
{
    public class IdentifierForm : Form
    {
        private Guid _persEditSessionID = Guid.Empty;
        private Identifier _shownIdentifier = null;
        private bool _creation = false;

        public IdentifierForm()
        {
            InitializeComponent();
        }

        public void CreateNewIdentifier(Guid persEditSessionID)
        {
            _persEditSessionID = persEditSessionID;
            _creation = true;
            _shownIdentifier = null;
            txtAccessGroup.Tag = Guid.Empty;
            txtCode.ReadOnly = false;
        }

        public void ChangePersonIdentifier(Guid persEditSessionID, Identifier editIdentifier)
        {
            _persEditSessionID = persEditSessionID;
            _creation = false;
            _shownIdentifier = editIdentifier;
            txtCode.ReadOnly = true;
            txtCode.Text = editIdentifier.CODE ?? string.Empty;
            txtAccessGroup.Tag = editIdentifier.ACCGROUP_ID;
            txtAccessGroup.Text = ClientState.GetAccGroupName(editIdentifier.ACCGROUP_ID);
            chkPrimary.Checked = editIdentifier.IS_PRIMARY;
            IdentifierTemp tempIdent = editIdentifier as IdentifierTemp;
            if (tempIdent == null)
                return;
            chkTemp.Checked = true;
            dtpFrom.Value = tempIdent.VALID_FROM;
            dtpTo.Value = tempIdent.VALID_TO;
        }

        private void OnOk()
        {
            try
            {
                // try to save orgUnit
                IntegrationService integServ = new IntegrationService();
                if (_creation)
                {
                    BaseIdentifier creatingItem = null;
                    if (chkTemp.Checked)
                    {
                        IdentifierTemp buf = new IdentifierTemp();
                        buf.VALID_FROM = dtpFrom.Value;
                        buf.VALID_TO = dtpTo.Value;
                        if (!Guid.Empty.Equals(txtAccessGroup.Tag))
                            buf.ACCGROUP_ID = (Guid)txtAccessGroup.Tag;
                        creatingItem = buf;
                    }
                    else if (!Guid.Empty.Equals(txtAccessGroup.Tag))
                    {
                        Identifier buf = new Identifier();
                        buf.ACCGROUP_ID = (Guid)txtAccessGroup.Tag;
                        creatingItem = buf;
                    }
                    else
                        creatingItem = new BaseIdentifier();
                    creatingItem.CODE = txtCode.Text;
                    creatingItem.IS_PRIMARY = chkPrimary.Checked;

                    BaseResult res = integServ.AddPersonIdentifier(_persEditSessionID, creatingItem);
                    if (res.Result != ClientState.Result_Success)
                    {
                        CommonFunctions.ShowErrorMessage(res.ErrorMessage);
                        return;
                    }
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }
                if (_shownIdentifier == null)
                {
                    DialogResult = DialogResult.Cancel;
                    _persEditSessionID = Guid.Empty;
                    Close();
                    return;
                }
                BaseIdentifier savingItem = null;
                if (chkTemp.Checked)
                {
                    IdentifierTemp buf = new IdentifierTemp();
                    buf.VALID_FROM = dtpFrom.Value;
                    buf.VALID_TO = dtpTo.Value;
                    if (!Guid.Empty.Equals(txtAccessGroup.Tag))
                        buf.ACCGROUP_ID = (Guid)txtAccessGroup.Tag;
                    savingItem = buf;
                }
                else if (!Guid.Empty.Equals(txtAccessGroup.Tag))
                {
                    Identifier buf = new Identifier();
                    buf.ACCGROUP_ID = (Guid)txtAccessGroup.Tag;
                    savingItem = buf;
                }
                else
                    savingItem = new BaseIdentifier();

                savingItem.CODE = _shownIdentifier.CODE;
                savingItem.IS_PRIMARY = chkPrimary.Checked;

                BaseResult bRes = integServ.ChangePersonIdentifier(_persEditSessionID, savingItem);
                if (bRes.Result != ClientState.Result_Success)
                {
                    CommonFunctions.ShowErrorMessage(bRes.ErrorMessage);
                    return;
                }
                _persEditSessionID = Guid.Empty;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowError(ex);
            }
        }

        private void ShowAccessGroupsList()
        {
            using (AccessGroupsListForm frm = new AccessGroupsListForm())
            {
                if (frm.ShowDialog(this) != DialogResult.OK)
                    return;
                AccessGroup selectedObject = frm.SelectedAccessGroup;
                if (selectedObject == null || selectedObject.ID.Equals(txtAccessGroup.Tag))
                    return;
                txtAccessGroup.Tag = selectedObject.ID;
                txtAccessGroup.Text = selectedObject.NAME ?? string.Empty;
            }
        }

        private void btnSelectAccessGroup_Click(object sender, EventArgs e)
        {
            ShowAccessGroupsList();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            OnOk();
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtCode = new System.Windows.Forms.TextBox();
			this.dtpFrom = new System.Windows.Forms.DateTimePicker();
			this.txtAccessGroup = new System.Windows.Forms.TextBox();
			this.btnSelectAccessGroup = new System.Windows.Forms.Button();
			this.chkTemp = new System.Windows.Forms.CheckBox();
			this.chkPrimary = new System.Windows.Forms.CheckBox();
			this.dtpTo = new System.Windows.Forms.DateTimePicker();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 5;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label2, 2, 2);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label4, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtCode, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.dtpFrom, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.txtAccessGroup, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.btnSelectAccessGroup, 4, 0);
			this.tableLayoutPanel1.Controls.Add(this.chkTemp, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.chkPrimary, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.dtpTo, 3, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(448, 82);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 58);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(14, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "С";
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(183, 58);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(21, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "По";
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(26, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Код";
			// 
			// label4
			// 
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(183, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(85, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Группа доступа";
			// 
			// txtCode
			// 
			this.txtCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.txtCode.Location = new System.Drawing.Point(35, 4);
			this.txtCode.Name = "txtCode";
			this.txtCode.Size = new System.Drawing.Size(142, 20);
			this.txtCode.TabIndex = 4;
			// 
			// dtpFrom
			// 
			this.dtpFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.dtpFrom.Location = new System.Drawing.Point(35, 55);
			this.dtpFrom.Name = "dtpFrom";
			this.dtpFrom.Size = new System.Drawing.Size(142, 20);
			this.dtpFrom.TabIndex = 5;
			// 
			// txtAccessGroup
			// 
			this.txtAccessGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.txtAccessGroup.Location = new System.Drawing.Point(274, 4);
			this.txtAccessGroup.Name = "txtAccessGroup";
			this.txtAccessGroup.ReadOnly = true;
			this.txtAccessGroup.Size = new System.Drawing.Size(142, 20);
			this.txtAccessGroup.TabIndex = 7;
			// 
			// btnSelectAccessGroup
			// 
			this.btnSelectAccessGroup.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.btnSelectAccessGroup.Image = global::ParsecIntegrationClient.Properties.Resources.folder_find;
			this.btnSelectAccessGroup.Location = new System.Drawing.Point(422, 3);
			this.btnSelectAccessGroup.Name = "btnSelectAccessGroup";
			this.btnSelectAccessGroup.Size = new System.Drawing.Size(23, 23);
			this.btnSelectAccessGroup.TabIndex = 8;
			this.btnSelectAccessGroup.UseVisualStyleBackColor = true;
			this.btnSelectAccessGroup.Click += new System.EventHandler(this.btnSelectAccessGroup_Click);
			// 
			// chkTemp
			// 
			this.chkTemp.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.chkTemp.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.chkTemp, 2);
			this.chkTemp.Location = new System.Drawing.Point(3, 32);
			this.chkTemp.Name = "chkTemp";
			this.chkTemp.Size = new System.Drawing.Size(85, 17);
			this.chkTemp.TabIndex = 9;
			this.chkTemp.Text = "Временный";
			this.chkTemp.UseVisualStyleBackColor = true;
			// 
			// chkPrimary
			// 
			this.chkPrimary.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.chkPrimary.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.chkPrimary, 2);
			this.chkPrimary.Location = new System.Drawing.Point(183, 32);
			this.chkPrimary.Name = "chkPrimary";
			this.chkPrimary.Size = new System.Drawing.Size(83, 17);
			this.chkPrimary.TabIndex = 10;
			this.chkPrimary.Text = "Первичный";
			this.chkPrimary.UseVisualStyleBackColor = true;
			this.chkPrimary.CheckedChanged += new System.EventHandler(this.chkPrimary_CheckedChanged);
			// 
			// dtpTo
			// 
			this.dtpTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.dtpTo.Location = new System.Drawing.Point(274, 55);
			this.dtpTo.Name = "dtpTo";
			this.dtpTo.Size = new System.Drawing.Size(142, 20);
			this.dtpTo.TabIndex = 11;
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(289, 88);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "Сохранить";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(370, 88);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Закрыть";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// IdentifierForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(448, 116);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "IdentifierForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "Идентификатор";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.TextBox txtAccessGroup;
        private System.Windows.Forms.Button btnSelectAccessGroup;
        private System.Windows.Forms.CheckBox chkTemp;
        private System.Windows.Forms.CheckBox chkPrimary;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;

		private void chkPrimary_CheckedChanged(object sender, EventArgs e)
		{

		}
	}
}
