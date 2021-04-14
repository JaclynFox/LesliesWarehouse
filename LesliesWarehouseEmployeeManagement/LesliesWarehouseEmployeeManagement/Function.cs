using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LesliesWarehouseEmployeeManagement
{
    public class Function
    {
        private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        private String tablename = "LesliesWarehouseEmployees";
        public async Task<List<Employee>> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
        {
            Employee emp = JsonConvert.DeserializeObject<Employee>(input.Body);
            string del = String.Empty;
            List<Employee> employees = new List<Employee>();
            if (emp.Request == "put")
            {
                EditEmployee(emp);
            }
            else if (emp.Request == "delete")
            {
                del = await DeleteEmployee(emp);
            }
            if (del == String.Empty || del == "success")
                employees = await GetEmployees();
            else
                employees.Add(new Employee("Employee not found", 0, null, null));
            return employees;
        }
        public async void EditEmployee(Employee emp)
        {
            Dictionary<String, AttributeValue> dict = new Dictionary<string, AttributeValue>();
            dict["empID"] = new AttributeValue { N = emp.EmpID.ToString() };
            dict["name"] = new AttributeValue { S = emp.Name };
            dict["title"] = new AttributeValue { S = emp.Title };
            PutItemRequest req = new PutItemRequest(tablename, dict);
            await client.PutItemAsync(req);
        }
        public async Task<String> DeleteEmployee(Employee emp)
        {
            Dictionary<String, AttributeValue> dict = new Dictionary<string, AttributeValue>();
            dict["empID"] = new AttributeValue { N = emp.EmpID.ToString() };
            DeleteItemRequest req = new DeleteItemRequest(tablename, dict, "ALL_OLD");
            DeleteItemResponse res = await client.DeleteItemAsync(req);
            if (res.Attributes.Count == 0)
                return "Employee does not exist";
            else
                return "success";
        }
        public async Task<List<Employee>> GetEmployees()
        {
            Dictionary<string, AttributeValue> lastKeyEvaluated = null;
            List<Employee> employees = new List<Employee>();
            List<String> attributes = new List<string>();
            attributes.Add("empID");
            attributes.Add("name");
            attributes.Add("title");
            do
            {
                ScanRequest req = new ScanRequest
                {
                    TableName = tablename,
                    Limit = 10,
                    ExclusiveStartKey = lastKeyEvaluated
                };
                ScanResponse res = await client.ScanAsync(tablename, attributes);
                foreach (Dictionary<String, AttributeValue> item in res.Items)
                    employees.Add(new Employee(int.Parse(item["empID"].N.ToString()), item["name"].S, item["title"].S));
            } while (lastKeyEvaluated != null && lastKeyEvaluated.Count != 0);
            return employees;
        }
    }
}
