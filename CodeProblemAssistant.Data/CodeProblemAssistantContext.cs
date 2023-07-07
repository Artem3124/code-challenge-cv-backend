using CodeProblemAssistant.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeProblemAssistant.Data
{
    public class CodeProblemAssistantContext : DbContext
    {
        public CodeProblemAssistantContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<CodeProblem> CodeProblems { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Vote> Votes { get; set; }

        public DbSet<Challenge> Challenges { get; set; }

        public DbSet<PrivateChallengeAllowedUsers> PrivateChallengeAllowedUsers { get; set; }

        public DbSet<ChallengeAttempt> Attempts { get; set; }
    }
}
