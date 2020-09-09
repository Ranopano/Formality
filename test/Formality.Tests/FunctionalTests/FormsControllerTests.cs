using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Formality.App.Common.Dto;
using Formality.App.Forms.Dto;
using Formality.App.Forms.Queries;
using Formality.Tests.Fixtures;
using Xunit;

namespace Formality.Tests.FunctionalTests
{
    [Collection(nameof(ControllerTestBase))]
    public class FormsControllerTests : ControllerTestBase
    {
        private readonly string _requestUri = "/api/forms";

        public FormsControllerTests(WebApplicationFixture factory) : base(factory)
        {
        }

        [Fact]
        public async Task SearchForms_NoExceptions()
        {
            var keyword = "Default";
            var queryString = GetQueryString(keyword);

            var response = await Client.GetAsync(_requestUri + $"?{queryString}");

            response.EnsureSuccessStatusCode();

            var forms = await DeserializeResponse<FormListDto[]>(response);
            var form = forms.FirstOrDefault(x => x.Name.Contains(keyword));

            form.Should().NotBeNull();
        }

        private string GetQueryString(string keyword)
        {
            var query = new SearchFormQuery
            {
                Keyword = keyword,
                MaxResults = 10,
                OrderBy = new[]
                {
                    new OrderDto { Name = "Id", Desc = true }
                }
            };

            return JsonSerializer.Serialize(query, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}
