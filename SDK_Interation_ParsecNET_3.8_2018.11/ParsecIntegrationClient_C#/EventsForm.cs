using System;
using System.Windows.Forms;

using ParsecIntegrationClient.IntegrationWebService;
using System.Collections.Generic;
using System.Globalization;

namespace ParsecIntegrationClient
{
    public class EventsForm : Form
    {
        private string _initialFormText = string.Empty;
        private Guid _personNode = Guid.Empty;
        private int _personType;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel statusBar;
        private Guid _territoryNode = Guid.Empty;

        public EventsForm()
        {
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            _initialFormText = Text;
        }

        private void ShowPersonHierarhy()
        {
            using (PersonHierarhy frm = new PersonHierarhy())
            {
                if (frm.ShowDialog(this) != DialogResult.OK)
                    return;
                BaseObject selectedObject = frm.SelectedNode;
                if (selectedObject == null)
                    return;
                Person personItem = selectedObject as Person;
                if (personItem != null)
                {
                    _personNode = personItem.ID;
                    _personType = 0; // person
                    txtPerson.Text = string.Format("{0} {1} {2}", personItem.LAST_NAME, personItem.FIRST_NAME, personItem.MIDDLE_NAME);
                    return;
                }
                OrgUnit orgUnitItem = selectedObject as OrgUnit;
                if (orgUnitItem == null)
                    return;
                _personNode = orgUnitItem.ID;
                _personType = 1; // organization
                txtPerson.Text = string.Format("{0}", orgUnitItem.NAME);
            }
        }

        private void ShowTerritoryHierarhy()
        {
            using (TerritoryHierarhy frm = new TerritoryHierarhy())
            {
                if (frm.ShowDialog(this) != DialogResult.OK)
                    return;
                Territory selectedObject = frm.SelectedTerritory;
                if (selectedObject == null)
                    return;
                _territoryNode = selectedObject.ID;
                txtTerritory.Text = string.Format("{0}", selectedObject.NAME);
            }
        }

        private void btnSelectPersonElement_Click(object sender, EventArgs e)
        {
            ShowPersonHierarhy();
        }

        private void btnSelectTerritory_Click(object sender, EventArgs e)
        {
            ShowTerritoryHierarhy();
        }

