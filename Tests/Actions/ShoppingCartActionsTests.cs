using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MinimalAPICrud.Tests;

public class ShoppingCartActionsTests
{
    private static AppDbContext GetDbContextWithData()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var db = new AppDbContext(options);
        db.ShoppingCarts.Add(new ShoppingCart());
        db.ShoppingCarts.Add(new ShoppingCart());
        db.SaveChanges();
        return db;
    }

    [Fact]
    public async Task GetAll_ReturnsAllCarts()
    {
        using var db = GetDbContextWithData();
        var result = await ShoppingCartActions.GetAll(db);
        Assert.IsType<Ok<List<ShoppingCartResponseDto>>>(result);
        var okResult = result;
        Assert.Equal(2, okResult.Value!.Count);
    }

    [Fact]
    public async Task GetById_ReturnsCart_WhenExists()
    {
        using var db = GetDbContextWithData();
        var firstCart = db.ShoppingCarts.First();
        var result = await ShoppingCartActions.GetById(firstCart.Id, db);
        Assert.IsType<Ok<ShoppingCartResponseDto>>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenNotExists()
    {
        using var db = GetDbContextWithData();
        var result = await ShoppingCartActions.GetById(999, db);
        Assert.IsType<NotFound>(result.Result);
    }

    [Fact]
    public async Task Create_ReturnsCreated()
    {
        using var db = GetDbContextWithData();
        var result = await ShoppingCartActions.Create(db);
        Assert.IsType<Created<ShoppingCartResponseDto>>(result);
        var created = result as Created<ShoppingCartResponseDto>;
        Assert.NotNull(created.Value);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenCartDoesNotExist()
    {
        using var db = GetDbContextWithData();
        var result = await ShoppingCartActions.Delete(999, db);
        Assert.IsType<NotFound>(result.Result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenCartExists()
    {
        using var db = GetDbContextWithData();
        var cart = db.ShoppingCarts.First();
        var result = await ShoppingCartActions.Delete(cart.Id, db);
        Assert.IsType<NoContent>(result.Result);
    }
}
