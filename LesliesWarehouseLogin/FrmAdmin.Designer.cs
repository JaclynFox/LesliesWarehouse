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
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LabelAdminName = new System.Windows.Forms.Label();
            this.LogoutBtn = new System.Windows.Forms.Button();
            this.DateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.ButtonEditPunch = new System.Windows.Forms.Button();
            this.TextBoxClockOut = new System.Windows.Forms.TextBox();
            this.TextBoxBreakIn = new System.Windows.Forms.TextBox();
            this.TextBoxBreakOut = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TextBoxLastName = new System.Windows.Forms.TextBox();
            this.TextBoxEmployeeId = new System.Windows.Forms.TextBox();
            this.TextBoxClockIn = new System.Windows.Forms.TextBox();
            this.TextBoxFirstName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ButtonAdminRefresh
            // 
            this.ButtonAdminRefresh.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ButtonAdminRefresh.Location = new System.Drawing.Point(266, 677);
            this.ButtonAdminRefresh.Name = "ButtonAdminRefresh";
            this.ButtonAdminRefresh.Size = new System.Drawing.Size(106, 40);
            this.ButtonAdminRefresh.TabIndex = 2;
            this.ButtonAdminRefresh.Text = "Refresh";
            this.ButtonAdminRefresh.UseVisualStyleBackColor = true;
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
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.ListViewEmployeeTime.HideSelection = false;
            this.ListViewEmployeeTime.Location = new System.Drawing.Point(40, 128);
            this.ListViewEmployeeTime.Name = "ListViewEmployeeTime";
            this.ListViewEmployeeTime.Size = new System.Drawing.Size(601, 514);
            this.ListViewEmployeeTime.TabIndex = 5;
            this.ListViewEmployeeTime.UseCompatibleStateImageBehavior = false;
            this.ListViewEmployeeTime.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "User ID";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "First Name";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Last Name";
            this.columnHeader3.Width = 90;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Clock in";
            this.columnHeader4.Width = 68;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Clock out";
            this.columnHeader5.Width = 78;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Break in";
            this.columnHeader6.Width = 70;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Break out";
            this.columnHeader7.Width = 82;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Total time";
            // 
            // LabelAdminName
            // 
            this.LabelAdminName.AutoSize = true;
            this.LabelAdminName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.LabelAdminName.Location = new System.Drawing.Point(524, 20);
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
            // 
            // DateTimePicker
            // 
            this.DateTimePicker.Location = new System.Drawing.Point(172, 65);
            this.DateTimePicker.Name = "DateTimePicker";
            this.DateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.DateTimePicker.TabIndex = 13;
            // 
            // ButtonEditPunch
            // 
            this.ButtonEditPunch.ForeColor = System.Drawing.SystemColors.WindowText;
            this.ButtonEditPunch.Location = new System.Drawing.Point(704, 632);
            this.ButtonEditPunch.Name = "ButtonEditPunch";
            this.ButtonEditPunch.Size = new System.Drawing.Size(75, 23);
            this.ButtonEditPunch.TabIndex = 38;
            this.ButtonEditPunch.Text = "Edit Punch";
            this.ButtonEditPunch.UseVisualStyleBackColor = true;
            // 
            // TextBoxClockOut
            // 
            this.TextBoxClockOut.Location = new System.Drawing.Point(671, 588);
            this.TextBoxClockOut.Name = "TextBoxClockOut";
            this.TextBoxClockOut.Size = new System.Drawing.Size(140, 20);
            this.TextBoxClockOut.TabIndex = 37;
            // 
            // TextBoxBreakIn
            // 
            this.TextBoxBreakIn.Location = new System.Drawing.Point(671, 507);
            this.TextBoxBreakIn.Name = "TextBoxBreakIn";
            this.TextBoxBreakIn.Size = new System.Drawing.Size(140, 20);
            this.TextBoxBreakIn.TabIndex = 36;
            // 
            // TextBoxBreakOut
            // 
            this.TextBoxBreakOut.Location = new System.Drawing.Point(671, 426);
            this.TextBoxBreakOut.Name = "TextBoxBreakOut";
            this.TextBoxBreakOut.Size = new System.Drawing.Size(140, 20);
            this.TextBoxBreakOut.TabIndex = 35;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label7.Location = new System.Drawing.Point(718, 470);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Break In";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label6.Location = new System.Drawing.Point(714, 389);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 33;
            this.label6.Text = "Break Out";
            // 
            // TextBoxLastName
            // 
            this.TextBoxLastName.Location = new System.Drawing.Point(671, 264);
            this.TextBoxLastName.Name = "TextBoxLastName";
            this.TextBoxLastName.ReadOnly = true;
            this.TextBoxLastName.Size = new System.Drawing.Size(140, 20);
            this.TextBoxLastName.TabIndex = 32;
            // 
            // TextBoxEmployeeId
            // 
            this.TextBoxEmployeeId.Location = new System.Drawing.Point(671, 102);
            this.TextBoxEmployeeId.Name = "TextBoxEmployeeId";
            this.TextBoxEmployeeId.ReadOnly = true;
            this.TextBoxEmployeeId.Size = new System.Drawing.Size(140, 20);
            this.TextBoxEmployeeId.TabIndex = 31;
            // 
            // TextBoxClockIn
            // 
            this.TextBoxClockIn.Location = new System.Drawing.Point(671, 345);
            this.TextBoxClockIn.Name = "TextBoxClockIn";
            this.TextBoxClockIn.Size = new System.Drawing.Size(140, 20);
            this.TextBoxClockIn.TabIndex = 30;
            // 
            // TextBoxFirstName
            // 
            this.TextBoxFirstName.Location = new System.Drawing.Point(671, 183);
            this.TextBoxFirstName.Name = "TextBoxFirstName";
            this.TextBoxFirstName.ReadOnly = true;
            this.TextBoxFirstName.Size = new System.Drawing.Size(140, 20);
            this.TextBoxFirstName.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label5.Location = new System.Drawing.Point(718, 308);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Clock In";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label4.Location = new System.Drawing.Point(712, 227);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 27;
            this.label4.Text = "Last Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label3.Location = new System.Drawing.Point(714, 551);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Clock Out";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label2.Location = new System.Drawing.Point(708, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 25;
            this.label2.Text = "Employee ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.label1.Location = new System.Drawing.Point(713, 146);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "First Name";
            // 
            // FrmAdmin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1365, 788);
            this.Controls.Add(this.ButtonEditPunch);
            this.Controls.Add(this.TextBoxClockOut);
            this.Controls.Add(this.TextBoxBreakIn);
            this.Controls.Add(this.TextBoxBreakOut);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.TextBoxLastName);
            this.Controls.Add(this.TextBoxEmployeeId);
            this.Controls.Add(this.TextBoxClockIn);
            this.Controls.Add(this.TextBoxFirstName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
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
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label LabelAdminName;
        private System.Windows.Forms.Button LogoutBtn;
        private System.Windows.Forms.DateTimePicker DateTimePicker;
        private System.Windows.Forms.Button ButtonEditPunch;
        private System.Windows.Forms.TextBox TextBoxClockOut;
        private System.Windows.Forms.TextBox TextBoxBreakIn;
        private System.Windows.Forms.TextBox TextBoxBreakOut;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TextBoxLastName;
        private System.Windows.Forms.TextBox TextBoxEmployeeId;
        private System.Windows.Forms.TextBox TextBoxClockIn;
        private System.Windows.Forms.TextBox TextBoxFirstName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}