using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Formality.App.Submissions.Models;
using Formality.Tests.Fixtures;
using Xunit;

namespace Formality.Tests.FunctionalTests
{
    public class FormsControllerTests : IWebApplicationFixture
    {
        private readonly HttpClient _client;

        public FormsControllerTests(WebApplicationFixture factory)
        {
            _client = factory.CreateClient();
        }
    }
}
