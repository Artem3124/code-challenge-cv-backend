namespace Gateway.Contact.Models
{
#nullable disable
    public class RegistrationRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string RepeatPassword { get; set; }

        public string Login { get; set; }
    }
#nullable enable
}
