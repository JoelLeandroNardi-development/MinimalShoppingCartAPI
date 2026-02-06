namespace MinimalAPICrud;

public static class ShoppingCartEndpoints
{
    public static void MapShoppingCartEndpoints(this WebApplication app)
    {
        app.MapGet("/shoppingcarts", ShoppingCartActions.GetAll);
        app.MapGet("/shoppingcarts/{id}", ShoppingCartActions.GetById);
        app.MapPost("/shoppingcarts", ShoppingCartActions.Create);
        app.MapDelete("/shoppingcarts/{id}", ShoppingCartActions.Delete);
    }
}
