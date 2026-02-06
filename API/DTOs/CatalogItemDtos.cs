namespace MinimalAPICrud;

public record CatalogItemCreateDto(string Name, decimal Price);
public record CatalogItemUpdateDto(string Name, decimal Price);
public record CatalogItemResponseDto(int Id, string Name, decimal Price);
