namespace WebApi.TokenOperations.Models
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime ExpireDate { get; set;}
        public string RefreshToken { get; set; }
    }
}