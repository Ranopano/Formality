using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Formality.App.Submissions.Models;
using Formality.Tests.Fixtures;
using Xunit;

namespace Formality.Tests.FunctionalTests
{
    public class SubmissionsControllerTests : IWebApplicationFixture
    {
        private readonly HttpClient _client;

        public SubmissionsControllerTests(WebApplicationFixture factory)
        {
            _client = factory.CreateClient();
        }
    }
}
