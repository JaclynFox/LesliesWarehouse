using Newtonsoft.Json;

namespace LesliesWarehouseLoginAPI
{
    public class LoginRequest
    {
        private string _loginID, _password;
        public string LoginID { get => _loginID; set => _loginID = value; }
        public string Password { get => _password; set => _password = value; }
        [JsonConstructor]
        public LoginRequest(string id, string p)
        {
            LoginID = id;
            Password = p;
        }
    }
}
