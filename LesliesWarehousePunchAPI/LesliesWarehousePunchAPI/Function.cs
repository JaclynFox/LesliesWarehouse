using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System.Globalization;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LesliesWarehousePunchAPI
{
    public class Function
    {
        private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        private String table = "LesliesWarehousePunches";
        public async Task<List<PunchRecord>> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
        {
            Dictionary<string, string> iparams = (Dictionary<string, string>)input.QueryStringParameters;
            string request = string.Empty, pid = string.Empty, empID = string.Empty, date = string.Empty, pType = string.Empty;
            iparams.TryGetValue("request", out request);
            List<PunchRecord> punches = new List<PunchRecord>();
            switch (request)
            {
                case "admin":
                    iparams.TryGetValue("date", out date);
                    punches = await AdminPunches(date);
                    break;
                case "update":
                    iparams.TryGetValue("punchID", out pid);
                    iparams.TryGetValue("empID", out empID);
                    iparams.TryGetValue("date", out date);
                    iparams.TryGetValue("pType", out pType);
                    UpdatePunch(pid, empID, date, pType);
                    break;
                default:
                    iparams.TryGetValue("empID", out empID);
                    iparams.TryGetValue("pType", out pType);
                    Punch(empID, pType);
                    break;
            }
            return punches;
        }
        public async Task<List<PunchRecord>> AdminPunches(string date)
        {
            Dictionary<string, AttributeValue> lastKeyEvaluated = null;
            List<PunchRecord> punches = new List<PunchRecord>();
            do
            {
                QueryRequest req = new QueryRequest
                {
                    TableName = table,
                    Limit = 10,
                    KeyConditionExpression = "date = :v_date",
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                        {":v_date", new AttributeValue { S =  date }}},
                    ExclusiveStartKey = lastKeyEvaluated
                };
                QueryResponse res = await client.QueryAsync(req);
                foreach (Dictionary<String, AttributeValue> item in res.Items)
                {
                    punches.Add(new PunchRecord(item["punchID"].S, item["empID"].S, item["date"].S, item["punchType"].S, item["flag"].S));
                }
            } while (lastKeyEvaluated != null && lastKeyEvaluated.Count != 0);
            return punches;
        }
        public async void UpdatePunch(string punchID, string empID, string date, string punchType)
        {
            Dictionary<string, AttributeValue > attr = new Dictionary<string, AttributeValue>();
            attr["punchID"] = new AttributeValue { S = punchID };
            attr["empID"] = new AttributeValue { S = empID };
            attr["date"] = new AttributeValue { S = date };
            attr["punchType"] = new AttributeValue { S = punchType };
            attr["flag"] = new AttributeValue { S = await CheckForFlag(empID, date, punchType) };
            PutItemRequest req = new PutItemRequest(table, attr);
            PutItemResponse res = await client.PutItemAsync(req);
        }
        public async void Punch(string empID, string punchType)
        {
            string datestring = DateTime.Now.ToString("G", CultureInfo.CreateSpecificCulture("en-us"));
            Dictionary<string, AttributeValue> attr = new Dictionary<string, AttributeValue>();
            attr["punchID"] = new AttributeValue { S = empID + " " + datestring };
            attr["empID"] = new AttributeValue { S = empID };
            attr["date"] = new AttributeValue { S = datestring };
            attr["punchType"] = new AttributeValue { S = punchType };
            attr["flag"] = new AttributeValue { S = await CheckForFlag(empID, datestring, punchType) };
            PutItemRequest req = new PutItemRequest(table, attr);
            PutItemResponse res = await client.PutItemAsync(req);
        }
        public async Task<string> CheckForFlag(string empID, string date, string punchType)
        {
            string flag = string.Empty;
            Dictionary<string, AttributeValue> lastKeyEvaluated = null;
            List<PunchRecord> punches = new List<PunchRecord>();
            do
            {
                QueryRequest req = new QueryRequest
                {
                    TableName = table,
                    Limit = 10,
                    KeyConditionExpression = "date < :v_date",
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                        {":v_date", new AttributeValue { S =  date }}},
                    ExclusiveStartKey = lastKeyEvaluated
                };
                QueryResponse res = await client.QueryAsync(req);
                foreach (Dictionary<String, AttributeValue> item in res.Items)
                {
                    punches.Add(new PunchRecord(item["punchID"].S, item["empID"].S, item["date"].S, item["punchType"].S, item["flag"].S));
                }
            } while (lastKeyEvaluated != null && lastKeyEvaluated.Count != 0);
            switch (punches[punches.Count - 1].PunchType)
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
    }
}
