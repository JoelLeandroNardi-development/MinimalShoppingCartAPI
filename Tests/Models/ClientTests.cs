namespace MinimalAPICrud.Tests;

public class ClientTests
{
    [Fact]
    public void Constructor_ValidParameters_ShouldSetProperties()
    {
        var client = new Client("Juan", "123456789");

        Assert.Equal("Juan", client.Name);
        Assert.Equal("123456789", client.PhoneNumber);
        Assert.NotNull(client.ShoppingCarts);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Update_InvalidName_ShouldThrowArgumentException(string? invalidName)
    {
        var client = new Client("Pedro", "987654321");

        var ex = Assert.Throws<ArgumentException>(() => client.Update(invalidName!, "987654321"));
        Assert.Equal("Name is required", ex.Message);
    }

    [Fact]
    public void Update_ValidParameters_ShouldUpdateProperties()
    {
        var client = new Client("Pedro", "987654321");
        client.Update("Ana", "555555555");

        Assert.Equal("Ana", client.Name);
        Assert.Equal("555555555", client.PhoneNumber);
    }

    [Fact]
    public void ShoppingCarts_ShouldBeInitialized()
    {
        var client = new Client("Luis", "111222333");
        Assert.NotNull(client.ShoppingCarts);
        Assert.Empty(client.ShoppingCarts);
    }
}
