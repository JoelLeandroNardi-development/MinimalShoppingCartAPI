using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MinimalAPICrud.Tests;

public class CatalogItemActionsTests
{
    private static AppDbContext GetDbContextWithData()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        var db = new AppDbContext(options);
        db.CatalogItems.Add(new CatalogItem("Item1", 10m));
        db.CatalogItems.Add(new CatalogItem("Item2", 20m));
        db.SaveChanges();
        return db;
    }

    [Fact]
    public async Task GetAll_ReturnsAllItems()
    {
        using var db = GetDbContextWithData();
        var result = await CatalogItemActions.GetAll(db);
        Assert.IsType<Ok<List<CatalogItemResponseDto>>>(result);
        var okResult = result;
        Assert.Equal(2, okResult.Value!.Count);
    }

    [Fact]
    public async Task GetById_ReturnsItem_WhenExists()
    {
        using var db = GetDbContextWithData();
        var firstItem = db.CatalogItems.First();
        var result = await CatalogItemActions.GetById(firstItem.Id, db);
        Assert.IsType<Ok<CatalogItemResponseDto>>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenNotExists()
    {
        using var db = GetDbContextWithData();
        var result = await CatalogItemActions.GetById(999, db);
        Assert.IsType<NotFound>(result.Result);
    }

    [Fact]
    public async Task Create_ReturnsValidationProblem_WhenInvalid()
    {
        var db = GetDbContextWithData();
        var validator = new Mock<ICatalogItemValidator>();
        validator.Setup(v => v.Validate(It.IsAny<CatalogItemCreateDto>()))
            .Returns(new Dictionary<string, string[]> { { "Name", new[] { "Required" } } });

        var dto = new CatalogItemCreateDto("", 10m);
        var result = await CatalogItemActions.Create(dto, validator.Object, db);
        Assert.IsType<ValidationProblem>(result.Result);
    }

    [Fact]
    public async Task Create_ReturnsCreated_WhenValid()
    {
        var db = GetDbContextWithData();
        var validator = new Mock<ICatalogItemValidator>();
        validator.Setup(v => v.Validate(It.IsAny<CatalogItemCreateDto>()))
            .Returns(new Dictionary<string, string[]>());

        var dto = new CatalogItemCreateDto("NewItem", 30m);
        var result = await CatalogItemActions.Create(dto, validator.Object, db);
        Assert.IsType<Created<CatalogItemResponseDto>>(result.Result);
    }

    [Fact]
    public async Task Update_ReturnsNotFound_WhenItemDoesNotExist()
    {
        var db = GetDbContextWithData();
        var validator = new Mock<ICatalogItemValidator>();
        validator.Setup(v => v.Validate(It.IsAny<CatalogItemCreateDto>()))
            .Returns(new Dictionary<string, string[]>());

        var dto = new CatalogItemUpdateDto("Updated", 50m);
        var result = await CatalogItemActions.Update(999, dto, validator.Object, db);
        Assert.IsType<NotFound>(result.Result);
    }

    [Fact]
    public async Task Update_ReturnsValidationProblem_WhenInvalid()
    {
        var db = GetDbContextWithData();
        var validator = new Mock<ICatalogItemValidator>();
        validator.Setup(v => v.Validate(It.IsAny<CatalogItemCreateDto>()))
            .Returns(new Dictionary<string, string[]> { { "Name", new[] { "Required" } } });

        var item = db.CatalogItems.First();
        var dto = new CatalogItemUpdateDto("", 50m);
        var result = await CatalogItemActions.Update(item.Id, dto, validator.Object, db);
        Assert.IsType<ValidationProblem>(result.Result);
    }

    [Fact]
    public async Task Update_ReturnsNoContent_WhenValid()
    {
        var db = GetDbContextWithData();
        var validator = new Mock<ICatalogItemValidator>();
        validator.Setup(v => v.Validate(It.IsAny<CatalogItemCreateDto>()))
            .Returns(new Dictionary<string, string[]>());

        var item = db.CatalogItems.First();
        var dto = new CatalogItemUpdateDto("Updated", 50m);
        var result = await CatalogItemActions.Update(item.Id, dto, validator.Object, db);
        Assert.IsType<NoContent>(result.Result);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenItemDoesNotExist()
    {
        var db = GetDbContextWithData();
        var result = await CatalogItemActions.Delete(999, db);
        Assert.IsType<NotFound>(result.Result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenItemExists()
    {
        var db = GetDbContextWithData();
        var item = db.CatalogItems.First();
        var result = await CatalogItemActions.Delete(item.Id, db);
        Assert.IsType<NoContent>(result.Result);
    }
}
