namespace LesliesWarehouse
{
    partial class FrmEmployee
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
            this.ButtonPunch = new System.Windows.Forms.Button();
            this.LabelWelcome = new System.Windows.Forms.Label();
            this.ButtonBreak = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ButtonPunch
            // 
            this.ButtonPunch.Location = new System.Drawing.Point(163, 372);
            this.ButtonPunch.Name = "ButtonPunch";
            this.ButtonPunch.Size = new System.Drawing.Size(75, 23);
            this.ButtonPunch.TabIndex = 0;
            this.ButtonPunch.Text = "Punch In";
            this.ButtonPunch.UseVisualStyleBackColor = true;
            // 
            // LabelWelcome
            // 
            this.LabelWelcome.AutoSize = true;
            this.LabelWelcome.ForeColor = System.Drawing.SystemColors.Window;
            this.LabelWelcome.Location = new System.Drawing.Point(160, 248);
            this.LabelWelcome.Name = "LabelWelcome";
            this.LabelWelcome.Size = new System.Drawing.Size(91, 13);
            this.LabelWelcome.TabIndex = 1;
            this.LabelWelcome.Text = "Welcome {Name}";
            // 
            // ButtonBreak
            // 
            this.ButtonBreak.Location = new System.Drawing.Point(285, 372);
            this.ButtonBreak.Name = "ButtonBreak";
            this.ButtonBreak.Size = new System.Drawing.Size(75, 23);
            this.ButtonBreak.TabIndex = 2;
            this.ButtonBreak.Text = "Break";
            this.ButtonBreak.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.Window;
            this.label1.Location = new System.Drawing.Point(335, 248);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Weekly Hours: {Hours}";
            // 
            // FrmEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(29)))), ((int)(((byte)(36)))));
            this.ClientSize = new System.Drawing.Size(956, 658);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ButtonBreak);
            this.Controls.Add(this.LabelWelcome);
            this.Controls.Add(this.ButtonPunch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmEmployee";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FrmEmployee_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonPunch;
        private System.Windows.Forms.Label LabelWelcome;
        private System.Windows.Forms.Button ButtonBreak;
        private System.Windows.Forms.Label label1;
    }
}

