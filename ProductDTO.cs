namespace Lab2;

public class ProductDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }

    public string Name => Title;
}