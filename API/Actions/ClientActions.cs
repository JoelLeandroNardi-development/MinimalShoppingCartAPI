using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace MinimalAPICrud;

public static class ClientActions
{
    public static async Task<Ok<List<ClientResponseDto>>> GetAll(AppDbContext db)
        => TypedResults.Ok(await db.Clients
            .Select(c => new ClientResponseDto(c.Id, c.Name, c.PhoneNumber))
            .ToListAsync());

    public static async Task<Results<Ok<ClientResponseDto>, NotFound>> GetById(
        int id, AppDbContext db)
    {
        var client = await db.Clients.FindAsync(id);
        return client is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(new ClientResponseDto(client.Id, client.Name, client.PhoneNumber));
    }

    public static async Task<Created<ClientResponseDto>> Create(
        ClientCreateDto dto, AppDbContext db)
    {
        var client = new Client(dto.Name, dto.PhoneNumber);
        db.Clients.Add(client);
        await db.SaveChangesAsync();

        return TypedResults.Created(
            $"/clients/{client.Id}",
            new ClientResponseDto(client.Id, client.Name, client.PhoneNumber));
    }

    public static async Task<Results<NoContent, NotFound>> Update(
        int id,
        ClientUpdateDto dto,
        AppDbContext db)
    {
        var client = await db.Clients.FindAsync(id);
        if (client is null)
            return TypedResults.NotFound();

        client.Update(dto.Name, dto.PhoneNumber);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    public static async Task<Results<NoContent, NotFound>> Delete(
        int id, AppDbContext db)
    {
        var client = await db.Clients.FindAsync(id);
        if (client is null)
            return TypedResults.NotFound();

        db.Clients.Remove(client);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }
}
