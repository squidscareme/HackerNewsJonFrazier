using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using HackerNewsJonFrazier.Core.Enums;

namespace HackerNewsJonFrazier.Core.Models
{
    public class Story
	{
		/// <summary>
		/// The item's unique id.
		/// </summary>
        [JsonPropertyName("id")]
		public long Id { get; set; }

		/// <summary>
		/// Whether the item is deleted.
		/// </summary>
        [JsonPropertyName("deleted")]
		public bool? Deleted { get; set; }

		/// <summary>
		/// The type of item.
		/// </summary>
		[JsonPropertyName("type")]
		public string Type { get; set; }

		/// <summary>
		/// The username of the item's author.
		/// </summary>
		[JsonPropertyName("by")]
		public string By { get; set; }

		/// <summary>
		/// Creation date of the item, in Unix Time.
		/// </summary>
		[JsonPropertyName("time")]
		public long Time { get; set; }

		/// <summary>
		/// The comment, story or poll text.
		/// </summary>
		/// <remarks>
		/// HTML.
		/// </remarks>
        [JsonPropertyName("text")]
		public string Text { get; set; }

		/// <summary>
		/// Whether the item is dead.
		/// </summary>
		[JsonPropertyName("dead")]
		public bool? Dead { get; set; }

		/// <summary>
		/// The comment's parent. 
		/// </summary>
		/// <remarks>
		/// Either another comment or the relevant story.
		/// </remarks>
		[JsonPropertyName("parent")]
		public long? Parent { get; set; }

		/// <summary>
		/// The pollopt's associated poll.
		/// </summary>
		[JsonPropertyName("poll")]
		public long? Poll { get; set; }

		/// <summary>
		/// The ids of the item's comments, in ranked display order.
		/// </summary>
		[JsonPropertyName("kids")]
		public List<long> Kids { get; set; }

		/// <summary>
		/// The URL of the story.
		/// </summary>
		[JsonPropertyName("url")]
		public string Url { get; set; }

		/// <summary>
		/// The story's score, or the votes for a pollopt.
		/// </summary>
		[JsonPropertyName("score")]
		public long? Score { get; set; }    

		/// <summary>
		/// The title of the story, poll or job. HTML.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title { get; set; }

		/// <summary>
		/// A list of related pollopts, in display order.
		/// </summary>
		[JsonPropertyName("parts")]
		public List<long> Parts { get; set; }

        /// <summary>
        /// In the case of stories or polls, the total comment count.
        /// </summary>
        [JsonPropertyName("descendents")]
		public List<long> Descendents { get; set; }
	}
}
