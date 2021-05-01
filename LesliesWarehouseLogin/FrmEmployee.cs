using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LesliesWarehouse
{
    public partial class FrmEmployee : Form
    {
        FrmLogin loginform;
        public FrmEmployee(FrmLogin f)
        {
            InitializeComponent();
            loginform = f;
            LabelWelcome.Text = "Welcome " + loginform.emp.Name;
        }

        private async void ButtonPunch_Click(object sender, EventArgs e)
        {
            await loginform.Punch(loginform.emp, "out");
            MessageBox.Show("You have been logged out.\nHave a good day.");
            loginform.Show();
            this.Close();
        }

        private async void ButtonBreak_Click(object sender, EventArgs e)
        {
            await loginform.Punch(loginform.emp, "lunchout");
            MessageBox.Show("You have been clocked out for lunch.\nEnjoy your break.");
            loginform.Show();
            this.Close();
        }

        private void ButtonEmployeeLogout_Click(object sender, EventArgs e)
        {
            loginform.Show();
            this.Close();
        }
    }
}
