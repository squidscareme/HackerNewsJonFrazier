using System;
using System.Diagnostics.CodeAnalysis;

namespace HackerNewsJonFrazier.Core.Services
{
    internal class Logger : ILogger
    {
        // NOTE: This class does nothing interesting right now so exclude from test coverage.
        [ExcludeFromCodeCoverage]
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
