namespace MinimalAPICrud;

public static class ClientEndpoints
{
    public static void MapClientEndpoints(this WebApplication app)
    {
        app.MapGet("/clients", ClientActions.GetAll);
        app.MapGet("/clients/{id}", ClientActions.GetById);
        app.MapPost("/clients", ClientActions.Create);
        app.MapPut("/clients/{id}", ClientActions.Update);
        app.MapDelete("/clients/{id}", ClientActions.Delete);
    }
}
