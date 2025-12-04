using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DocumentService.WebAPI;
using DocumentService.WebAPI.SeedData;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace DocumentService.Tests
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public HttpClient Client { get; private set; }

        public IntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            SetUpClient();
        }

        private async Task CheckOnBadRequest(DocumentForm document, string errorMessage)
        {
            var response0 = await Client.PostAsync($"/api/documents",
                new StringContent(JsonConvert.SerializeObject(document), Encoding.UTF8, "application/json"));
            response0.StatusCode.Should().BeEquivalentTo(StatusCodes.Status400BadRequest);
            var responseMessage = await response0.Content.ReadAsStringAsync();
            responseMessage.Contains(errorMessage).Should().BeTrue();
        }

        [Fact]
        // Checking Title
        public async Task TestValidateTitle()
        {
            string titleError = "Title is invalid: Title must contain a minimum of 5 characters and a maximum of 35, and each word should start with an uppercase letter";
            await CheckOnBadRequest(new DocumentForm
            {
                Title = "Mont",
                Size = 300,
                Format = "pdf"
            }, titleError);
            await CheckOnBadRequest(new DocumentForm
            {
                Title = "Monthly Revenue Report Monthly Revenue Report",
                Size = 300,
                Format = "pdf"
            }, titleError);
            await CheckOnBadRequest(new DocumentForm
            {
                Title = "monthly revenue report",
                Size = 300,
                Format = "pdf"
            }, titleError);

            var document = new DocumentForm
            {
                Title = "Monthly Revenue Report",
                Size = 300,
                Format = "pdf"
            };
            var response3 = await Client.PostAsync($"/api/documents",
                new StringContent(JsonConvert.SerializeObject(document), Encoding.UTF8, "application/json"));
            response3.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);
        }


        [Fact]
        // Checking Size
        public async Task TestValidateSize()
        {
            string errorMessage = "Size is invalid: Size must be greater than 0 MB and less than 500 MB";
            await CheckOnBadRequest(new DocumentForm
            {
                Title = "Monthly Revenue Report",
                Size = 0,
                Format = "txt"
            }, errorMessage);
            await CheckOnBadRequest(new DocumentForm
            {
                Title = "Monthly Revenue Report",
                Size = 501,
                Format = "txt"
            }, errorMessage);

            var document = new DocumentForm
            {
                Title = "Monthly Revenue Report",
                Size = 1,
                Format = "txt"
            };
            var response3 = await Client.PostAsync($"/api/documents",
                new StringContent(JsonConvert.SerializeObject(document), Encoding.UTF8, "application/json"));
            response3.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);
        }


        [Fact]
        // Checking Format
        public async Task TestValidateFormat()
        {
            string errorMessage = "Format is invalid: Format must be lowercase and equal one of the following: 'txt', 'pdf', 'docx'";
            await CheckOnBadRequest(new DocumentForm
            {
                Title = "Monthly Revenue Report",
                Size = 334,
                Format = ""
            }, errorMessage);
            await CheckOnBadRequest(new DocumentForm
            {
                Title = "Monthly Revenue Report",
                Size = 334,
                Format = "TXT"
            }, errorMessage);
            await CheckOnBadRequest(new DocumentForm
            {
                Title = "Monthly Revenue Report",
                Size = 334,
                Format = "xls"
            }, errorMessage);

            var document = new DocumentForm
            {
                Title = "Monthly Revenue Report",
                Size = 234,
                Format = "txt"
            };
            var response3 = await Client.PostAsync($"/api/documents",
                new StringContent(JsonConvert.SerializeObject(document), Encoding.UTF8, "application/json"));
            response3.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);

            var document2 = new DocumentForm
            {
                Title = "Monthly Revenue Report",
                Size = 234,
                Format = "pdf"
            };
            var response4 = await Client.PostAsync($"/api/documents",
                new StringContent(JsonConvert.SerializeObject(document2), Encoding.UTF8, "application/json"));
            response4.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);

            var document3 = new DocumentForm
            {
                Title = "Monthly Revenue Report",
                Size = 234,
                Format = "docx"
            };
            var response5 = await Client.PostAsync($"/api/documents",
                new StringContent(JsonConvert.SerializeObject(document3), Encoding.UTF8, "application/json"));
            response5.StatusCode.Should().BeEquivalentTo(StatusCodes.Status200OK);
        }

        private void SetUpClient()
        {
            Client = _factory.WithWebHostBuilder(builder =>
                builder.UseStartup<Startup>()
                .ConfigureServices(services =>
                {

                })).CreateClient();
        }
    }
}
