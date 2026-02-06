namespace MinimalAPICrud;

public static class CatalogItemEndpoints
{
    public static void MapCatalogItemEndpoints(this WebApplication app)
    {
        app.MapGet("/catalogitems", CatalogItemActions.GetAll);
        app.MapGet("/catalogitems/{id}", CatalogItemActions.GetById);
        app.MapPost("/catalogitems", CatalogItemActions.Create);
        app.MapPut("/catalogitems/{id}", CatalogItemActions.Update);
        app.MapDelete("/catalogitems/{id}", CatalogItemActions.Delete);
    }
}
