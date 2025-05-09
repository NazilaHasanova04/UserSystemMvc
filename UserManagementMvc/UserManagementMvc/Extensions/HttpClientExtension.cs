namespace UserManagementMvc.Extensions
{
    public static class HttpClientExtension
    {
        public static void AddHttpClientConfigs(this IServiceCollection services)
        {
            services.AddHttpClient("MyClient", client =>
            {
                client.Timeout = TimeSpan.FromMinutes(5);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });
        }
    }
}
