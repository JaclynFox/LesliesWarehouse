namespace LesliesWarehouse
{
    partial class FrmAdmin
    {
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
            this.ButtonAdminRefresh = new System.Windows.Forms.Button();
            this.ButtonAdminReport = new System.Windows.Forms.Button();
            this.ListViewEmployeeTime = new System.Windows.Forms.ListView();
            this.colUserID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPunchDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPunchTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPunchType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LabelAdminName = new System.Windows.Forms.Label();
            this.LogoutBtn = new System.Windows.Forms.Button();
            this.DateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.ButtonEditPunch = new System.Windows.Forms.Button();
            this.TextBoxEmployeeId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.colFlag = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // ButtonAdminRefresh
            // 
            this.ButtonAdminRefresh.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ButtonAdminRefresh.Location = new System.Drawing.Point(241, 65);
            this.ButtonAdminRefresh.Name = "ButtonAdminRefresh";
            this.ButtonAdminRefresh.Size = new System.Drawing.Size(83, 20);
            this.ButtonAdminRefresh.TabIndex = 2;
            this.ButtonAdminRefresh.Text = "Refresh";
            this.ButtonAdminRefresh.UseVisualStyleBackColor = true;
            this.ButtonAdminRefresh.Click += new System.EventHandler(this.ButtonAdminRefresh_Click);
            // 
            // ButtonAdminReport
            // 
            this.ButtonAdminReport.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ButtonAdminReport.Location = new System.Drawing.Point(1202, 713);
            this.ButtonAdminReport.Name = "ButtonAdminReport";
            this.ButtonAdminReport.Size = new System.Drawing.Size(104, 28);
            this.ButtonAdminReport.TabIndex = 3;
            this.ButtonAdminReport.Text = "Export To CSV";
            this.ButtonAdminReport.UseVisualStyleBackColor = true;
            // 
            // ListViewEmployeeTime
            // 
            this.ListViewEmployeeTime.BackColor = System.Drawing.Color.White;
            this.ListViewEmployeeTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListViewEmployeeTime.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colUserID,
            this.colPunchDate,
            this.colPunchTime,
            this.colPunchType,
            this.colFlag});
            this.ListViewEmployeeTime.FullRowSelect = true;
            this.ListViewEmployeeTime.HideSelection = false;
            this.ListViewEmployeeTime.Location = new System.Drawing.Point(12, 91);
            this.ListViewEmployeeTime.Name = "ListViewEmployeeTime";
            this.ListViewEmployeeTime.Size = new System.Drawing.Size(618, 514);
            this.ListViewEmployeeTime.TabIndex = 5;
            this.ListViewEmployeeTime.UseCompatibleStateImageBehavior = false;
            this.ListViewEmployeeTime.View = System.Windows.Forms.View.Details;
            this.ListViewEmployeeTime.SelectedIndexChanged += new System.EventHandler(this.ListViewEmployeeTime_SelectedIndexChanged);
            // 
            // colUserID
            // 
            this.colUserID.Text = "User ID";
            this.colUserID.Width = 185;
            // 
            // colPunchDate
            // 
            this.colPunchDate.Text = "Punch Date";
            this.colPunchDate.Width = 137;
            // 
            // colPunchTime
            // 
            this.colPunchTime.Text = "Punch Time";
            this.colPunchTime.Width = 131;
            // 
            // colPunchType
            // 
            this.colPunchType.Text = "Punch Type";
            this.colPunchType.Width = 92;
            // 
            // LabelAdminName
            // 
            this.LabelAdminName.AutoSize = true;
            this.LabelAdminName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.LabelAdminName.Location = new System.Drawing.Point(396, 27);
            this.LabelAdminName.Name = "LabelAdminName";
            this.LabelAdminName.Size = new System.Drawing.Size(92, 13);
            this.LabelAdminName.TabIndex = 8;
            this.LabelAdminName.Text = "Welcome {Admin}";
            // 
            // LogoutBtn
            // 
            this.LogoutBtn.BackColor = System.Drawing.Color.Red;
            this.LogoutBtn.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.LogoutBtn.Location = new System.Drawing.Point(1248, 38);
            this.LogoutBtn.Name = "LogoutBtn";
            this.LogoutBtn.Size = new System.Drawing.Size(75, 23);
            this.LogoutBtn.TabIndex = 10;
            this.LogoutBtn.Text = "Log Out";
            this.LogoutBtn.UseVisualStyleBackColor = false;
            this.LogoutBtn.Click += new System.EventHandler(this.LogoutBtn_Click);
            // 
            // DateTimePicker
            // 
            this.DateTimePicker.Location = new System.Drawing.Point(12, 65);
            this.DateTimePicker.Name = "DateTimePicker";
            this.DateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.DateTimePicker.TabIndex = 13;
            // 
            // ButtonEditPunch
            // 
            this.ButtonEditPunch.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ButtonEditPunch.Location = new System.Drawing.Point(755, 276);
            this.ButtonEditPunch.Name = "ButtonEditPunch";
            this.ButtonEditPunch.Size = new System.Drawing.Size(75, 23);
            this.ButtonEditPunch.TabIndex = 38;
            this.ButtonEditPunch.Text = "Edit Punch";
            this.ButtonEditPunch.UseVisualStyleBackColor = true;
            this.ButtonEditPunch.Click += new System.EventHandler(this.ButtonEditPunch_Click);
            // 
            // TextBoxEmployeeId
            // 
            this.TextBoxEmployeeId.Location = new System.Drawing.Point(664, 144);
            this.TextBoxEmployeeId.Name = "TextBoxEmployeeId";
            this.TextBoxEmployeeId.ReadOnly = true;
            this.TextBoxEmployeeId.Size = new System.Drawing.Size(255, 20);
            this.TextBoxEmployeeId.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label4.Location = new System.Drawing.Point(769, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Last Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label2.Location = new System.Drawing.Point(763, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Employee ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label1.Location = new System.Drawing.Point(763, 167);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Punch Date";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(664, 183);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(255, 20);
            this.dateTimePicker1.TabIndex = 39;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dateTimePicker2.Location = new System.Drawing.Point(665, 222);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(254, 20);
            this.dateTimePicker2.TabIndex = 40;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Punch in",
            "Punch out",
            "Lunch out",
            "Lunch in"});
            this.comboBox1.Location = new System.Drawing.Point(665, 249);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(254, 21);
            this.comboBox1.TabIndex = 41;
            // 
            // colFlag
            // 
            this.colFlag.Text = "Flagged?";
            // 
            // FrmAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1365, 788);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.ButtonEditPunch);
            this.Controls.Add(this.TextBoxEmployeeId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DateTimePicker);
            this.Controls.Add(this.LogoutBtn);
            this.Controls.Add(this.LabelAdminName);
            this.Controls.Add(this.ListViewEmployeeTime);
            this.Controls.Add(this.ButtonAdminReport);
            this.Controls.Add(this.ButtonAdminRefresh);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAdmin";
            this.Text = "Admincs";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ButtonAdminRefresh;
        private System.Windows.Forms.Button ButtonAdminReport;
        private System.Windows.Forms.ListView ListViewEmployeeTime;
        private System.Windows.Forms.ColumnHeader colUserID;
        private System.Windows.Forms.ColumnHeader colPunchTime;
        private System.Windows.Forms.ColumnHeader colPunchType;
        private System.Windows.Forms.Label LabelAdminName;
        private System.Windows.Forms.Button LogoutBtn;
        private System.Windows.Forms.DateTimePicker DateTimePicker;
        private System.Windows.Forms.Button ButtonEditPunch;
        private System.Windows.Forms.TextBox TextBoxEmployeeId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader colPunchDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ColumnHeader colFlag;
    }
}