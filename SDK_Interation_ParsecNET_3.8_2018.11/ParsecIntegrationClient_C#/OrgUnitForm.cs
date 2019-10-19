using System;
using System.Windows.Forms;

using ParsecIntegrationClient.IntegrationWebService;

namespace ParsecIntegrationClient
{
    public class OrgUnitForm : Form
    {
        private Guid _shownOrgUnitID = Guid.Empty;
        private Guid _editSessionID = Guid.Empty;
        private OrgUnit _shownOrgUnit = null;
        private bool _creation = false;

        public OrgUnitForm()
        {
            InitializeComponent();
            txtParent.Tag = Guid.Empty;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            ShowOrgUnitDetails();
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
                integServ.CloseOrgUnitEditingSession(_editSessionID);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowError(ex);
            }
        }

        public void SetOrgUnit(Guid orgUnitID)
        {
            _shownOrgUnitID = orgUnitID;
            _shownOrgUnit = null;
            _editSessionID = Guid.Empty;
            _creation = false;
            btnOk.Visible = false;
            btnEdit.Visible = true;
        }

        public void CreateOrgUnit(Guid parentOrgUnitID)
        {
            _shownOrgUnitID = Guid.Empty;
            _shownOrgUnit = null;
            _editSessionID = Guid.Empty;
            _creation = true;
            btnOk.Visible = true;
            btnEdit.Visible = false;
            ClearDetails();
            txtParent.Tag = parentOrgUnitID;
            txtParent.Text = ClientState.GetOrgUnitName(parentOrgUnitID) ?? string.Empty;
        }

