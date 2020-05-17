using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HackerNewsJonFrazier.Core.Models;

namespace HackerNewsJonFrazier.Core.Services.BusinessLogic
{
    public interface IStoryService
    {
        Task<IList<StorySummary>> GetStorySummariesAsync();
    }
}


/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HackerNewsJonFrazier.Core.Models;

namespace HackerNewsJonFrazier.Core.Services.BusinessLogic
{
    internal class StoryService : IStoryService
    {
        public Task<IList<StorySummary>> GetStorySummariesAsync()
        {
            var dummyItems = new List<StorySummary>()
            {
                new StorySummary()
				{
                    Id = 123,
                    Title = "A fart to remember",
                    By = "Tootie",
                    Url = "https://duckduckgo.com",
                    Type = Core.Enums.StoryTypes.Story,
                    Text = "This is the text of the story about a turd smell",
                    Time = DateTime.Now.Ticks

				},
                new StorySummary()
                {
                    Id = 987,
                    Title = "Todd's Big Day Out",
                    By = "Tootie",
                    Url = "https://google.com",
                    Type = Core.Enums.StoryTypes.Story,
                    Text = "This is the text of the story about Todd",
                    Time = DateTime.Now.Ticks - 7576

                }
            };
            return Task.FromResult<IList<StorySummary>>(dummyItems);
        }
    }
}

     */
