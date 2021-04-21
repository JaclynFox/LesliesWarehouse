using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LesliesWarehousePunchAPI
{
    public class PunchRecord
    {
        private string _punchID, _empID, _datetime, _punchType, _flag;
        public string PunchID { get => _punchID; set => _punchID = value; }
        public string EmpID { get => _empID; set => _empID = value; }
        public string Datetime { get => _datetime; set => _datetime = value; }
        public string PunchType { get => _punchType; set => _punchType = value; }
        public string Flag { get => _flag; set => _flag = value; }
        [JsonConstructor]
        public PunchRecord(string pid, string e, string d, string p, string f)
        {
            PunchID = pid;
            EmpID = e;
            Datetime = d;
            PunchType = p;
            Flag = f;
        }
        public PunchRecord() { }
    }
}
