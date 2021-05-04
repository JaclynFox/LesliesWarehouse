using System;
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
        //Sends a request to the API that adds a punch to the database. It then displays a goodbye message to let the employee know that they have been logged out.
        private async void ButtonPunch_Click(object sender, EventArgs e)
        {
            await loginform.Punch(loginform.emp, "out");
            MessageBox.Show("You have been logged out.\nHave a good day.");
            loginform.Show();
            this.Close();
        }
        //Sends a request to the API that adds a punch to the database. Then displays a message to let the employee know they have been clocked out for lunch.
        private async void ButtonBreak_Click(object sender, EventArgs e)
        {
            await loginform.Punch(loginform.emp, "lunchout");
            MessageBox.Show("You have been clocked out for lunch.\nEnjoy your break.");
            loginform.Show();
            this.Close();
        }
        //Simply closes this form and shows the login form. Since the login form is the main form, it is always loaded.
        private void ButtonEmployeeLogout_Click(object sender, EventArgs e)
        {
            loginform.Show();
            this.Close();
        }
    }
}
