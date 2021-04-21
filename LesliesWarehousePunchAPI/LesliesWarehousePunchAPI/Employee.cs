﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LesliesWarehousePunchAPI
{
    public class Employee
    {
        private int _empID;
        private string _name, _title, _request, _password, _type;
        public int EmpID { get => _empID; set => _empID = value; }
        public string Name { get => _name; set => _name = value; }
        public string Title { get => _title; set => _title = value; }
        public string Request { get => _request; set => _request = value; }
        public string Password { get => _password; set => _password = value; }
        public string Type { get => _type; set => _type = value; }
        public Employee() { }
        [JsonConstructor]
        public Employee(string r, int id, string n, string t, string p, string type)
        {
            Request = r;
            EmpID = id;
            Name = n;
            Title = t;
            Password = p;
            Type = type;
        }
        public Employee(int id, string n, string t, string p, string type)
        {
            EmpID = id;
            Name = n;
            Title = t;
            Password = p;
            Type = type;
        }
    }
}
