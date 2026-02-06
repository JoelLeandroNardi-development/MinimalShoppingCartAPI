using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace MinimalAPICrud;

public static class CatalogItemActions
{
    public static async Task<Ok<List<CatalogItemResponseDto>>> GetAll(AppDbContext db)
        => TypedResults.Ok(await db.CatalogItems
            .Select(i => new CatalogItemResponseDto(i.Id, i.Name, i.Price))
            .ToListAsync());

    public static async Task<Results<Ok<CatalogItemResponseDto>, NotFound>> GetById(
        int id, AppDbContext db)
    {
        var item = await db.CatalogItems.FindAsync(id);
        return item is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(new CatalogItemResponseDto(item.Id, item.Name, item.Price));
    }

    public static async Task<Results<Created<CatalogItemResponseDto>, ValidationProblem>> Create(
        CatalogItemCreateDto dto,
        ICatalogItemValidator validator,
        AppDbContext db)
    {
        var errors = validator.Validate(dto);
        if (errors.Any())
            return TypedResults.ValidationProblem(errors);

        var item = new CatalogItem(dto.Name, dto.Price);
        db.CatalogItems.Add(item);
        await db.SaveChangesAsync();

        return TypedResults.Created(
            $"/catalogitems/{item.Id}",
            new CatalogItemResponseDto(item.Id, item.Name, item.Price));
    }

    public static async Task<Results<NoContent, NotFound, ValidationProblem>> Update(
        int id,
        CatalogItemUpdateDto dto,
        ICatalogItemValidator validator,
        AppDbContext db)
    {
        var item = await db.CatalogItems.FindAsync(id);
        if (item is null)
            return TypedResults.NotFound();

        var errors = validator.Validate(new CatalogItemCreateDto(dto.Name, dto.Price));
        if (errors.Any())
            return TypedResults.ValidationProblem(errors);

        item.Update(dto.Name, dto.Price);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    public static async Task<Results<NoContent, NotFound>> Delete(
        int id, AppDbContext db)
    {
        var item = await db.CatalogItems.FindAsync(id);
        if (item is null)
            return TypedResults.NotFound();

        db.CatalogItems.Remove(item);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
}
