namespace MinimalAPICrud.Tests;

public class CatalogItemTests
{
    [Fact]
    public void Constructor_ValidParameters_ShouldSetProperties()
    {
        var item = new CatalogItem("Libro", 10.5m);

        Assert.Equal("Libro", item.Name);
        Assert.Equal(10.5m, item.Price);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Update_InvalidName_ShouldThrowArgumentException(string? invalidName)
    {
        var item = new CatalogItem("Producto", 5m);

        var ex = Assert.Throws<ArgumentException>(() => item.Update(invalidName!, 5m));
        Assert.Equal("Name is required", ex.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Update_InvalidPrice_ShouldThrowArgumentException(decimal invalidPrice)
    {
        var item = new CatalogItem("Producto", 5m);

        var ex = Assert.Throws<ArgumentException>(() => item.Update("Nuevo", invalidPrice));
        Assert.Equal("Price must be greater than zero", ex.Message);
    }

    [Fact]
    public void Update_ValidParameters_ShouldUpdateProperties()
    {
        var item = new CatalogItem("Producto", 5m);
        item.Update("Nuevo", 15m);

        Assert.Equal("Nuevo", item.Name);
        Assert.Equal(15m, item.Price);
    }
}
