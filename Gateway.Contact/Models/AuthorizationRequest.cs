namespace Gateway.Contact.Models
{
#nullable disable
    public class AuthorizationRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
#nullable enable
}
