namespace MinimalAPICrud;

public interface ICatalogItemValidator
{
    IDictionary<string, string[]> Validate(CatalogItemCreateDto dto);
}
