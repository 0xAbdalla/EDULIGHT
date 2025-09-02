namespace EDULIGHT.Identity.Context
{
    public class AuthModel
    {
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime ExpireOn { get; set; }


    }
}
