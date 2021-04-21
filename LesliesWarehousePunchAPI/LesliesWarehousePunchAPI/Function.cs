using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Newtonsoft.Json;

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
            string type = string.Empty, pid = string.Empty, empID = string.Empty, date = string.Empty, pType = string.Empty;
            iparams.TryGetValue("type", out type);
            List<PunchRecord> punches = new List<PunchRecord>();
            switch (type)
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
                    await UpdatePunch(int.Parse(pid), int.Parse(empID), date, pType);
                    break;
                default:
                    iparams.TryGetValue("empID", out empID);
                    iparams.TryGetValue("pType", out pType);
                    await Punch(empID, pType);
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
                    punches.Add(new PunchRecord(int.Parse(item["punchID"].N.ToString()), int.Parse(item["empID"].N.ToString()), item["date"].S, item["punchType"].S, item["flag"].S));
                }
            } while (lastKeyEvaluated != null && lastKeyEvaluated.Count != 0);
            return punches;
        }
        public async void UpdatePunch(int punchID, int empID, string date, string punchType)
        {
            Dictionary<string, AttributeValue > attr = new Dictionary<string, AttributeValue>();
            attr["punchID"] = new AttributeValue { N = punchID.ToString() };
            attr["empID"] = new AttributeValue { N = empID.ToString() };
            attr["date"] = new AttributeValue { S = date };
            attr["punchType"] = new AttributeValue { S = punchType };
            attr["flag"] = new AttributeValue { S = await CheckForFlag(empID, date, punchType) };
            PutItemRequest req = new PutItemRequest(table, attr);
            PutItemResponse res = await client.PutItemAsync(req);
        }
        public async void Punch(int empID, string punchType)
        {
            //todo code
        }
        public async Task<string> CheckForFlag(int empID, string date, string punchType)
        {

        }
    }
}
