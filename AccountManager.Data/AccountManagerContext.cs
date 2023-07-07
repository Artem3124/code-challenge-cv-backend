using AccountManager.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountManager.Data
{
    public class AccountManagerContext : DbContext
    {
        public AccountManagerContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserChangeHistory> UserChangeHistory { get; set; }
    }

    public class UserChangeHistory
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public string ColumnName { get; set; }

        public DateTime DateTimeUtc { get; set; }

        public string OldValue { get; set; }
    }
}
