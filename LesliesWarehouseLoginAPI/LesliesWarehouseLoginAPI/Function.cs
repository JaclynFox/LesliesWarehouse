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

namespace LesliesWarehouseLoginAPI
{
    public class Function
    {
        private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        private String empTable = "LesliesWarehouseEmployees";
        public async Task<Employee> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
        {
            LoginRequest lr = JsonConvert.DeserializeObject<LoginRequest>(input.Body);
            Employee emp = await CheckUserPass(lr);
            if (emp.Request == "good")
                return emp;
            else
                return new Employee("Invalid ID or password", 0, null, null, null, null);
        }
        public async Task<Employee> CheckUserPass(LoginRequest lr)
        {
            Dictionary<string, AttributeValue> lastKeyEvaluated = null;
            List<Employee> employees = new List<Employee>();
            do
            {
                QueryRequest req = new QueryRequest
                {
                    TableName = empTable,
                    Limit = 10,
                    KeyConditionExpression = "loginID = :v_loginID",
                    ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                        {":v_loginID", new AttributeValue { S =  lr.LoginID }}},
                    ExclusiveStartKey = lastKeyEvaluated
                };
                QueryResponse res = await client.QueryAsync(req);
                foreach (Dictionary<String, AttributeValue> item in res.Items)
                {
                    employees.Add(new Employee(item["empID"].S, item["name"].S, item["title"].S, item["password"].S, item["empType"].S, item["loginID"].S));
                }
            } while (lastKeyEvaluated != null && lastKeyEvaluated.Count != 0);

  /*          GetItemResponse res = await client.GetItemAsync(empTable, new Dictionary<string, AttributeValue>
                {
                    { "loginID", new AttributeValue {S = lr.LoginID} }
                });*/
            Employee emp = JsonConvert.DeserializeObject<Employee>(Document.FromAttributeMap(res.Item).ToJson());
            if (emp.EmpID.Equals(lr.EmpID) && emp.Password.Equals(lr.Password))
            {
                emp.Request = "good";
                return emp;
            }
            else
                return new Employee("Invalid username or password", null, null, null, null, null, null);
        }
    }
}
