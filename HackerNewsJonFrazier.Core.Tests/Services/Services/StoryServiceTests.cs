using System;
using System.Net.Http;
using System.Threading.Tasks;
using HackerNewsJonFrazier.Core.Services;
using HackerNewsJonFrazier.Core.Services.BusinessLogic;
using HackerNewsJonFrazier.Core.Tests.Helpers;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace HackerNewsJonFrazier.Core.Tests.Services.Services
{
    public class StoryServiceTests
    {
        [Fact]
        public async Task GetStorySummariesAsync_WhenCannotFetchData_LogsError()
        {
            // SET UP
            var loggerMock = new Mock<ILogger>();
            var urlHelperMock = new Mock<IUrlHelper>();
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var configurationMock = new Mock<IConfiguration>();
            var mockHttpMessageHandler = new MockHttpMessageHandler("[123]",
                System.Net.HttpStatusCode.InternalServerError);


            urlHelperMock
                .Setup(x => x.SummariesUrl)
                .Returns("https://example.com/");

            urlHelperMock
                .Setup(x => x.GetDetailsUrl(It.IsAny<long>()))
                .Returns("urlDetails");

            httpClientFactoryMock
                .Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient(mockHttpMessageHandler));

            configurationMock
                .Setup(x => x["MaxNewStories"])
                .Returns("123");

            var sut = new StoryService(loggerMock.Object, urlHelperMock.Object,
                httpClientFactoryMock.Object, configurationMock.Object);

            // ACT
            await sut.GetStorySummariesAsync();

            // VERIFY
            loggerMock.Verify(x => x.LogError("Error retrieving new story ids from HN"));
        }

        [Fact]
        public async Task GetStorySummariesAsync_WhenNoErrors_LogsInfo()
        {
            // SET UP
            var loggerMock = new Mock<ILogger>();
            var urlHelperMock = new Mock<IUrlHelper>();
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var configurationMock = new Mock<IConfiguration>();
            var mockHttpMessageHandler = new MockHttpMessageHandler("[123]",
                System.Net.HttpStatusCode.OK);


            urlHelperMock
                .Setup(x => x.SummariesUrl)
                .Returns("https://example.com/");

            urlHelperMock
                .Setup(x => x.GetDetailsUrl(It.IsAny<long>()))
                .Returns("https://google.com");

            httpClientFactoryMock
                .Setup(x => x.CreateClient(It.IsAny<string>()))
                .Returns(new HttpClient(mockHttpMessageHandler));

            configurationMock
                .Setup(x => x["MaxNewStories"])
                .Returns("123");

            var sut = new StoryService(loggerMock.Object, urlHelperMock.Object,
                httpClientFactoryMock.Object, configurationMock.Object);

            // ACT
            await sut.GetStorySummariesAsync();

            // VERIFY
            loggerMock.Verify(x => x.LogError("Retrieved new story ids from HN"), Times.Never);
            loggerMock.Verify(x => x.LogInfo("Error retrieving new story ids from HN"), Times.Never);
        }
    }
}
