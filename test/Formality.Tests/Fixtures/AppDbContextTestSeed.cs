using System.Linq;
using System.Threading.Tasks;
using Formality.App.Infrastructure;
using Formality.App.Submissions.Models;
using Microsoft.EntityFrameworkCore;

namespace Formality.Tests.Fixtures;

public class AppDbContextTestSeed : AppDbContextSeed
{
    private readonly AppDbContext _context;

    public AppDbContextTestSeed(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public override async Task SeedAsync()
    {
        await base.SeedAsync();

        if (!await _context.Submissions.AnyAsync())
        {
            await AddDefaultSubmission();
        }
    }

    private async Task AddDefaultSubmission()
    {
        var form = await _context.Forms
            .Include(x => x.Fields)
            .FirstAsync();

        var submission = new Submission
        {
            Form = form,
            Values = form.Fields
                .Select(x => new SubmissionValue
                {
                    Field = x,
                    Type = x.Type,
                    Value = x.Name
                })
                .ToList()
        };

        _context.Add(submission);

        await _context.SaveChangesAsync();
    }
}
