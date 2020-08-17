using Microsoft.EntityFrameworkCore;
using Formality.App.Forms.Models;
using Formality.App.Submissions.Models;

namespace Formality.App.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Form> Forms { get; set; } = default!;

        public DbSet<FormField> FormFields { get; set; } = default!;

        public DbSet<FormFieldValue> FieldValues { get; set; } = default!;

        public DbSet<Submission> Submissions { get; set; } = default!;

        public DbSet<SubmissionValue> SubmissionValues { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContextConfiguration).Assembly);
        }
    }
}
