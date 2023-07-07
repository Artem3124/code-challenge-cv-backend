namespace AccountManager.Contract.Models
{
    public class UserUpdateRequest
    {
        public string? Email { get; set; }

        public string? Login { get; set; }

        public string? OldPassword { get; set; }

        public string? NewPassword { get; set; }
    }
}
