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
            GetItemResponse res = await client.GetItemAsync(empTable, new Dictionary<string, AttributeValue>
                {
                    { "empID", new AttributeValue {N = lr.EmpID.ToString()} }
                });
            Employee emp = JsonConvert.DeserializeObject<Employee>(Document.FromAttributeMap(res.Item).ToJson());
            if (emp.EmpID.Equals(lr.EmpID) && emp.Password.Equals(lr.Password))
            {
                emp.Request = "good";
                return emp;
            }
            else
                return new Employee("Invalid username or password", 0, null, null, null, null);
        }
    }
}
