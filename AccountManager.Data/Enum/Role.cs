using System.ComponentModel;

namespace AccountManager.Data.Enum
{
    public enum Role : byte
    {
        [Description("User")]
        User = 0,
        [Description("Admin")]
        Admin = 1,
    }
}
