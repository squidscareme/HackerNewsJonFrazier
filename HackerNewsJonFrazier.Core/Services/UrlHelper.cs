using System;
using Microsoft.Extensions.Configuration;

namespace HackerNewsJonFrazier.Core.Services
{
    internal class UrlHelper : IUrlHelper
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private string _url;

        public UrlHelper(IConfiguration configuration, ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public string SummariesUrl
        {
            get {
                if (string.IsNullOrWhiteSpace(_url))
                {
                    _url = getUrlFromConfiguration();
                }

                return $"{_url}newstories.json";
            }
        }

        public string GetDetailsUrl(long storyId)
        {
            if (string.IsNullOrWhiteSpace(_url))
            {
                _url = getUrlFromConfiguration();
            }

            return $"{_url}item/{storyId}.json";
        }

        private string getUrlFromConfiguration()
        {
            var url = _configuration["HackerNewsBaseApi"];
            var version = _configuration["HackerNewsApiVersion"];

            // TODO: unit test
            if (string.IsNullOrWhiteSpace(url))
            {
                var message = "Url does not exist in configuration";
                _logger.LogError(message);
                throw new NullReferenceException(message);
            }

            // TODO: unit test
            if (string.IsNullOrWhiteSpace(version))
            {
                var message = "Version does not exist in configuration";
                _logger.LogError(message);
                throw new NullReferenceException(message);
            }

            var urlWithVersion = $"{url}/{version}/";

            _logger.LogInfo($"Retrieved url {urlWithVersion} from configuration");

            return urlWithVersion;
        }
    }
}
