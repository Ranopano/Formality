using System.Threading.Tasks;

namespace Formality.App.Infrastructure
{
    public class AppDbContextSeed
    {
        private readonly AppDbContext _context;

        public AppDbContextSeed(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await Task.CompletedTask;
        }
    }
}
