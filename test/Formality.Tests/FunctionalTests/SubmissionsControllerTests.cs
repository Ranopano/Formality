using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Formality.App.Common.Dto;
using Formality.App.Forms.Dto;
using Formality.App.Forms.Queries;
using Formality.App.Submissions.Dto;
using Formality.App.Submissions.Queries;
using Formality.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Formality.Tests.FunctionalTests
{
    [Collection(nameof(ControllerTestBase))]
    public class SubmissionsControllerTests : ControllerTestBase
    {
        private readonly string _requestUri = "/api/submissions";

        public SubmissionsControllerTests(WebApplicationFixture factory) : base(factory)
        {
        }

        [Fact]
        public async Task SearchSubmissions_NoExceptions()
        {
            var keyword = "name";
            var queryString = GetSearchQueryString(keyword);

            var response = await Client.GetAsync(_requestUri + $"?{queryString}");
            response.EnsureSuccessStatusCode();

            var submissions = await DeserializeResponse<SubmissionListDto[]>(response);
            var submission = submissions.FirstOrDefault();

            submission.Should().NotBeNull();
        }

        [Fact]
        public async Task AddSubmission_Invalid_ShouldReturnValidationErrors()
        {
            var json = File.ReadAllText("Fixtures/submission.invalid.json");
            var jsonContent = new StringContent(json, UTF8Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await Client.PostAsync(_requestUri, jsonContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var details = await DeserializeResponse<ValidationProblemDetails>(response);

            details.Errors.Should().HaveCount(1);
        }

        [Fact]
        public async Task AddSubmission_Valid_ShouldBeCreatedAndReturned()
        {
            var json = File.ReadAllText("Fixtures/submission.json");
            var jsonContent = new StringContent(json, UTF8Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await Client.PostAsync(_requestUri, jsonContent);

            response.EnsureSuccessStatusCode();

            var id = await DeserializeResponse<int>(response);

            id.Should().BePositive();
        }

        protected string GetSearchQueryString(string keyword)
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
