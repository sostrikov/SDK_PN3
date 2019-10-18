using System;
using System.Collections.Generic;
using System.Windows.Forms;

using ParsecIntegrationClient.IntegrationWebService;

namespace ParsecIntegrationClient
{
    public class PersonHierarhy : Form
    {
        public PersonHierarhy()
        {
            InitializeComponent();
        }

        public BaseObject SelectedNode
        {
            get
            {
                if (tvHierarhy.SelectedNode == null)
                    return null;
                return tvHierarhy.SelectedNode.Tag as BaseObject;
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            ShowHierarhy();
        }

        private void ShowHierarhy()
        {
            try
            {
                IntegrationService integServ = new IntegrationService();
                BaseObject[] hierarhyList = integServ.GetOrgUnitsHierarhyWithPersons(ClientState.SessionID);
                try
                {
                    tvHierarhy.BeginUpdate();
                    tvHierarhy.Nodes.Clear();
                    if (hierarhyList == null || hierarhyList.Length == 0)
                        return;
                    IDictionary<Guid, Guid> parentsDict = new Dictionary<Guid, Guid>();
                    IDictionary<Guid, TreeNode> nodesDict = new Dictionary<Guid, TreeNode>();
                    // create nodes
                    int i = 0;
                    for (i = hierarhyList.Length - 1; i >= 0; i--)
                    {
                        if (hierarhyList[i] == null)
                            continue;
                        Person personItem = hierarhyList[i] as Person;
                        TreeNode resNode = null;
                        if (personItem != null)
                        {
                            // create persons node
                            if (parentsDict.ContainsKey(personItem.ID))
                                continue;
                            parentsDict.Add(personItem.ID, personItem.ORG_ID);
                            resNode = new TreeNode(string.Format("{0} {1} {2}", personItem.LAST_NAME ?? string.Empty, personItem.FIRST_NAME ?? string.Empty, personItem.MIDDLE_NAME ?? string.Empty));
                            resNode.ImageIndex = 1;
                            resNode.SelectedImageIndex = 1;
                            resNode.Tag = personItem;
                            nodesDict.Add(personItem.ID, resNode);
                            continue;
                        }
                        OrgUnit orgUnitItem = hierarhyList[i] as OrgUnit;
                        if (orgUnitItem == null || parentsDict.ContainsKey(orgUnitItem.ID))
                            continue;
                        // create organization units node
                        parentsDict.Add(orgUnitItem.ID, orgUnitItem.PARENT_ID);
                        resNode = new TreeNode(string.Format("{0}", orgUnitItem.NAME ?? string.Empty));
                        resNode.ImageIndex = 0;
                        resNode.SelectedImageIndex = 0;
                        resNode.Tag = orgUnitItem;
                        nodesDict.Add(orgUnitItem.ID, resNode);
                    }
                    for (i = 0; i < hierarhyList.Length; i++)
                    {
                        if (hierarhyList[i] == null)
                            continue;
                        Guid nodeID = Guid.Empty;
                        if (hierarhyList[i] is Person)
                            nodeID = ((Person)hierarhyList[i]).ID;
                        else if (hierarhyList[i] is OrgUnit)
                            nodeID = ((OrgUnit)hierarhyList[i]).ID;
                        else
                            continue;
                        Guid parentID = parentsDict[nodeID];
                        if (parentID.Equals(nodeID) || !parentsDict.ContainsKey(parentID))
                        {
                            // root node
                            tvHierarhy.Nodes.Add(nodesDict[nodeID]);
                            continue;
                        }
                        nodesDict[parentID].Nodes.Add(nodesDict[nodeID]);
                    }
                }
                finally
                {
                    tvHierarhy.EndUpdate();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersonHierarhy));
            this.tvHierarhy = new System.Windows.Forms.TreeView();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ilSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.ilLargeIcons = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // tvHierarhy
            // 
            this.tvHierarhy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvHierarhy.HideSelection = false;
            this.tvHierarhy.ImageIndex = 0;
            this.tvHierarhy.ImageList = this.ilSmallIcons;
            this.tvHierarhy.Location = new System.Drawing.Point(0, 0);
            this.tvHierarhy.Name = "tvHierarhy";
            this.tvHierarhy.SelectedImageIndex = 0;
            this.tvHierarhy.Size = new System.Drawing.Size(608, 464);
            this.tvHierarhy.TabIndex = 0;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(440, 470);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Выбрать";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(521, 470);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Закрыть";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // ilSmallIcons
            // 
            this.ilSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSmallIcons.ImageStream")));
            this.ilSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilSmallIcons.Images.SetKeyName(0, "folder.png");
            this.ilSmallIcons.Images.SetKeyName(1, "user.png");
            // 
            // ilLargeIcons
            // 
            this.ilLargeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilLargeIcons.ImageStream")));
            this.ilLargeIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilLargeIcons.Images.SetKeyName(0, "folder.png");
            this.ilLargeIcons.Images.SetKeyName(1, "user.png");
            // 
            // PersonHierarhy
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(608, 496);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.tvHierarhy);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PersonHierarhy";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Иерархия персонала";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvHierarhy;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ImageList ilSmallIcons;
        private System.Windows.Forms.ImageList ilLargeIcons;
    }
}
