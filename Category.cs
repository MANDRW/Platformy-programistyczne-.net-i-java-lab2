namespace Lab2;

public class Category
{
    public int Id { get; set; }
    public String Name { get; set; }
    public List<Product> Products { get; set; }= new List<Product>();
    
}