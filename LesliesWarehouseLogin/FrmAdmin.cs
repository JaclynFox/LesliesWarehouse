using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Globalization;
using Newtonsoft.Json;

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

        //This method happens when the refresh button is clicked. It sends a request to the punchAPI and gets all punches for a certain date then loads them into the listView
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
        //This method simply exports the data from the selected record to the fields to the right of the listView.
        private void ListViewEmployeeTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection items = this.ListViewEmployeeTime.SelectedItems;
            foreach (ListViewItem item in items)
            {
                TextBoxEmployeeId.Text = item.SubItems[0].Text;
                dateTimePicker1.Value = Convert.ToDateTime(item.SubItems[1].Text, CultureInfo.InvariantCulture);
                dateTimePicker2.Value = Convert.ToDateTime(item.SubItems[2].Text, CultureInfo.InvariantCulture);
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
        /*The method that does the actual magic of sending the GET request to the API with a request type of admin and a punchDate of the date selected in the 
         * datetime picker. The API then returns all punchrecords found for that date.*/
        private async Task<List<PunchRecord>> RetreivePunches(DateTime dt)
        {
            FrmSplash splash = new FrmSplash();
            splash.Show();
            string datestring = dt.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            string urlstring = "https://56w4zz9yr2.execute-api.us-west-2.amazonaws.com/LoginAndPunch";
            urlstring += "?&request=" + "admin";
            urlstring += "&punchDate=" + datestring;
            HttpRequestMessage req = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(urlstring)
            };
            HttpResponseMessage res = await client.SendAsync(req);
            MessageBox.Show(datestring);
            List<PunchRecord> pr = JsonConvert.DeserializeObject<List<PunchRecord>>(await res.Content.ReadAsStringAsync());
            splash.Close();
            return pr;
        }
        /*This simply sends a GET request to the API with proper params to edit a punch. The method then calls the proper method to make all values
         * in the listView refresh.*/
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
                urlstring += "&punchDate=" + dateTimePicker1.Value.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                urlstring += "&punchTime=" + dateTimePicker2.Value.ToString("hh:mm:ss.fff tt", CultureInfo.InvariantCulture);
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
        //Just closes the admin form and shows the login form. The login form is always loaded as it is the startup form and closing it exits the entire program.
        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            loginform.Show();
            this.Close();
        }
        /*Exports the records in the listView to a CSV file. Calls a method that retreives the name of the employee. This is simply for readability
         * as the empID is the employee's name and a timestamp. Since this report is mainly for human review, the readability is important. The logic
         * in the foreach that builds the file saves employee names and empIDs to a dictionary to reduce read requests. In operation this makes it so that
         * a request is sent only when the name is not already in the dictionary. */
        private async void ButtonAdminReport_Click(object sender, EventArgs e)
        {
            List<PunchRecord> punchRecords = new List<PunchRecord>();
            List<Employee> employees = new List<Employee>();
            Dictionary<string, string> names = new Dictionary<string, string>();
            string name = string.Empty;
            punchRecords = await RetreivePunches(DateTimePicker.Value);
            FrmSplash splash = new FrmSplash();
            splash.Show();
            string filepath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\logs\"));
            filepath += $"log_{DateTimePicker.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}.csv";
            string delimiter = ",";
            List<string> rows = new List<string>();
            string header = "Employee Name, Punch Date, Punch Time, Punch Type, Flagged";
            foreach(PunchRecord element in punchRecords)
            {
                if (!names.TryGetValue(element.EmpID, out name))
                    names.Add(element.EmpID, await QueryEmpName(element.EmpID));
                names.TryGetValue(element.EmpID, out name);
                string temp = name + delimiter;
                temp += element.PunchDate + delimiter;
                temp += element.PunchTime + delimiter;
                temp += element.PunchType + delimiter;
                temp += element.Flag;
                name = string.Empty;
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
            catch(Exception ex)
            {
                MessageBox.Show("Error exporting to CSV. Error message below:\n" + ex.Message);
            }
            splash.Close();
        }
        //Simple method. This just sends a request to the API that gets an Employee object based on the empID it sends.
        private async Task<string> QueryEmpName(string empID)
        {
            string contentString = "{\"request\":\"getName\",\"empID\":\"" + empID + "\"}";
            HttpRequestMessage req = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://nedxc42fj7.execute-api.us-west-2.amazonaws.com/LesliesWarehouseEmployeeManagement"),
                Content = new StringContent(contentString, Encoding.UTF8, "application/json")
            };
            HttpResponseMessage res = await client.SendAsync(req);
            List<Employee> employees = JsonConvert.DeserializeObject<List<Employee>>(await res.Content.ReadAsStringAsync());
            return employees[0].Name;
        }
    }
}
