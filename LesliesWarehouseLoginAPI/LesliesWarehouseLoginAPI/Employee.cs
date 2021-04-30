using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;

namespace LesliesWarehouseLoginAPI
{
    public class Employee
    {
        private string _empID, _name, _title, _request, _password, _empType, loginID;
        public string EmpID { get => _empID; set => _empID = value; }
        public string Name { get => _name; set => _name = value; }
        public string Title { get => _title; set => _title = value; }
        public string Request { get => _request; set => _request = value; }
        public string Password { get => _password; set => _password = value; }
        public string EmpType { get => _empType; set => _empType = value; }
        public string LoginID { get => _loginID; set => _loginID = value; }
        public Employee() { }
        [JsonConstructor]
        public Employee(string r, string id, string n, string t, string p, string type, string lid)
        {
            Request = r;
            EmpID = id;
            Name = n;
            Title = t;
            Password = p;
            EmpType = type;
            LoginID = lid;
        }
        public Employee(string id, string n, string t, string p, string type, string lid)
        {
            EmpID = id;
            Name = n;
            Title = t;
            Password = p;
            EmpType = type;
            LoginID = lid;
        }
        public Employee(string n, string t, string p, string type, string lid)
        {
            string datestring = DateTime.Now.ToString("G", CultureInfo.CreateSpecificCulture("en-us"));
            EmpID = n + " " + datestring;
            Name = n;
            Title = t;
            Password = p;
            EmpType = type;
            LoginID = lid;
        }
    }
}
