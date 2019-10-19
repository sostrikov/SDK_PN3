using System;
using System.Collections.Generic;
using System.Windows.Forms;

using ParsecIntegrationClient.IntegrationWebService;

namespace ParsecIntegrationClient
{
    public class TerritoryHierarhy : Form
    {
        public TerritoryHierarhy()
        {
            InitializeComponent();
        }

        public Territory SelectedTerritory
        {
            get
            {
                if (tvHierarhy.SelectedNode == null)
                    return null;
                return tvHierarhy.SelectedNode.Tag as Territory;
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
                Territory[] hierarhyList = integServ.GetTerritoriesHierarhy(ClientState.SessionID);
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
                        if (hierarhyList[i] == null || parentsDict.ContainsKey(hierarhyList[i].ID))
                            continue;
                        parentsDict.Add(hierarhyList[i].ID, hierarhyList[i].PARENT_ID);

                        TreeNode resNode = new TreeNode(string.Format("{0}", hierarhyList[i].NAME ?? string.Empty));
                        switch (hierarhyList[i].TYPE)
                        {
                            case 0:
                                resNode.ImageIndex = 0;
                                resNode.SelectedImageIndex = 0;
                                break;
                            case 1:
                                resNode.ImageIndex = 1;
                                resNode.SelectedImageIndex = 1;
                                break;
                            default:
                                resNode.ImageIndex = 2;
                                resNode.SelectedImageIndex = 2;
                                break;
                        }
                        resNode.Tag = hierarhyList[i];
                        nodesDict.Add(hierarhyList[i].ID, resNode);
                    }
                    for (i = 0; i < hierarhyList.Length; i++)
                    {
                        if (hierarhyList[i] == null)
                            continue;
                        Guid nodeID = hierarhyList[i].ID;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TerritoryHierarhy));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.tvHierarhy = new System.Windows.Forms.TreeView();
            this.ilSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.ilLargeIcons = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(533, 462);
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
            this.btnOk.Location = new System.Drawing.Point(452, 462);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Выбрать";
            this.btnOk.UseVisualStyleBackColor = true;
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
            this.tvHierarhy.Size = new System.Drawing.Size(608, 456);
            this.tvHierarhy.TabIndex = 5;
            // 
            // ilSmallIcons
            // 
            this.ilSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSmallIcons.ImageStream")));
            this.ilSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilSmallIcons.Images.SetKeyName(0, "folder.png");
            this.ilSmallIcons.Images.SetKeyName(1, "cpu.png");
            this.ilSmallIcons.Images.SetKeyName(2, "unknown.png");
            // 
            // ilLargeIcons
            // 
            this.ilLargeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilLargeIcons.ImageStream")));
            this.ilLargeIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilLargeIcons.Images.SetKeyName(0, "folder.png");
            this.ilLargeIcons.Images.SetKeyName(1, "cpu.png");
            this.ilLargeIcons.Images.SetKeyName(2, "unknown.png");
            // 
            // TerritoryHierarhy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 487);
            this.Controls.Add(this.tvHierarhy);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TerritoryHierarhy";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Иерархия территории";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TreeView tvHierarhy;
        private System.Windows.Forms.ImageList ilSmallIcons;
        private System.Windows.Forms.ImageList ilLargeIcons;
    }
}
