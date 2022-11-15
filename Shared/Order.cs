namespace Shared;

public class Order
{
    public int Id { get; set; }
    public List<Product> Products { get; set; }
    public User User { get; set; }
}