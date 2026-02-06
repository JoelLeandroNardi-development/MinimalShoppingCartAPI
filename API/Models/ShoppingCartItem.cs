namespace MinimalAPICrud;

public class ShoppingCartItem
{
    public int Id { get; private set; }
    public CatalogItem Item { get; private set; } = default!;
    public int Quantity { get; private set; }

    public decimal TotalPrice => Item.Price * Quantity;

    private ShoppingCartItem() { }

    public ShoppingCartItem(CatalogItem item, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero");

        Item = item;
        Quantity = quantity;
    }
}
