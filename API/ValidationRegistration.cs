namespace MinimalAPICrud;

public static class ValidationRegistration
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<ICatalogItemValidator, CatalogItemValidator>();
        return services;
    }
}
