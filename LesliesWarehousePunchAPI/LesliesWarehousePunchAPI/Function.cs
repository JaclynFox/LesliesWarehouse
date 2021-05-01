using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;


[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LesliesWarehousePunchAPI
{
    // This function allows for punching in, getting all punches by date, or updating a PunchRecord
    public class Function
    {
        private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        private String punchTable = "LesliesWarehousePunches", lastPunchTable = "LesliesWarehouseLastPunches";
        public async Task<List<PunchRecord>> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
        {
            Dictionary<string, string> iparams = (Dictionary<string, string>)input.QueryStringParameters;
            string request = string.Empty, punchDate = string.Empty, punchTime = string.Empty, empID = string.Empty, punchType = string.Empty;
            iparams.TryGetValue("request", out request);
            List<PunchRecord> punches = new List<PunchRecord>();
            /*Switch checks which of the four cases you have: admin, update a punch, get last punch,
             * or the default; add a punch. */
            switch (request)
            {
                case "admin":
                    iparams.TryGetValue("punchDate", out punchDate);
                    punches = await AdminPunches(punchDate);
                    break;
                case "update":
                    iparams.TryGetValue("punchDate", out punchDate);
                    iparams.TryGetValue("punchTime", out punchTime);
                    iparams.TryGetValue("empID", out empID);
                    iparams.TryGetValue("punchType", out punchType);
                    punches.Add(await UpdatePunch(punchDate, punchTime, empID, punchType));
                    break;
                case "get":
                    iparams.TryGetValue("empID", out empID);
                    punches.Add(await GetLastPunch(empID));
                    break;
                default:
                    iparams.TryGetValue("empID", out empID);
                    iparams.TryGetValue("punchType", out punchType);
                    punches.Add(await Punch(empID, punchType));
                    break;
            }
            return punches;
        }
        //This method returns all punches for a given date.
        public async Task<List<PunchRecord>> AdminPunches(string date)
        {
            Dictionary<string, AttributeValue> lastKeyEvaluated = null;
            List<PunchRecord> punches = new List<PunchRecord>();
            do
            {
                QueryRequest req = new QueryRequest
                {
                    TableName = punchTable,
                    Limit = 10,
                    KeyConditionExpression = "punchDate = :v_punchDate",
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                        {":v_punchDate", new AttributeValue { S =  date }}},
                    ExclusiveStartKey = lastKeyEvaluated
                };
                QueryResponse res = await client.QueryAsync(req);
                foreach (Dictionary<String, AttributeValue> item in res.Items)
                {
                    punches.Add(new PunchRecord(item["punchDate"].S, item["punchTime"].S, item["empID"].S, item["punchType"].S, item["flag"].S));
                }
            } while (lastKeyEvaluated != null && lastKeyEvaluated.Count != 0);
            return punches;
        }
        //This method updates a punch, then returns PunchRecord representation of what was added.
        public async Task<PunchRecord> UpdatePunch(string punchDate, string punchTime, string empID, string punchType)
        {
            Dictionary<string, AttributeValue > attr = new Dictionary<string, AttributeValue>();
            attr["punchDate"] = new AttributeValue { S = punchDate };
            attr["punchTime"] = new AttributeValue { S = punchTime };
            attr["empID"] = new AttributeValue { S = empID };
            attr["punchType"] = new AttributeValue { S = punchType };
            attr["flag"] = new AttributeValue { S = await CheckForFlag(empID, punchType) };
            PutItemRequest req = new PutItemRequest(punchTable, attr);
            PutItemResponse res = await client.PutItemAsync(req);
            PunchRecord pr = await GetLastPunch(empID);
            if (DateTime.Parse(punchDate + " " + punchTime) >= DateTime.Parse(pr.PunchDate + " " + pr.PunchTime))
            {
                Dictionary<string, AttributeValue> attrr = new Dictionary<string, AttributeValue>();
                attrr["empID"] = attr["empID"];
                attrr["lastPunch"] = new AttributeValue { S = punchDate + " " + punchTime };
                attrr["punchType"] = new AttributeValue { S = punchType };
                req = new PutItemRequest(lastPunchTable, attrr);
                res = await client.PutItemAsync(req);
            }
            return new PunchRecord(attr["punchDate"].S, attr["punchTime"].S, attr["empID"].S, attr["punchType"].S, attr["flag"].S);
        }
        //This method adds a punch and returns a PunchRecord representation of what was added.
        public async Task<PunchRecord> Punch(string empID, string punchType)
        {
            string[] datestring = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt").Split(' ');
            Dictionary<string, AttributeValue> attr = new Dictionary<string, AttributeValue>(), attrr = new Dictionary<string, AttributeValue>();
            attr["punchDate"] = new AttributeValue { S = datestring[0] };
            attr["punchTime"] = new AttributeValue { S = datestring[1] + " " + datestring[2] };
            attr["empID"] = new AttributeValue { S = empID };
            attr["punchType"] = new AttributeValue { S = punchType };
            attr["flag"] = new AttributeValue { S = await CheckForFlag(empID, punchType) };
            attrr["empID"] = attr["empID"];
            attrr["lastPunch"] = new AttributeValue { S = datestring[0] + " " + datestring[1] + " " + datestring[2] };
            attrr["punchType"] = new AttributeValue { S = punchType };
            PutItemRequest req = new PutItemRequest(punchTable, attr);
            PutItemResponse res = await client.PutItemAsync(req);
            req = new PutItemRequest(lastPunchTable, attrr);
            res = await client.PutItemAsync(req);
            return new PunchRecord(attr["punchDate"].S, attr["punchTime"].S, attr["empID"].S, attr["punchType"].S, attr["flag"].S);
        }
        /* This method checks whether the punch should be flagged or not. It does this by first checking what your
         * last punch is. If you have not clocked in, it flags it if you clock out. If you have not clocked out 
         * for lunch, it flags you for clocking in from lunch, etc. */
        public async Task<string> CheckForFlag(string empID, string punchType)
        {
            string flag = string.Empty;
            PunchRecord pr = await GetLastPunch(empID);
            if (string.IsNullOrEmpty(pr.EmpID))
                flag = "no";
            else
                switch (pr.PunchType)
                {
                    case "out":
                        if (punchType == "in" || punchType == "lunchin")
                            flag = "no";
                        else
                            flag = "yes";
                        break;
                    case "in":
                        if (punchType == "out")
                            flag = "no";
                        else
                            flag = "yes";
                        break;
                    case "lunchout":
                        if (punchType == "in")
                            flag = "no";
                        else
                            flag = "yes";
                        break;
                    case "lunchin":
                        if (punchType == "lunchout")
                            flag = "no";
                        else
                            flag = "yes";
                        break;
                    default:
                        flag = "yes";
                        break;
                }
            return flag;
        }
        /*This simply retrieves the employee's last punch which is stored in a separate table to save time
         * and cut down on read requests. */
        public async Task<PunchRecord> GetLastPunch(string empID)
        {
            GetItemResponse res = await client.GetItemAsync(lastPunchTable, new Dictionary<string, AttributeValue>
            {
                { "empID", new AttributeValue {S = empID} }
            });
            string[] datestring = res.Item["lastPunch"].S.Split(' ');
            return new PunchRecord(datestring[0], datestring[1] + datestring[2], res.Item["empID"].S, res.Item["punchType"].S, "no");
        }
    }
}
