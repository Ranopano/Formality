using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Formality.Tests.Fixtures;

public abstract class ControllerTestBase : IWebApplicationFixture
{
    public ControllerTestBase(WebApplicationFixture factory)
    {
        Client = factory.CreateClient();
    }

    protected HttpClient Client { get; }

    protected async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrEmpty(content))
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
}
