using System;
using HackerNewsJonFrazier.Core.Enums;

namespace HackerNewsJonFrazier.Core.Models
{
    public class StorySummary
    {
        /// <summary>
		/// The item's unique id.
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// The type of item.
		/// </summary>
		public StoryTypes Type { get; set; }

		/// <summary>
		/// The username of the item's author.
		/// </summary>
		public string By { get; set; }

		/// <summary>
		/// The URL of the story.
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// The title of the story, poll or job. HTML.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Creation date of the item, in Unix Time.
		/// </summary>
		public long Time { get; set; }

		/// <summary>
		/// The comment, story or poll text.
		/// </summary>
		/// <remarks>
		/// HTML.
		/// </remarks>
		public string Text { get; set; }
	}
}
