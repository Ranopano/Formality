using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Formality.App.Forms.Models;
using Formality.App.Submissions.Models;

namespace Formality.App.Infrastructure
{
    public class AppDbContextConfiguration :
        IEntityTypeConfiguration<Form>,
        IEntityTypeConfiguration<FormField>,
        IEntityTypeConfiguration<FormFieldRule>,
        IEntityTypeConfiguration<FormFieldValue>,
        IEntityTypeConfiguration<Submission>,
        IEntityTypeConfiguration<SubmissionValue>
    {

        public void Configure(EntityTypeBuilder<Form> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(2000)
                .IsRequired();

            builder.HasMany(x => x.Fields)
                .WithOne(x => x.Form)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.Name).IsUnique();
        }

        public void Configure(EntityTypeBuilder<FormField> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Label)
                .HasMaxLength(2000)
                .IsRequired();

            builder.Property(x => x.Type)
                .IsRequired();

            builder.Property(x => x.Deleted)
                .HasDefaultValue(false);

            builder.HasIndex(x => x.Name)
                .HasFilter($"{nameof(FormField.Deleted)} != 1")
                .IsUnique();
        }

        public void Configure(EntityTypeBuilder<FormFieldRule> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type)
                .IsRequired();
        }

        public void Configure(EntityTypeBuilder<FormFieldValue> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Value)
                .HasMaxLength(2000)
                .IsRequired();
        }

        public void Configure(EntityTypeBuilder<Submission> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Values)
                .WithOne(x => x.Submission)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public void Configure(EntityTypeBuilder<SubmissionValue> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type)
                .IsRequired();

            builder.HasOne(x => x.Field)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Value)
                .HasMaxLength(2000)
                .IsRequired();
        }
    }
}
