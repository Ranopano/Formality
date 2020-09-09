using System.Linq;
using Formality.App.Forms.Models;
using Formality.App.Submissions.Models;
using Microsoft.EntityFrameworkCore;

namespace Formality.App.Infrastructure
{
    public interface IReadOnlyAppDbContext
    {
        IQueryable<Form> Forms { get; }

        IQueryable<FormField> FormFields { get; }

        IQueryable<FormFieldValue> FieldValues { get; }

        IQueryable<Submission> Submissions { get; }

        IQueryable<SubmissionValue> SubmissionValues { get; }
    }

    public class AppDbContext : DbContext, IReadOnlyAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Form> Forms { get; set; } = default!;

        public DbSet<FormField> FormFields { get; set; } = default!;

        public DbSet<FormFieldValue> FieldValues { get; set; } = default!;

        public DbSet<Submission> Submissions { get; set; } = default!;

        public DbSet<SubmissionValue> SubmissionValues { get; set; } = default!;

        #region IReadOnlyAppDbContext
        IQueryable<Form> IReadOnlyAppDbContext.Forms => Forms.AsNoTracking();

        IQueryable<FormField> IReadOnlyAppDbContext.FormFields => FormFields.AsNoTracking();

        IQueryable<FormFieldValue> IReadOnlyAppDbContext.FieldValues => FieldValues.AsNoTracking();

        IQueryable<Submission> IReadOnlyAppDbContext.Submissions => Submissions.AsNoTracking();

        IQueryable<SubmissionValue> IReadOnlyAppDbContext.SubmissionValues => SubmissionValues.AsNoTracking();
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContextConfiguration).Assembly);
        }
    }
}
