namespace MinimalAPICrud;

public class CatalogItemValidator : ICatalogItemValidator
{
    public IDictionary<string, string[]> Validate(CatalogItemCreateDto dto)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(dto.Name))
            errors["Name"] = ["Name is required"];

        if (dto.Price <= 0)
            errors["Price"] = ["Price must be greater than zero"];

        return errors;
    }
}
