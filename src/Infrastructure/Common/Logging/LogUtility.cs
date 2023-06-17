using Serilog;

namespace Infrastructure.Common.Logging
{
    public static class LogUtility
    {
        public static void LogRequest(string? host, string? path, string? queryString, object? body, object? header)
        {
            Log.Information(LogTemplates.Request, LogTemplates.HostName, host, path, queryString, body, header);
        }

        public static void LogResponse(string? host, string? path, int? httpStatusCode, object? body)
        {
            Log.Information(LogTemplates.Response, LogTemplates.HostName, host, path, httpStatusCode, body);
        }

        public static void LogInformation(string? className, string? methodName, object? extraData)
        {
            Log.Information(LogTemplates.Information, LogTemplates.HostName, className, methodName, extraData);
        }

        public static void LogError(Exception ex)
        {
            Log.Error(ex, LogTemplates.Exception, LogTemplates.HostName);
        }
    }
}
