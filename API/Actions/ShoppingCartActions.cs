using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace MinimalAPICrud;

public static class ShoppingCartActions
{
    public static async Task<Ok<List<ShoppingCartResponseDto>>> GetAll(AppDbContext db)
        => TypedResults.Ok(await db.ShoppingCarts
            .Select(c => new ShoppingCartResponseDto(c.Id, c.TotalPrice))
            .ToListAsync());

    public static async Task<Results<Ok<ShoppingCartResponseDto>, NotFound>> GetById(
        int id, AppDbContext db)
    {
        var cart = await db.ShoppingCarts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == id);

        return cart is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(new ShoppingCartResponseDto(cart.Id, cart.TotalPrice));
    }

    public static async Task<Created<ShoppingCartResponseDto>> Create(AppDbContext db)
    {
        var cart = new ShoppingCart();
        db.ShoppingCarts.Add(cart);
        await db.SaveChangesAsync();

        return TypedResults.Created(
            $"/shoppingcarts/{cart.Id}",
            new ShoppingCartResponseDto(cart.Id, cart.TotalPrice));
    }

    public static async Task<Results<NoContent, NotFound>> Delete(
        int id, AppDbContext db)
    {
        var cart = await db.ShoppingCarts.FindAsync(id);
        if (cart is null)
            return TypedResults.NotFound();

        db.ShoppingCarts.Remove(cart);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
}
