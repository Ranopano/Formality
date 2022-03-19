using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Formality.App.Common.Queries;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace Formality.Api.ModelBinders;

public class SearchQueryModelBinder : IModelBinder
{
    public async Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var request = bindingContext.ActionContext.HttpContext.Request;

        var queryString = WebUtility.UrlDecode(request.QueryString.Value?.TrimStart('?'));

        if (string.IsNullOrWhiteSpace(queryString))
        {
            return;
        }

        var query = JsonSerializer.Deserialize(
            queryString,
            bindingContext.ModelType,
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

        bindingContext.Result = ModelBindingResult.Success(query);

        // TODO: handle parsing errors

        await Task.CompletedTask;
    }

    public class Provider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            var type = context.Metadata.ModelType.BaseType;

            if (type?.IsGenericType == true && type.GetGenericTypeDefinition() == typeof(SearchQuery<>))
            {
                return new BinderTypeModelBinder(typeof(SearchQueryModelBinder));
            }

            return null;
        }
    }
}
