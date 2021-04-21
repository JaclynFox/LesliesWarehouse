using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LesliesWarehousePunchAPI
{
    public class PunchRecord
    {
        private int _punchID, _empID;
        private string _datetime, _punchtype, _flag;
        public int PunchID { get => _punchID; set => _punchID = value; }
        public int EmpID { get => _empID; set => _empID = value; }
        public string Datetime { get => _datetime; set => _datetime = value; }
        public string Punchtype { get => _punchtype; set => _punchtype = value; }
        public string Flag { get => _flag; set => _flag = value; }
        [JsonConstructor]
        public PunchRecord(int pid, int e, string d, string p, string f)
        {
            PunchID = pid;
            EmpID = e;
            Datetime = d;
            Punchtype = p;
            Flag = f;
        }
        public PunchRecord() { }
    }
}
