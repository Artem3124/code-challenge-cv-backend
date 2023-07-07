namespace AccountManager.Contract.Models
{
#nullable disable
    public class UserCreateRequest
    {
        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string RepeatPassword { get; set; }

        public UserCreateRequest(string email, string password, string login, string repeatPassword)
        {
            Email = email;
            Password = password;
            Login = login;
            RepeatPassword = repeatPassword;
        }
    }
#nullable enable
}
