using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LesliesWarehouseLoginAPI
{
    public class LoginRequest
    {
        private int _empID;
        private string _password;
        public int EmpID { get => _empID; set => _empID = value; }
        public string Password { get => _password; set => _password = value; }
        [JsonConstructor]
        public LoginRequest(int id, string p)
        {
            EmpID = id;
            Password = p;
        }
    }
}
