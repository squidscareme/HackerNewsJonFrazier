using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

using HackerNewsJonFrazier.Core.Models;

namespace HackerNewsJonFrazier.Core.Services.BusinessLogic
{
    internal class StoryService : IStoryService
    {
        private readonly ILogger _logger;
        private readonly IUrlHelper _urlHelper;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _configuration;

        public StoryService(ILogger logger,
            IUrlHelper urlHelper,
            IHttpClientFactory clientFactory,
            IConfiguration configuration)
        {
            _logger = logger;
            _urlHelper = urlHelper;
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public async Task<IEnumerable<Story>> GetStorySummariesAsync()
        {
            IEnumerable<long> newStoryIds = await GetNewStoryIds();

            var stories = await GetStoriesByStoryIds(newStoryIds);

            return stories;
        }

        private async Task<IEnumerable<long>> GetNewStoryIds()
        {
            IEnumerable<long> newStoryIds = new List<long>();
            var httpRequestMessage = CreateRequestGetSummaries();

            var httpClient = _clientFactory.CreateClient();

            var response = await httpClient.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInfo("Retrieved new story ids from HN");

                //new "using" syntax. Woot!
                using var responseStream = await response.Content.ReadAsStreamAsync();
                newStoryIds = await JsonSerializer.DeserializeAsync<IEnumerable<long>>(responseStream);
            }
            else
            {
                _logger.LogError("Error retrieving new story ids from HN");
            }

            return newStoryIds
                .OrderByDescending(x => x)
                .Take(int.Parse( _configuration["MaxNewStories"]));
        }

        private async Task<IEnumerable<Story>> GetStoriesByStoryIds(IEnumerable<long> storyIds)
        {
            var stories = new List<Story>();

            var storyDetailRequestMessages = storyIds.Select(storyId =>
                new HttpRequestMessage(HttpMethod.Get, _urlHelper.GetDetailsUrl(storyId)))
                .ToList();

            var responseMessageTasks = new List<Task<HttpResponseMessage>>();
            foreach (var storyDetailRequestMessage in storyDetailRequestMessages)
            {
                responseMessageTasks.Add(_clientFactory.CreateClient().SendAsync(storyDetailRequestMessage));
            }

            await Task.WhenAll(responseMessageTasks).ContinueWith(async x =>
            {
                var results = await x;
                foreach(var result in results)
                {
                    var str = await result.Content.ReadAsStringAsync();
                    var story = JsonSerializer.Deserialize<Story>(str);
                    stories.Add(story);
                }
            });

            return stories;
        }

        private HttpRequestMessage CreateRequestGetSummaries()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, _urlHelper.SummariesUrl);

            return httpRequestMessage;
        }
    }
}
