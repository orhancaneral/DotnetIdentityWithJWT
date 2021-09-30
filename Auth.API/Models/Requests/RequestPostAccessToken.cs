namespace Auth.API.Models.Requests
{
    public class RequestPostAccessToken
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
