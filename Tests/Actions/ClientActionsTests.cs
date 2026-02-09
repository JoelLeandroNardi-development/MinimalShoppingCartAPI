using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MinimalAPICrud.Tests;

public class ClientActionsTests
{
    private static AppDbContext GetDbContextWithData()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var db = new AppDbContext(options);
        db.Clients.Add(new Client("Alice", "123456789"));
        db.Clients.Add(new Client("Bob", "987654321"));
        db.SaveChanges();
        return db;
    }

    [Fact]
    public async Task GetAll_ReturnsAllClients()
    {
        using var db = GetDbContextWithData();
        var result = await ClientActions.GetAll(db);
        Assert.IsType<Ok<List<ClientResponseDto>>>(result);
        var okResult = result as Ok<List<ClientResponseDto>>;
        Assert.Equal(2, okResult.Value!.Count);
    }

    [Fact]
    public async Task GetById_ReturnsClient_WhenExists()
    {
        using var db = GetDbContextWithData();
        var firstClient = db.Clients.First();
        var result = await ClientActions.GetById(firstClient.Id, db);
        Assert.IsType<Ok<ClientResponseDto>>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenNotExists()
    {
        using var db = GetDbContextWithData();
        var result = await ClientActions.GetById(999, db);
        Assert.IsType<NotFound>(result.Result);
    }

    [Fact]
    public async Task Create_ReturnsCreated()
    {
        using var db = GetDbContextWithData();
        var dto = new ClientCreateDto("Charlie", "555555555");
        var result = await ClientActions.Create(dto, db);
        Assert.IsType<Created<ClientResponseDto>>(result);
        var created = result;
        Assert.Equal("Charlie", created.Value!.Name);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenClientDoesNotExist()
    {
        using var db = GetDbContextWithData();
        var dto = new ClientUpdateDto("Updated", "000000000");
        var result = await ClientActions.Update(999, dto, db);
        Assert.IsType<NotFound>(result.Result);
    }

    [Fact]
    public async Task Update_ReturnsNoContent_WhenClientExists()
    {
        using var db = GetDbContextWithData();
        var client = db.Clients.First();
        var dto = new ClientUpdateDto("Updated", "000000000");
        var result = await ClientActions.Update(client.Id, dto, db);
        Assert.IsType<NoContent>(result.Result);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenClientDoesNotExist()
    {
        using var db = GetDbContextWithData();
        var result = await ClientActions.Delete(999, db);
        Assert.IsType<NotFound>(result.Result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenClientExists()
    {
        using var db = GetDbContextWithData();
        var client = db.Clients.First();
        var result = await ClientActions.Delete(client.Id, db);
        Assert.IsType<NoContent>(result.Result);
    }
}
