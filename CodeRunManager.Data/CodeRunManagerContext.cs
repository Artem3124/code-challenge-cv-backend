using CodeRunManager.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeRunManager.Data
{
    public class CodeRunManagerContext : DbContext
    {
        public CodeRunManagerContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<CodeRun> CodeRuns { get; set; }

        public DbSet<CodeRunResult> CodeRunResults { get; set; }
    }
}
