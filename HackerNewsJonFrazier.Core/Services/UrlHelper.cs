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

        public string Url
        {
            get {
                if (string.IsNullOrWhiteSpace(_url))
                {
                    _url = getUrlFromConfiguration();
                }

                return _url;
            }
        }

        private string getUrlFromConfiguration()
        {
            var urlSection = _configuration.GetSection("Urls");

            // TODO: unit test
            if (urlSection == null)
            {
                var message = "Urls config section does not exist";
                _logger.LogError(message);
                throw new NullReferenceException(message);
            }

            var url = urlSection.GetValue<string>("HackerNewsBaseApi");

            // TODO: unit test
            if (url == null)
            {
                var message = "HackerNewsBaseApi does not exist in Urls config section";
                _logger.LogError(message);
                throw new NullReferenceException(message);
            }

            var version = _configuration.GetValue<string>("HackerNewsApiVersion");

            // TODO: unit test
            if (string.IsNullOrWhiteSpace(version))
            {
                var message = "Version does not exist in configuration";
                _logger.LogError(message);
                throw new NullReferenceException(message);
            }

            return $"{url}/{version}";
        }
    }
}
