using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Formality.Api.Controllers
{
    public static class ControllersExtensions
    {
        // TODO: custom model binding for json query
        public static T GetQuery<T>(this ControllerBase controller, string? query) where T : new()
        {
            return string.IsNullOrWhiteSpace(query) ? new T() : JsonConvert.DeserializeObject<T>(query);
        }
    }
}
