using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LesliesWarehouse
{
    public partial class FrmLogin : Form
    {
        public Employee emp = new Employee();
        static readonly HttpClient client = new HttpClient();
        public FrmLogin()
        {
            InitializeComponent();
        }
        /*This happens when the login button is clicked (if it were me that designed the form, it would have been called btnLogin for simplicity's sake).
         * This first validates the loginID and password fields. It then determines if the person whom logged in is an admin or not. If they are an admin
         * it shows the admin form. If they are an employee, it shows the clock form. In production, we may want to display a form that gives the admin
         * the option to either display the admin form or punch form since there is currently no way for an admin to punch in other than adding a punch
         * through the admin form which is a bit obtuse.*/
        private async void button_WOC1_Click(object sender, EventArgs e)
        {
            bool valid = ValidateFields();
            if (valid == true)
            {
                emp = await CheckLogin(TextBoxLogin.Text, TextBoxPassword.Text);
                if (emp.Request != "Invalid ID or password")
                {
                    if (emp.EmpType == "admin")
                    {
                        FrmAdmin frmAdmin = new FrmAdmin(this);
                        this.Hide();
                        frmAdmin.Show();
                    }
                    else
                    {
                        PunchRecord pr = await Punch(emp, "get");
                        /*Checks the type of punch. Automatically punches the employee in or back from lunch depending on the circumstance. Displays appropriate message
                         * if the employee is automatically punched. This is a measure to eliminate accidental punches.*/
                        switch (pr.PunchType)
                        {
                            case "out":
                                await Punch(emp, "in");
                                MessageBox.Show("You have been clocked in", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            case "lunchout":
                                await Punch(emp, "lunchin");
                                MessageBox.Show("Welcome back from lunch. You have been clocked in.", "Back From Lunch", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            default:
                                FrmEmployee frmEmployee = new FrmEmployee(this);
                                this.Hide();
                                frmEmployee.Show();
                                break;
                        }
                    }
                }
                else
                    MessageBox.Show("Bad username or password");
            }
            TextBoxLogin.Text = "";
            TextBoxPassword.Text = "";
        }
        //Makes sure that something has been entered in both the login and password box. The API handles validation regarding loginID/password combination.
        private bool ValidateFields()
        {
            bool returns = true;
            if (TextBoxLogin.Text == string.Empty || TextBoxPassword.Text == string.Empty)
                returns = false;
            return returns;
        }
        /*This sends the punch request to the API. With how the API handles punches, if a request type falls out of the switch, a punch is added. This is why the 
         * request type and punch type are the same. In production this works as such: punch types are "in", "out", "lunchout", or "lunchin." A request type of
         * any of these four will fall out and default to adding a punch.*/
        public async Task<PunchRecord> Punch(Employee emp, string type)
        {
            FrmSplash splash = new FrmSplash();
            splash.Show();
            string urlstring = "https://56w4zz9yr2.execute-api.us-west-2.amazonaws.com/LoginAndPunch";
            urlstring += "?&request=" + type;
            urlstring += "&empID=" + emp.EmpID;
            urlstring += "&punchType=" + type;
            HttpRequestMessage req = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(urlstring)
            };
            HttpResponseMessage res = await client.SendAsync(req);
            List<PunchRecord> pr = JsonConvert.DeserializeObject<List<PunchRecord>>(await res.Content.ReadAsStringAsync());
            splash.Close();
            return pr[0];
        }
        /*This encrypts the password and sends it along with the loginID to the API. The API then checks to see if the loginID/password pair matches. All
         * passwords are stored in the database encrypted as a 128 byte value that has been converted to base64String for transmittal vial JSON and simplicity.
         * CSPRNG is not the most secure, we may want to look into using SHA-3.*/
        public async Task<Employee> CheckLogin(string un, string pw)
        {
            FrmSplash splash = new FrmSplash();
            splash.Show();
            byte[] hash = new byte[128];
            byte[] salt = new byte[64];
            var passthing = new Rfc2898DeriveBytes(pw, salt, 10000);
            hash = passthing.GetBytes(128);
            string password = Convert.ToBase64String(hash);
            string contentString = "{\"loginID\":\"" + un + "\",\"password\":\"" + password + "\"}";
            HttpRequestMessage req = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://56w4zz9yr2.execute-api.us-west-2.amazonaws.com/LoginAndPunch"),
                Content = new StringContent(contentString, Encoding.UTF8, "application/json")
            };
            HttpResponseMessage res = await client.SendAsync(req);
            Employee emp = JsonConvert.DeserializeObject<Employee>(await res.Content.ReadAsStringAsync());
            splash.Close();
            return emp;
        }
    }
}
