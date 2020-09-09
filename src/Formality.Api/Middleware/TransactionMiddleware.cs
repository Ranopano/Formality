using System;
using System.Linq;
using System.Threading.Tasks;
using Formality.App.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace Formality.Api.Middleware
{
    public class TransactionMiddleware : IMiddleware
    {
        private readonly AppDbContext _context;

        private readonly string[] _methods = new[]
        {
            HttpMethods.Post,
            HttpMethods.Put,
            HttpMethods.Delete,
        };

        public TransactionMiddleware(AppDbContext context)
        {
            _context = context;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var method = context.Request.Method.ToUpper();

            if (_methods.Contains(method))
            {
                using var transaction = _context.Database.BeginTransaction();

                await next(context);

                //await _context.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            else
            {
                await next(context);
            }
        }
    }
}
