namespace SmokeZeroDigitalProject.Helpers
{
    public static class ApiEndpoints
    {
        public const string Login = "/api/Auth/login";
    }

    public class ApiConfig
    {
        private readonly IConfiguration _configuration;

        public ApiConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string BaseUrl => _configuration["ApiSettings:BaseUrl"] ?? string.Empty;

        public string GetEndpoint(string endpoint) => $"{BaseUrl}{endpoint}";
    }
}
