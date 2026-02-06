namespace MinimalAPICrud.Tests;

public class ShoppingCartTests
{
    [Fact]
    public void Constructor_ShouldInitializeEmptyItems()
    {
        var cart = new ShoppingCart();
        Assert.NotNull(cart.Items);
        Assert.Empty(cart.Items);
    }

    [Fact]
    public void TotalPrice_ShouldReturnSumOfItemTotalPrices()
    {
        var item1 = new ShoppingCartItem(new CatalogItem("Libro", 10m), 2);
        var item2 = new ShoppingCartItem(new CatalogItem("Lapiz", 5m), 1);

        var cart = new ShoppingCart();
        cart.ReplaceItems([item1, item2]);

        Assert.Equal(25m, cart.TotalPrice);
    }

    [Fact]
    public void ReplaceItems_ShouldClearAndAddNewItems()
    {
        var item1 = new ShoppingCartItem(new CatalogItem("Libro", 10m), 1);
        var cart = new ShoppingCart();
        cart.ReplaceItems([item1]);

        var item2 = new ShoppingCartItem(new CatalogItem("Lapiz", 5m), 2);
        cart.ReplaceItems([item2]);

        Assert.Single(cart.Items);
        Assert.Equal(item2, cart.Items[0]);
    }
}
