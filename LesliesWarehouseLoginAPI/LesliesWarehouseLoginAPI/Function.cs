using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Newtonsoft.Json;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LesliesWarehouseLoginAPI
{
    /*This function is simply for logging in. It accepts a LoginRequest object and then checks the loginID and password. 
     * If the combination is good, it returns the employee. If the combination is bad, it returns an empty employee with 
     * the request type of "Invalid ID or password." */
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
                return new Employee("Invalid ID or password", null, null, null, null, null, null);
        }
        /* This method does the actual logic for checking if the combination of loginID and password are correct.
         * It currently only is called once, but I made it a method for ease of future development.
         * Currently, due to limitations within dynamo, it has to use the most inefficient way of retrieving the 
         * information by doing a scan and then filtering the information. Filtering within the API or serverside 
         * using the filter argument doesn't matter as it is the same amount of read actions. This can be solved
         * by shuffling some of the information to a different table. I will at a later date shuffle that info to a
         * new table and modify references to fix the issue. */
        public async Task<Employee> CheckUserPass(LoginRequest lr)
        {
            Dictionary<string, AttributeValue> lastKeyEvaluated = null;
            List<Employee> employees = new List<Employee>();
            Employee returns = null;
            List<String> attributes = new List<string>();
            attributes.Add("empID");
            attributes.Add("name");
            attributes.Add("title");
            attributes.Add("password");
            attributes.Add("empType");
            attributes.Add("loginID");
            do
            {
                ScanRequest req = new ScanRequest
                {
                    TableName = empTable,
                    Limit = 10,
                    ExclusiveStartKey = lastKeyEvaluated
                };
                ScanResponse res = await client.ScanAsync(empTable, attributes);
                foreach (Dictionary<String, AttributeValue> item in res.Items)
                {
                    employees.Add(new Employee(item["empID"].S, item["name"].S, item["title"].S, item["password"].S, item["empType"].S, item["loginID"].S));
                }
            } while (lastKeyEvaluated != null && lastKeyEvaluated.Count != 0);
            foreach (Employee e in employees)
            {
                if (e.LoginID == lr.LoginID && e.Password == lr.Password)
                {
                    returns = new Employee("good", e.EmpID, e.Name, e.Title, e.Password, e.EmpType, e.LoginID);
                    break;
                }
            }
            if (returns != null)
                return returns;
            else
                return new Employee("Invalid username or password", null, null, null, null, null, null);
        }
    }
}
