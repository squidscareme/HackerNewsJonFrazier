using System;
namespace HackerNewsJonFrazier.Core.Services
{
    public interface IUrlHelper
    {
        string SummariesUrl { get; }
        string GetDetailsUrl(long storyId);
    }
}
