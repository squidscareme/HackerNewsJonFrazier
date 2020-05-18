using System;
using Microsoft.Extensions.Configuration;

using HackerNewsJonFrazier.Core.Services;

using FluentAssertions;
using Moq;
using Xunit;

namespace HackerNewsJonFrazier.Core.Tests.Services
{
    public class UrlHelperTests
    {
        private const string apiKey = "HackerNewsBaseApi";
        private const string versionKey = "HackerNewsApiVersion";

        [Fact]
        public void GetSummariesUrl_Never_CallsConfigurationMoreThanOnce()
        {
            // SET UP
            var configurationMock = new Mock<IConfiguration>();
            var loggerMock = new Mock<ILogger>();
            configurationMock
                .Setup(x => x[apiKey])
                .Returns("ANY_STRING");

            configurationMock
                .Setup(x => x[versionKey])
                .Returns("ANY_STRING");

            var sut = new UrlHelper(configurationMock.Object, loggerMock.Object);

            // ACT
            var url = sut.SummariesUrl;

            // VERIFY
            configurationMock.Verify(x => x[apiKey], Times.Once);
            configurationMock.Verify(x => x[versionKey], Times.Once);

        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(default(string))]
        public void GetSummariesUrl_WhenUrlIsNotPresent_ThrowsException(string url)
        {
            // SET UP
            var configurationMock = new Mock<IConfiguration>();
            var loggerMock = new Mock<ILogger>();

            configurationMock.
                Setup(x => x[versionKey])
                .Returns("ANY_STRING");

            configurationMock
                .Setup(x => x[apiKey])
                .Returns(url);

            var sut = new UrlHelper(configurationMock.Object, loggerMock.Object);
            Action action = () => { var summariesUrl = sut.SummariesUrl; };

            // ACT
            var exception = Assert.Throws<NullReferenceException>(action);

            // VERIFY
            exception.Should().NotBeNull();
            loggerMock.Verify(x => x.LogError("Url does not exist in configuration"), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(default(string))]
        public void GetSummariesUrl_WhenVersionIsNotPresent_ThrowsException(string version)
        {
            // SET UP
            var configurationMock = new Mock<IConfiguration>();
            var loggerMock = new Mock<ILogger>();

            configurationMock.
                Setup(x => x[apiKey])
                .Returns("ANY_STRING");

            configurationMock.
                Setup(x => x[versionKey])
                .Returns(version);

            var sut = new UrlHelper(configurationMock.Object, loggerMock.Object);
            Action action = () => { var summariesUrl = sut.SummariesUrl; };

            // ACT
            var exception = Assert.Throws<NullReferenceException>(action);

            // VERIFY
            exception.Should().NotBeNull();
            loggerMock.Verify(x => x.LogError("Version does not exist in configuration"), Times.Once);
        }

        [Fact]
        public void GetSummariesUrl_Always_ReturnsUrl()
        {
            // SET UP
            var configurationMock = new Mock<IConfiguration>();
            var loggerMock = new Mock<ILogger>();
            const string url = "https://example.com/";
            const string version = "v123";

            configurationMock.
                Setup(x => x[apiKey])
                .Returns(url);

            configurationMock.
                Setup(x => x[versionKey])
                .Returns(version);

            var sut = new UrlHelper(configurationMock.Object, loggerMock.Object);

            // ACT
            var returnedUrl = sut.SummariesUrl;

            // VERIFY
            returnedUrl.Should()
                .StartWith(url)
                .And.Contain(version)
                .And.EndWith("newstories.json");
        }

        [Fact]
        public void GetDetailsUrl_Never_CallsConfigurationMoreThanOnce()
        {
            // SET UP
            var configurationMock = new Mock<IConfiguration>();
            var loggerMock = new Mock<ILogger>();
            configurationMock
                .Setup(x => x[apiKey])
                .Returns("ANY_STRING");

            configurationMock
                .Setup(x => x[versionKey])
                .Returns("ANY_STRING");

            var sut = new UrlHelper(configurationMock.Object, loggerMock.Object);

            // ACT
            var url = sut.GetDetailsUrl(123);

            // VERIFY
            configurationMock.Verify(x => x[apiKey], Times.Once);
            configurationMock.Verify(x => x[versionKey], Times.Once);

        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(default(string))]
        public void GetDetailsUrl_WhenUrlIsNotPresent_ThrowsException(string url)
        {
            // SET UP
            var configurationMock = new Mock<IConfiguration>();
            var loggerMock = new Mock<ILogger>();

            configurationMock.
                Setup(x => x[versionKey])
                .Returns("ANY_STRING");

            configurationMock
                .Setup(x => x[apiKey])
                .Returns(url);

            var sut = new UrlHelper(configurationMock.Object, loggerMock.Object);
            Action action = () => { var summariesUrl = sut.GetDetailsUrl(123); };

            // ACT
            var exception = Assert.Throws<NullReferenceException>(action);

            // VERIFY
            exception.Should().NotBeNull();
            loggerMock.Verify(x => x.LogError("Url does not exist in configuration"), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(default(string))]
        public void GetDetailsUrl_WhenVersionIsNotPresent_ThrowsException(string version)
        {
            // SET UP
            var configurationMock = new Mock<IConfiguration>();
            var loggerMock = new Mock<ILogger>();

            configurationMock.
                Setup(x => x[apiKey])
                .Returns("ANY_STRING");

            configurationMock.
                Setup(x => x[version])
                .Returns(version);

            var sut = new UrlHelper(configurationMock.Object, loggerMock.Object);
            Action action = () => { var summariesUrl = sut.GetDetailsUrl(123); };

            // ACT
            var exception = Assert.Throws<NullReferenceException>(action);

            // VERIFY
            exception.Should().NotBeNull();
            loggerMock.Verify(x => x.LogError("Version does not exist in configuration"), Times.Once);
        }

        [Fact]
        public void GetDetailsUrl_Always_ReturnsUrl()
        {
            // SET UP
            var configurationMock = new Mock<IConfiguration>();
            var loggerMock = new Mock<ILogger>();
            const string url = "https://example.com/";
            const string version = "v123";
            const long id = 123;

            configurationMock.
                Setup(x => x[apiKey])
                .Returns(url);

            configurationMock.
                Setup(x => x[versionKey])
                .Returns(version);

            var sut = new UrlHelper(configurationMock.Object, loggerMock.Object);

            // ACT
            var returnedUrl = sut.GetDetailsUrl(id);

            // VERIFY
            returnedUrl.Should()
                .StartWith(url)
                .And.Contain(version)
                .And.EndWith($"item/{id}.json");
        }
    }
}
