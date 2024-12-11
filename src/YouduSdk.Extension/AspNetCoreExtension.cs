using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YouduSdk.Common;

namespace YouduSdk.Extension;

public static class AspNetCoreExtension
{
    public static IServiceCollection AddYdSdk(this IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }
    public static IServiceCollection AddYdSdk(this IServiceCollection services,ApiOptions options)
    {
        return services;
    }
    
}
