namespace Infrastructure.Common.Logging
{
    public static class LogTemplates
    {
        public static readonly string Request = "Request InstanceName: {@HostName}, Host: {@Host}, Path: {@Path}, QueryString: {@QueryString}, Body: {@Body}, Header: {@Header}";
        public static readonly string Response = "Response InstanceName: {@HostName}, Host: {@Host}, Path: {@Path}, HttpStatus: {@HttpStatus}, Body: {@Body}";
        public static readonly string Exception = "InstanceName: {@HostName}";
        public static readonly string Information = "InstanceName: {@HostName}, ClassName: {@ClassName}, MethodName: {@MethodName}, ExtraData: {@ExtraData}";
        public static string? HostName => Environment.GetEnvironmentVariable("HOSTNAME");
    }
}
