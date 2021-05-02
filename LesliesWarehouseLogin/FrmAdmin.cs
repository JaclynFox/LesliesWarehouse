using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;

namespace LesliesWarehouse
{
    public partial class FrmAdmin : Form
    {
        private static HttpClient client = new HttpClient();
        FrmLogin loginform;
        public FrmAdmin(FrmLogin l)
        {
            InitializeComponent();
            loginform = l;
        }
        //----------------------------------------------------------------------------------
        // Moving application with a borderless window ------------------------------------
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        // ----------------------------------------------------------------------------------

        private async void ButtonAdminRefresh_Click(object sender, EventArgs e)
        {
            List<PunchRecord> punchRecords = new List<PunchRecord>();
            punchRecords = await RetreivePunches(DateTimePicker.Value);
            ListViewEmployeeTime.Items.Clear();
            foreach (PunchRecord pr in punchRecords)
            {
                string[] addthis = { pr.PunchDate, pr.PunchTime, pr.PunchType, pr.Flag };
                ListViewEmployeeTime.Items.Add(pr.EmpID).SubItems.AddRange(addthis);
            }
        }
        private void ListViewEmployeeTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection items = this.ListViewEmployeeTime.SelectedItems;
            foreach (ListViewItem item in items)
            {
                TextBoxEmployeeId.Text = item.SubItems[0].Text;
                dateTimePicker1.Value = Convert.ToDateTime(item.SubItems[1].Text);
                dateTimePicker2.Value = Convert.ToDateTime(item.SubItems[2].Text);
                switch (item.SubItems[3].Text)
                {
                    case "out":
                        comboBox1.SelectedIndex = 1;
                        break;
                    case "in":
                        comboBox1.SelectedIndex = 0;
                        break;
                    case "lunchout":
                        comboBox1.SelectedIndex = 2;
                        break;
                    case "lunchin":
                        comboBox1.SelectedIndex = 3;
                        break;
                }
            }
        }
        private async Task<List<PunchRecord>> RetreivePunches(DateTime dt)
        {
            FrmSplash splash = new FrmSplash();
            splash.Show();
            string datestring = dt.ToString("MM/dd/yyyy");
            string urlstring = "https://56w4zz9yr2.execute-api.us-west-2.amazonaws.com/LoginAndPunch";
            urlstring += "?&request=" + "admin";
            urlstring += "&punchDate=" + datestring;
            HttpRequestMessage req = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(urlstring)
            };
            HttpResponseMessage res = await client.SendAsync(req);
            List<PunchRecord> pr = JsonConvert.DeserializeObject<List<PunchRecord>>(await res.Content.ReadAsStringAsync());
            splash.Close();
            return pr;
        }

        private async void ButtonEditPunch_Click(object sender, EventArgs e)
        {
            if (TextBoxEmployeeId.Text != string.Empty && comboBox1.SelectedItem.ToString() != string.Empty)
            {
                FrmSplash splash = new FrmSplash();
                splash.Show();
                string punch = string.Empty;
                switch (comboBox1.SelectedItem.ToString())
                {
                    case "Lunch out":
                        punch = "lunchout";
                        break;
                    case "Lunch in":
                        punch = "lunchin";
                        break;
                    case "Punch out":
                        punch = "out";
                        break;
                    case "Punch in":
                        punch = "in";
                        break;
                }
                string urlstring = "https://56w4zz9yr2.execute-api.us-west-2.amazonaws.com/LoginAndPunch";
                urlstring += "?&request=" + "update";
                urlstring += "&punchDate=" + dateTimePicker1.Value.ToString("MM/dd/yyyy");
                urlstring += "&punchTime=" + dateTimePicker2.Value.ToString("hh:mm:ss.fff tt");
                urlstring += "&empID=" + TextBoxEmployeeId.Text;
                urlstring += "&punchType=" + punch;
                HttpRequestMessage req = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(urlstring)
                };
                HttpResponseMessage res = await client.SendAsync(req);
                List<PunchRecord> pr = JsonConvert.DeserializeObject<List<PunchRecord>>(await res.Content.ReadAsStringAsync());
                splash.Close();
                ButtonAdminRefresh_Click(this, new EventArgs());
            }
        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            loginform.Show();
            this.Close();
        }
        
        private async void ButtonAdminReport_Click(object sender, EventArgs e)
        {
            List<PunchRecord> punchRecords = new List<PunchRecord>();
            punchRecords = await RetreivePunches(DateTimePicker.Value);
            string filepath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\logs\"));
            filepath += $"log_{DateTimePicker.Value.ToString("yyyy-MM-dd")}.csv";
            string delimiter = ",";

            List<string> rows = new List<string>();
            string header = "Punch Date, User ID, Punch Time, Punch Type, Flagged";
            foreach(PunchRecord element in punchRecords)
            {
                string temp = element.PunchDate + delimiter;
                temp += element.EmpID + delimiter;
                temp += element.PunchTime + delimiter;
                temp += element.PunchType + delimiter;
                temp += element.Flag;

                rows.Add(temp);
            }
            try
            {
                FileStream f = new FileStream(filepath, FileMode.OpenOrCreate);
                StreamWriter s = new StreamWriter(f);
                s.WriteLine(header);
                foreach (string row in rows)
                    s.WriteLine(row);
                s.Close();  
            }
            catch(Exception ex) { }
        }
    }
}
