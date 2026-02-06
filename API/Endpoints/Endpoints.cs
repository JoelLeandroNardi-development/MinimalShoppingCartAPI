namespace MinimalAPICrud;

public static class Endpoints
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapCatalogItemEndpoints();
        app.MapClientEndpoints();
        app.MapShoppingCartEndpoints();
    }
}
