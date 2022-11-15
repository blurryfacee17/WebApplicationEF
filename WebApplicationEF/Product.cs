using Microsoft.EntityFrameworkCore;

namespace WebApplicationEF;

[PrimaryKey("Id")]
public class Product
{
    protected Product()
    {
    }

    public Product(string name,decimal price,decimal amount)
    {
        Name = name;
        Price = price;
        Amount = amount;
    }
    
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
}