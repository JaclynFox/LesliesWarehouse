using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LesliesWarehouseEmployeeManagement
{
    public class Employee
    {
        private int _empID;
        private string _name, _title, _request;
        public int EmpID { get => _empID; set => _empID = value; }
        public string Name { get => _name; set => _name = value; }
        public string Title { get => _title; set => _title = value; }
        public string Request { get => _request; set => _request = value; }
        public Employee() { }
        [JsonConstructor]
        public Employee(string r, int id, string n, string t)
        {
            Request = r;
            EmpID = id;
            Name = n;
            Title = t;
        }
        public Employee(int id, string n, string t)
        {
            EmpID = id;
            Name = n;
            Title = t;
        }
    }
}
