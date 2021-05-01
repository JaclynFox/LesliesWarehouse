using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
        private bool ValidateFields()
        {
            bool returns = true;
            if (TextBoxLogin.Text == string.Empty || TextBoxPassword.Text == string.Empty)
                returns = false;
            return returns;
        }
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
