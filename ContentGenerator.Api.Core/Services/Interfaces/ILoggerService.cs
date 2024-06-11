namespace ContentGenerator.Api.Core.Services.Interfaces
{
    namespace ContentGenerator.Api.Core.Services.Interfaces
    {
        public interface ILoggerService<T>
        {
            void LogInformation(string message);
            void LogWarning(string message);
            void LogError(Exception ex, string message);
        }
    }

}
