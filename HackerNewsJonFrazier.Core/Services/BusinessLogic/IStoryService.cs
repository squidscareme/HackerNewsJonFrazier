using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using HackerNewsJonFrazier.Core.Models;

namespace HackerNewsJonFrazier.Core.Services.BusinessLogic
{
    public interface IStoryService
    {
        Task<IEnumerable<Story>> GetStorySummariesAsync();
    }
}