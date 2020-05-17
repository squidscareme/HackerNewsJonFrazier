using System;
namespace HackerNewsJonFrazier.Core.Services
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogError(string message);
    }
}
