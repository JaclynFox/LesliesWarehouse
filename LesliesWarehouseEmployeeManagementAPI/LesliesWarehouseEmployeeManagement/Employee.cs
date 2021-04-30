using System;
using Newtonsoft.Json;

namespace LesliesWarehouseEmployeeManagement
{
    //This is a class for holding employee records fetched from the database or created by the main UI for data manipulation.
    public class Employee
    {
        private string _empID, _name, _title, _request, _password, _empType, _loginID;
        public string EmpID { get => _empID; set => _empID = value; }
        public string Name { get => _name; set => _name = value; }
        public string Title { get => _title; set => _title = value; }
        public string Request { get => _request; set => _request = value; }
        public string Password { get => _password; set => _password = value; }
        public string EmpType { get => _empType; set => _empType = value; }
        public string LoginID { get => _loginID; set => _loginID = value; }
        //Blank constructor. No current use, but is here mostly for testing.
        public Employee() { }
        //This is the main constructor. This is the constructor used by newtonsoft.json. Only difference here is that it has a request field to decipher what kind of request is being called.
        [JsonConstructor]
        public Employee(string r, string id, string n, string t, string p, string type, string l)
        {
            Request = r;
            EmpID = id;
            Name = n;
            Title = t;
            Password = p;
            EmpType = type;
            LoginID = l;
        }
        //Constructor for making an employee without a request. This is what you use for data storage.
        public Employee(string id, string n, string t, string p, string type, string l)
        {
            EmpID = id;
            Name = n;
            Title = t;
            Password = p;
            EmpType = type;
            LoginID = l;
        }
        //Constructor for making a new employee record. This constructor programatically creates the empID field. This constructor is only to be used when you need to add a new employee record to the database.
        public Employee(string n, string t, string p, string type, string l)
        {
            string datestring = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt");
            EmpID = n + " " + datestring;
            Name = n;
            Title = t;
            Password = p;
            EmpType = type;
            LoginID = l;
        }
    }
}
