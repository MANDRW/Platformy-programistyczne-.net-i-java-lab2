namespace Lab2;

public class Product
{
    public int Id{ get; set; }
    public string Name{ get; set; }
    public decimal Price{ get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Price: {Price},Category: {Category.Name}";
    }
    
}