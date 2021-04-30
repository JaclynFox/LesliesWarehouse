using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LesliesWarehousePunchAPI
{
    public class PunchRecord
    {
        //Simply a data class for the punch records in the database.
        private string _punchDate, _punchTime, _empID, _punchType, _flag;
        public string PunchTime { get => _punchTime; set => _punchTime = value; }
        public string EmpID { get => _empID; set => _empID = value; }
        public string PunchDate { get => _punchDate; set => _punchDate = value; }
        public string PunchType { get => _punchType; set => _punchType = value; }
        public string Flag { get => _flag; set => _flag = value; }
        [JsonConstructor]
        public PunchRecord(string d, string t, string e, string p, string f)
        {
            PunchDate = d;
            PunchTime = t;
            EmpID = e;
            PunchType = p;
            Flag = f;
        }
        public PunchRecord() { }
    }
}
