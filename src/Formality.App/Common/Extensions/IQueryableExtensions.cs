using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Formality.App.Common.Dto;
using Formality.App.Common.Models;
using Formality.App.Common.Queries;

namespace Formality.App.Common.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<TEntity> WithOrderBy<TEntity, TDto>(
            this IQueryable<TEntity> query,
            SearchQuery<TDto> request)
            where TEntity : Entity
        {
            var orders = request.OrderBy ?? Enumerable.Empty<OrderDto>();

            // TODO: get rid of this
            var thennable = false;

            // TODO: some reflection
            var expressions = new Dictionary<string, Expression<Func<TEntity, dynamic>>>
            {
                { nameof(Entity.Id).ToUpper(), x => x.Id },
            };

            foreach (var order in orders)
            {
                if (!expressions.TryGetValue(order.Name.ToUpper(), out var expression))
                {
                    continue;
                }

                if (thennable && query is IOrderedQueryable<TEntity> orderedQuery)
                {
                    query = order.Desc
                        ? orderedQuery.ThenByDescending(expression)
                        : orderedQuery.ThenBy(expression);
                }
                else
                {
                    query = order.Desc
                        ? query.OrderByDescending(expression)
                        : query.OrderBy(expression);

                    thennable = true;
                }
            }

            return query;
        }
    }
}