        private void OnEdit()
        {
            // start new editing session
            try
            {
                IntegrationService integServ = new IntegrationService();
                GuidResult res = integServ.OpenOrgUnitEditingSession(ClientState.SessionID, _shownOrgUnitID);
                if (res.Result != ClientState.Result_Success)
                {
                    CommonFunctions.ShowErrorMessage(res.ErrorMessage);
                    return;
                }
                _editSessionID = res.Value;
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
            try
            {
                // try to save orgUnit
                IntegrationService integServ = new IntegrationService();
                if (_creation)
                {
                    OrgUnit savingOrgUnit = new OrgUnit();
                    savingOrgUnit.NAME = txtName.Text;
                    savingOrgUnit.DESC = txtDesc.Text;
                    savingOrgUnit.PARENT_ID = (Guid)txtParent.Tag;
                    GuidResult res = integServ.CreateOrgUnit(ClientState.SessionID, savingOrgUnit);
                    if (res.Result != ClientState.Result_Success)
                    {
                        CommonFunctions.ShowErrorMessage(res.ErrorMessage);
                        return;
                    }
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }
                if (_shownOrgUnit == null)
                {
                    DialogResult = DialogResult.Cancel;
                    _editSessionID = Guid.Empty;
                    Close();
                    return;
                }
                BaseOrgUnit savingRes = null;
                if (_shownOrgUnit.PARENT_ID.Equals((Guid)txtParent.Tag))
                {
                    savingRes = new BaseOrgUnit();
                    savingRes.ID = _shownOrgUnit.ID;
                }
                else
                {
                    savingRes = _shownOrgUnit;
                    _shownOrgUnit.PARENT_ID = (Guid)txtParent.Tag;
                }
                savingRes.NAME = txtName.Text;
                savingRes.DESC = txtDesc.Text;
                BaseResult bRes = integServ.SaveOrgUnit(_editSessionID, savingRes);
                if (bRes.Result != ClientState.Result_Success)
                {
                    CommonFunctions.ShowErrorMessage(bRes.ErrorMessage);
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

        private void ShowOrgUnitDetails()
        {
            try
            {
                if (_shownOrgUnitID.Equals(Guid.Empty))
                    return;
                ClearDetails();
                IntegrationService integServ = new IntegrationService();
                OrgUnit orgUnitItem = integServ.GetOrgUnit(ClientState.SessionID, _shownOrgUnitID);
                if (orgUnitItem == null)
                    return;
                _shownOrgUnit = orgUnitItem;
                txtName.Text = orgUnitItem.NAME ?? string.Empty;
                txtDesc.Text = orgUnitItem.DESC ?? string.Empty;
                txtParent.Tag = orgUnitItem.PARENT_ID;
                if (orgUnitItem.ID.Equals(orgUnitItem.PARENT_ID))
                    txtParent.Text = orgUnitItem.NAME ?? string.Empty;
                else if (Guid.Empty.Equals(orgUnitItem.PARENT_ID))
                    txtParent.Text = string.Empty;
                else
                    // get parent string
                    txtParent.Text = ClientState.GetOrgUnitName(orgUnitItem.PARENT_ID) ?? string.Empty;
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowError(ex);
            }
        }

        private void ClearDetails()
        {
            txtName.Text = string.Empty;
            txtDesc.Text = string.Empty;
            txtParent.Tag = Guid.Empty;
            txtParent.Text = string.Empty;
        }

        private void ShowOrgUnitHierarhy()
        {
            using (OrgUnitHierarhyForm frm = new OrgUnitHierarhyForm())
            {
                if (frm.ShowDialog(this) != DialogResult.OK)
                    return;
                OrgUnit selectedObject = frm.SelectedOrgUnit;
                if (selectedObject == null)
                    return;
                if (!Guid.Empty.Equals(_shownOrgUnitID))
                {
                    // new parent could not be one of the child
                    Guid parentID = selectedObject.ID;
                    while (!Guid.Empty.Equals(parentID) && !_shownOrgUnitID.Equals(parentID) && !parentID.Equals(ClientState.GetParentOrgUnit(parentID)))
                        parentID = ClientState.GetParentOrgUnit(parentID);
                    if (_shownOrgUnitID.Equals(parentID))
                        return;
                }
                txtParent.Tag = selectedObject.ID;
                txtParent.Text = string.Format("{0}", selectedObject.NAME);
            }
        }

        private void btnSelectParent_Click(object sender, EventArgs e)
        {
            ShowOrgUnitHierarhy();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            OnOk();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            OnEdit();
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.txtParent = new System.Windows.Forms.TextBox();
            this.btnSelectParent = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtDesc, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtParent, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnSelectParent, 2, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(310, 85);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Название";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Описание";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Поразделение";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txtName, 2);
            this.txtName.Location = new System.Drawing.Point(90, 3);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(217, 20);
            this.txtName.TabIndex = 3;
            // 
            // txtDesc
            // 
            this.txtDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.txtDesc, 2);
            this.txtDesc.Location = new System.Drawing.Point(90, 29);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(217, 20);
            this.txtDesc.TabIndex = 4;
            // 
            // txtParent
            // 
            this.txtParent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParent.Location = new System.Drawing.Point(90, 56);
            this.txtParent.Name = "txtParent";
            this.txtParent.ReadOnly = true;
            this.txtParent.Size = new System.Drawing.Size(188, 20);
            this.txtParent.TabIndex = 5;
            // 
            // btnSelectParent
            // 
            this.btnSelectParent.Image = global::ParsecIntegrationClient.Properties.Resources.folder_find;
            this.btnSelectParent.Location = new System.Drawing.Point(284, 55);
            this.btnSelectParent.Name = "btnSelectParent";
            this.btnSelectParent.Size = new System.Drawing.Size(23, 23);
            this.btnSelectParent.TabIndex = 6;
            this.btnSelectParent.UseVisualStyleBackColor = true;
            this.btnSelectParent.Click += new System.EventHandler(this.btnSelectParent_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(232, 91);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Закрыть";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(151, 91);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Сохранить";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(132, 91);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(94, 23);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Редактировать";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // OrgUnitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(310, 121);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrgUnitForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Подразделение";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.TextBox txtParent;
        private System.Windows.Forms.Button btnSelectParent;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnEdit;
    }
}
