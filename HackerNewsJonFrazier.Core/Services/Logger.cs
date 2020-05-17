using System;
namespace HackerNewsJonFrazier.Core.Services
{
    internal class Logger : ILogger
    {
        public void LogInfo(string message)
        {
            LogMessage("Info", message);
        }

        public void LogError(string message)
        {
            LogMessage("Error", message);
        }

        private void LogMessage(string logLevel, string message)
        {
            var formattedMessage = $"{logLevel} | {DateTime.UtcNow} | {message}";

            Console.WriteLine(formattedMessage);
        }
    }
}
