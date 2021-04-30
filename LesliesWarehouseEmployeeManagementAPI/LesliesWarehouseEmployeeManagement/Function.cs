using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LesliesWarehouseEmployeeManagement
{
    /*This function is for managing the employees in the database. It has methods for adding, editing, retrieving, 
     * and deleting records.*/
    public class Function
    {
        private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        private String tablename = "LesliesWarehouseEmployees";
        public async Task<List<Employee>> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
        {
            Employee emp = JsonConvert.DeserializeObject<Employee>(input.Body);
            string del = String.Empty;
            List<Employee> employees = new List<Employee>();
            //This multipart if statement checks whether you are editing, adding, or deleting an employee.
            if (emp.Request == "put")
            {
                await EditEmployee(emp);
            }
            /*You'll notice that this just calls the EditEmployee method. The reason for this is that put with 
             * either edit or add a record, so it doesn't matter much. The only reason you need a different request 
             * type is because if you're creating a new employee, you need to call the constructor that creates the 
             * employee's empID field.*/
            else if (emp.Request == "new")
            {
                await EditEmployee(new Employee(emp.Name, emp.Title, emp.Password, emp.EmpType, emp.LoginID));
            }
            else if (emp.Request == "delete")
            {
                del = await DeleteEmployee(emp);
            }
            if (del == String.Empty || del == "success")
                employees = await GetEmployees();
            else
                employees.Add(new Employee("Employee not found", null, null, null, null, null));
            return employees;
        }
        //This calls for a put request on the database.
        public async Task EditEmployee(Employee emp)
        {
            Dictionary<String, AttributeValue> dict = new Dictionary<string, AttributeValue>();
            dict["empID"] = new AttributeValue { S = emp.EmpID };
            dict["name"] = new AttributeValue { S = emp.Name };
            dict["title"] = new AttributeValue { S = emp.Title };
            dict["password"] = new AttributeValue { S = emp.Password };
            dict["empType"] = new AttributeValue { S = emp.EmpType };
            dict["loginID"] = new AttributeValue { S = emp.LoginID };
            PutItemRequest req = new PutItemRequest(tablename, dict);
            await client.PutItemAsync(req);
        }
        /*This deletes a record from the database with simple error checking to make sure that an appropriate message
         * is returned if the employee is not found within the database. */
        public async Task<String> DeleteEmployee(Employee emp)
        {
            Dictionary<String, AttributeValue> dict = new Dictionary<string, AttributeValue>();
            dict["empID"] = new AttributeValue { S = emp.EmpID };
            DeleteItemRequest req = new DeleteItemRequest(tablename, dict, "ALL_OLD");
            DeleteItemResponse res = await client.DeleteItemAsync(req);
            if (res.Attributes.Count == 0)
                return "Employee does not exist";
            else
                return "success";
        }
        /*Simple scan statement. Scans and returns all employees. Resource intensive, but it should take 
         * 100-200 employees before it becomes an issue. At that point it might be worth looking into using
         * a different cloud provider as it could get expensive due to how AWS records are read and stored. */
        public async Task<List<Employee>> GetEmployees()
        {
            Dictionary<string, AttributeValue> lastKeyEvaluated = null;
            List<Employee> employees = new List<Employee>();
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
                    TableName = tablename,
                    Limit = 10,
                    ExclusiveStartKey = lastKeyEvaluated
                };
                ScanResponse res = await client.ScanAsync(tablename, attributes);
                foreach (Dictionary<String, AttributeValue> item in res.Items)
                {
                    employees.Add(new Employee(item["empID"].S, item["name"].S, item["title"].S, item["password"].S, item["empType"].S, item["loginID"].S));
                }
            } while (lastKeyEvaluated != null && lastKeyEvaluated.Count != 0);
            return employees;
        }
    }
}