        private void btnGetEvents_Click(object sender, EventArgs e)
        {
            try
            {
                IntegrationService integServ = new IntegrationService();

                statusBar.Text = "Вычисление параметров...";
                EventHistoryQueryParams param = new EventHistoryQueryParams();
                param.StartDate = dtpFrom.Value.Date.ToUniversalTime();
                param.EndDate = dtpTo.Value.Date.AddDays(1).ToUniversalTime();
                param.Territories = _territoryNode != Guid.Empty ? new Guid[] { _territoryNode } : null;
                if (_personNode != Guid.Empty)
                {
                    if (_personType == 0)
                        param.Users = new Guid[] { _personNode };
                    else
                        param.Organizations = new Guid[] { _personNode };
                }

                param.EventsWithoutUser = false;
                param.TransactionTypes = new uint[] { 590144, 590152, 65867, 590165, 590145, 590153, 65866, 590166 };
                param.MaxResultSize = (10000);

                statusBar.Text = "Открытие сессии для получения событий...";
                Application.DoEvents();
                GuidResult res = integServ.OpenEventHistorySession(ClientState.SessionID, param);
                if (res.Result != ClientState.Result_Success)
                {
                    MessageBox.Show(res.ErrorMessage);
                    return;
                }
                Guid eventSessionID = res.Value;

                int _count = integServ.GetEventHistoryResultCount(ClientState.SessionID, eventSessionID);
                
                EventHistoryFields constants = new EventHistoryFields();
                List<Guid> fields = new List<Guid>();
                fields.Add(EventHistoryFields.EVENT_DATE_TIME); //0
                fields.Add(EventHistoryFields.USER_FULL_NAME);  //1
                fields.Add(EventHistoryFields.IDENTIFIER);      //2
                fields.Add(EventHistoryFields.EVENT_AREA);      //3
                fields.Add(EventHistoryFields.EVENT_CODE_HEX);  //4
                fields.Add(EventHistoryFields.EVENT_DESCRIPTION); //5
                // EventHistoryFields это константы. (список приведён ниже, см. п.5).

                // show events in the list
                try
                {
                    statusBar.Text = "Получение и вывод данных...";
                    Application.DoEvents();

                    lvEvents.BeginUpdate();
                    lvEvents.Items.Clear();


                    int nRetrive = 0;
                    for (int chunck = 0; chunck < _count; chunck += nRetrive)
                    {
                        EventObject[] events = null;
                        nRetrive = Math.Max(Math.Min(_count / 10, 150), Math.Min(50, _count));
                        nRetrive = chunck + nRetrive <= _count ? nRetrive : _count - chunck;
                        events = integServ.GetEventHistoryResult(ClientState.SessionID, eventSessionID, fields.ToArray(), chunck, nRetrive);

                        for (int i = 0; i < events.Length; i++)
                        {
                            ListViewItem lvItem = new ListViewItem(events[i].Values[0].ToString());
                            lvItem.SubItems.Add((string)events[i].Values[5]);

                            switch ((string)events[i].Values[4])
                            {
                                case "90140":
                                case "90148":
                                case "1014B":
                                case "90155":
                                    lvItem.ImageIndex = 0;
                                break;
                                case "90141":
                                case "90149":
                                case "90142":
                                case "901A4":
                                case "90156":
                                case "901A5":
                                    lvItem.ImageIndex = 1;
                                break;
                                default:
                                    lvItem.ImageIndex = 2;
                                    break;
                            }


                            lvItem.SubItems.Add(events[i].Values[1] != null ? events[i].Values[1].ToString() : "");
                            lvItem.SubItems.Add(events[i].Values[2] != null ? events[i].Values[2].ToString() : "");
                            lvItem.SubItems.Add(events[i].Values[3] != null ? events[i].Values[3].ToString() : "");
                            lvEvents.Items.Add(lvItem);
                        }



                        if (res.Result != ClientState.Result_Success)
                        {
                            CommonFunctions.ShowErrorMessage(res.ErrorMessage);
                            return;
                        }
                    }

                    for (int i = lvEvents.Columns.Count - 1; i >= 0; i--)
                        lvEvents.Columns[i].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

                }
                finally
                {
                    lvEvents.EndUpdate();
                    statusBar.Text = string.Format("{0} - в списке {1} элементов", _initialFormText ?? string.Empty, lvEvents.Items.Count);
                }

                integServ.CloseEventHistorySession(ClientState.SessionID, eventSessionID);
            }
            catch (Exception ex)
            {
                CommonFunctions.ShowError(ex);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EventsForm));
            this.gpbParams = new System.Windows.Forms.GroupBox();
            this.btnGetEvents = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPerson = new System.Windows.Forms.TextBox();
            this.txtTerritory = new System.Windows.Forms.TextBox();
            this.btnSelectPersonElement = new System.Windows.Forms.Button();
            this.btnSelectTerritory = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lvEvents = new System.Windows.Forms.ListView();
            this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPerson = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIdentifier = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTerritory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ilLargeIcons = new System.Windows.Forms.ImageList(this.components);
            this.ilSmallIcons = new System.Windows.Forms.ImageList(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.gpbParams.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpbParams
            // 
            this.gpbParams.Controls.Add(this.btnGetEvents);
            this.gpbParams.Controls.Add(this.tableLayoutPanel1);
            this.gpbParams.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpbParams.Location = new System.Drawing.Point(0, 0);
            this.gpbParams.Name = "gpbParams";
            this.gpbParams.Size = new System.Drawing.Size(700, 112);
            this.gpbParams.TabIndex = 0;
            this.gpbParams.TabStop = false;
            this.gpbParams.Text = "Параметры";
            // 
            // btnGetEvents
            // 
            this.btnGetEvents.Location = new System.Drawing.Point(6, 86);
            this.btnGetEvents.Name = "btnGetEvents";
            this.btnGetEvents.Size = new System.Drawing.Size(112, 23);
            this.btnGetEvents.TabIndex = 1;
            this.btnGetEvents.Text = "Получить события";
            this.btnGetEvents.UseVisualStyleBackColor = true;
            this.btnGetEvents.Click += new System.EventHandler(this.btnGetEvents_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtPerson, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtTerritory, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSelectPersonElement, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSelectTerritory, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.dtpFrom, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpTo, 4, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(694, 64);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Персонал";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Территория";
            // 
            // txtPerson
            // 
            this.txtPerson.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPerson.Location = new System.Drawing.Point(76, 4);
            this.txtPerson.Name = "txtPerson";
            this.txtPerson.ReadOnly = true;
            this.txtPerson.Size = new System.Drawing.Size(276, 20);
            this.txtPerson.TabIndex = 2;
            this.txtPerson.Text = "Все";
            // 
            // txtTerritory
            // 
            this.txtTerritory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTerritory.Location = new System.Drawing.Point(76, 33);
            this.txtTerritory.Name = "txtTerritory";
            this.txtTerritory.ReadOnly = true;
            this.txtTerritory.Size = new System.Drawing.Size(276, 20);
            this.txtTerritory.TabIndex = 3;
            this.txtTerritory.Text = "Все";
            // 
            // btnSelectPersonElement
            // 
            this.btnSelectPersonElement.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSelectPersonElement.Image = global::ParsecIntegrationClient.Properties.Resources.folder_find;
            this.btnSelectPersonElement.Location = new System.Drawing.Point(358, 3);
            this.btnSelectPersonElement.Name = "btnSelectPersonElement";
            this.btnSelectPersonElement.Size = new System.Drawing.Size(23, 23);
            this.btnSelectPersonElement.TabIndex = 4;
            this.btnSelectPersonElement.UseVisualStyleBackColor = true;
            this.btnSelectPersonElement.Click += new System.EventHandler(this.btnSelectPersonElement_Click);
            // 
            // btnSelectTerritory
            // 
            this.btnSelectTerritory.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSelectTerritory.Image = global::ParsecIntegrationClient.Properties.Resources.folder_find;
            this.btnSelectTerritory.Location = new System.Drawing.Point(358, 32);
            this.btnSelectTerritory.Name = "btnSelectTerritory";
            this.btnSelectTerritory.Size = new System.Drawing.Size(23, 23);
            this.btnSelectTerritory.TabIndex = 5;
            this.btnSelectTerritory.UseVisualStyleBackColor = true;
            this.btnSelectTerritory.Click += new System.EventHandler(this.btnSelectTerritory_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(387, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "С";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(387, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "По";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpFrom.Location = new System.Drawing.Point(414, 4);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(277, 20);
            this.dtpFrom.TabIndex = 8;
            // 
            // dtpTo
            // 
            this.dtpTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpTo.Location = new System.Drawing.Point(414, 33);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(277, 20);
            this.dtpTo.TabIndex = 9;
            // 
            // lvEvents
            // 
            this.lvEvents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvEvents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDate,
            this.colType,
            this.colPerson,
            this.colIdentifier,
            this.colTerritory});
            this.lvEvents.FullRowSelect = true;
            this.lvEvents.GridLines = true;
            this.lvEvents.LargeImageList = this.ilLargeIcons;
            this.lvEvents.Location = new System.Drawing.Point(0, 112);
            this.lvEvents.Name = "lvEvents";
            this.lvEvents.Size = new System.Drawing.Size(700, 361);
            this.lvEvents.SmallImageList = this.ilSmallIcons;
            this.lvEvents.TabIndex = 1;
            this.lvEvents.UseCompatibleStateImageBehavior = false;
            this.lvEvents.View = System.Windows.Forms.View.Details;
            // 
            // colDate
            // 
            this.colDate.Text = "Дата";
            // 
            // colType
            // 
            this.colType.Text = "Тип";
            this.colType.Width = 74;
            // 
            // colPerson
            // 
            this.colPerson.Text = "Сотрудник";
            // 
            // colIdentifier
            // 
            this.colIdentifier.Text = "Код";
            // 
            // colTerritory
            // 
            this.colTerritory.Text = "Территория";
            // 
            // ilLargeIcons
            // 
            this.ilLargeIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilLargeIcons.ImageStream")));
            this.ilLargeIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilLargeIcons.Images.SetKeyName(0, "arrow_left_green.png");
            this.ilLargeIcons.Images.SetKeyName(1, "arrow_right_blue.png");
            this.ilLargeIcons.Images.SetKeyName(2, "unknown.png");
            // 
            // ilSmallIcons
            // 
            this.ilSmallIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilSmallIcons.ImageStream")));
            this.ilSmallIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilSmallIcons.Images.SetKeyName(0, "arrow_left_green.png");
            this.ilSmallIcons.Images.SetKeyName(1, "arrow_right_blue.png");
            this.ilSmallIcons.Images.SetKeyName(2, "unknown.png");
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 476);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(700, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusBar
            // 
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(0, 17);
            // 
            // EventsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 498);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.lvEvents);
            this.Controls.Add(this.gpbParams);
            this.MinimizeBox = false;
            this.Name = "EventsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "События";
            this.gpbParams.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gpbParams;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPerson;
        private System.Windows.Forms.TextBox txtTerritory;
        private System.Windows.Forms.Button btnSelectPersonElement;
        private System.Windows.Forms.Button btnSelectTerritory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.ListView lvEvents;
        private System.Windows.Forms.ColumnHeader colDate;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colPerson;
        private System.Windows.Forms.ColumnHeader colIdentifier;
        private System.Windows.Forms.ColumnHeader colTerritory;
        private System.Windows.Forms.Button btnGetEvents;
        private System.Windows.Forms.ImageList ilSmallIcons;
        private System.Windows.Forms.ImageList ilLargeIcons;
    }
}
