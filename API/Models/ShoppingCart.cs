namespace MinimalAPICrud;

public class ShoppingCart
{
    public int Id { get; private set; }
    public List<ShoppingCartItem> Items { get; private set; } = [];

    public decimal TotalPrice => Items.Sum(i => i.TotalPrice);

    public void ReplaceItems(IEnumerable<ShoppingCartItem> items)
    {
        Items.Clear();
        Items.AddRange(items);
    }
}
