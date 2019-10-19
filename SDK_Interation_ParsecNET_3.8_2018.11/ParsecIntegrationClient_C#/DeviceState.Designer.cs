namespace ParsecIntegrationClient
{
	partial class DeviceState
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose ( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			this.label2 = new System.Windows.Forms.Label();
			this.txtTerritory = new System.Windows.Forms.TextBox();
			this.btnSelectTerritory = new System.Windows.Forms.Button();
			this.btnRequestState = new System.Windows.Forms.Button();
			this.listState = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.label1 = new System.Windows.Forms.Label();
			this.labelState = new System.Windows.Forms.Label();
			this.comboCommand = new System.Windows.Forms.ComboBox();
			this.btnSendCommand = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(67, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Территория";
			// 
			// txtTerritory
			// 
			this.txtTerritory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtTerritory.Location = new System.Drawing.Point(85, 9);
			this.txtTerritory.Name = "txtTerritory";
			this.txtTerritory.ReadOnly = true;
			this.txtTerritory.Size = new System.Drawing.Size(248, 20);
			this.txtTerritory.TabIndex = 7;
			this.txtTerritory.Text = "<нет>";
			// 
			// btnSelectTerritory
			// 
			this.btnSelectTerritory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSelectTerritory.Image = global::ParsecIntegrationClient.Properties.Resources.folder_find;
			this.btnSelectTerritory.Location = new System.Drawing.Point(339, 7);
			this.btnSelectTerritory.Name = "btnSelectTerritory";
			this.btnSelectTerritory.Size = new System.Drawing.Size(29, 23);
			this.btnSelectTerritory.TabIndex = 8;
			this.btnSelectTerritory.UseVisualStyleBackColor = true;
			this.btnSelectTerritory.Click += new System.EventHandler(this.btnSelectTerritory_Click);
			// 
			// btnRequestState
			// 
			this.btnRequestState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRequestState.Location = new System.Drawing.Point(229, 70);
			this.btnRequestState.Name = "btnRequestState";
			this.btnRequestState.Size = new System.Drawing.Size(139, 23);
			this.btnRequestState.TabIndex = 9;
			this.btnRequestState.Text = "Получить состояние";
			this.btnRequestState.UseVisualStyleBackColor = true;
			this.btnRequestState.Click += new System.EventHandler(this.btnRequestState_Click);
			// 
			// listState
			// 
			this.listState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listState.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.listState.FullRowSelect = true;
			this.listState.Location = new System.Drawing.Point(15, 99);
			this.listState.Name = "listState";
			this.listState.Size = new System.Drawing.Size(353, 339);
			this.listState.TabIndex = 10;
			this.listState.UseCompatibleStateImageBehavior = false;
			this.listState.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Компонент";
			this.columnHeader1.Width = 160;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Состояние";
			this.columnHeader2.Width = 160;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 75);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(111, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "Текущее состояние:";
			// 
			// labelState
			// 
			this.labelState.AutoSize = true;
			this.labelState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelState.Location = new System.Drawing.Point(129, 75);
			this.labelState.Name = "labelState";
			this.labelState.Size = new System.Drawing.Size(14, 13);
			this.labelState.TabIndex = 12;
			this.labelState.Text = "0";
			// 
			// comboCommand
			// 
			this.comboCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboCommand.FormattingEnabled = true;
			this.comboCommand.Location = new System.Drawing.Point(15, 43);
			this.comboCommand.Name = "comboCommand";
			this.comboCommand.Size = new System.Drawing.Size(207, 21);
			this.comboCommand.TabIndex = 13;
			// 
			// btnSendCommand
			// 
			this.btnSendCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSendCommand.Location = new System.Drawing.Point(229, 41);
			this.btnSendCommand.Name = "btnSendCommand";
			this.btnSendCommand.Size = new System.Drawing.Size(139, 23);
			this.btnSendCommand.TabIndex = 14;
			this.btnSendCommand.Text = "Послать команду";
			this.btnSendCommand.UseVisualStyleBackColor = true;
			this.btnSendCommand.Click += new System.EventHandler(this.btnSendCommand_Click);
			// 
			// DeviceState
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(380, 450);
			this.Controls.Add(this.btnSendCommand);
			this.Controls.Add(this.comboCommand);
			this.Controls.Add(this.labelState);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listState);
			this.Controls.Add(this.btnRequestState);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtTerritory);
			this.Controls.Add(this.btnSelectTerritory);
			this.Name = "DeviceState";
			this.Text = "DeviceState";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtTerritory;
		private System.Windows.Forms.Button btnSelectTerritory;
		private System.Windows.Forms.Button btnRequestState;
		private System.Windows.Forms.ListView listState;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelState;
		private System.Windows.Forms.ComboBox comboCommand;
		private System.Windows.Forms.Button btnSendCommand;
	}
}