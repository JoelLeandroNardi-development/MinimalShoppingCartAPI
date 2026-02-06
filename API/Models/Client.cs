namespace MinimalAPICrud;

public class Client
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;

    public List<ShoppingCart> ShoppingCarts { get; private set; } = [];

    private Client() { }

    public Client(string name, string phoneNumber) => Update(name, phoneNumber);

    public void Update(string name, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required");

        Name = name;
        PhoneNumber = phoneNumber;
    }
}
