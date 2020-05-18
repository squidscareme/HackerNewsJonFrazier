using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using HackerNewsJonFrazier.Core.Models;
using HackerNewsJonFrazier.Core.Services.BusinessLogic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HackerNewsJonFrazier.Api.Controllers
{
    [Route("api/stories")]
    public class StoryController : Controller
    {
        private readonly IStoryService _storyService = null;

        public StoryController(IStoryService storyService)
        {
            _storyService = storyService;
        }

        // GET: api
        // returns Task<IEnumerable<Story>>
        [HttpGet]
        [Route("summaries")]
        //public async Task<IEnumerable<Story>> Get()
        public async Task<ActionResult<Story>> Get()
        {
            var stories = await _storyService.GetStorySummariesAsync();

            if (stories == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(stories);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
