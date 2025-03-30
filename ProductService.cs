using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace Lab2;

public class ProductService
{
    private readonly HttpClient _client = new HttpClient();

    public async Task FetchAndStoreProductsAsync()
    {
        using var context = new Shop();

        var products = await FetchProductsFromApi();
        if (products == null || !products.Any())
        {
            Console.WriteLine("Brak nowych produktów do dodania.");
            return;
        }
        
        var existingCategories = await context.categories.ToListAsync();
        
        var newCategories = new Dictionary<string, Category>();
        foreach (var product in products)
        {
            if (!existingCategories.Any(c => c.Name == product.Category) && 
                !newCategories.ContainsKey(product.Category))
            {
                var category = new Category { Name = product.Category };
                context.categories.Add(category);
                newCategories[product.Category] = category;
            }
        }
        
        if (newCategories.Any())
        {
            await context.SaveChangesAsync();
            existingCategories.AddRange(newCategories.Values);
        }
        
        var addedProducts = 0;
        foreach (var product in products)
        {
            if (await context.products.AnyAsync(p => p.Id == product.Id))
            {
                continue;
            }

            var category = existingCategories.First(c => c.Name == product.Category);
            
            var newProduct = new Product
            {
                Id = product.Id,
                Name = product.Title,
                Price = product.Price,
                CategoryId = category.Id
            };

            context.products.Add(newProduct);
            addedProducts++;
        }

        if (addedProducts > 0)
        {
            await context.SaveChangesAsync();
            Console.WriteLine($"Dodano {addedProducts} nowych produktów.");
        }
        else
        {
            Console.WriteLine("Wszystkie produkty już istnieją w bazie.");
        }
    }

    private async Task<ProductDto[]> FetchProductsFromApi()
    {
        try
        {
            string url = "https://dummyjson.com/products";
            var response = await _client.GetStringAsync(url);
            
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            
            var result = JsonSerializer.Deserialize<ApiResponse>(response, options);
            return result?.Products ?? Array.Empty<ProductDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd pobierania danych: {ex.Message}");
            return Array.Empty<ProductDto>();
        }
    }
    
    public void ShowAllProducts()
    {
        using var context = new Shop();
        var products = context.products.Include(p => p.Category).ToList();

        if (!products.Any())
        {
            Console.WriteLine("Brak produktów w bazie.");
            return;
        }

        Console.WriteLine("Lista wszystkich produktów:");
        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Nazwa: {product.Name}, Cena: {product.Price} zł, Kategoria: {product.Category?.Name ?? "brak"}");
        }
    }

    public void ShowProductsByCategory(string categoryName)
    {
        using var context = new Shop();
        var products = context.products.Include(p => p.Category)
                                       .Where(p => p.Category.Name.ToLower() == categoryName.ToLower())
                                       .ToList();

        if (!products.Any())
        {
            Console.WriteLine($"Brak produktów w kategorii: {categoryName}");
            return;
        }

        Console.WriteLine($"Produkty w kategorii {categoryName}:");
        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Nazwa: {product.Name}, Cena: {product.Price} zł");
        }
    }
}