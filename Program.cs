using Microsoft.EntityFrameworkCore;

namespace Lab2;

class Program
{
    static async Task Main()
    {
        var productService = new ProductService();

        while (true)
        {
            Console.WriteLine("\nMENU:");
            Console.WriteLine("1 - Pobierz produkty z API i zapisz do bazy");
            Console.WriteLine("2 - Wyświetl wszystkie produkty");
            Console.WriteLine("3 - Wyczysc baze danych");
            Console.WriteLine("4 - Wyświetl produkty według kategorii");
            Console.WriteLine("5 - Dodaj produkt");
            Console.WriteLine("6 - Wyjście");
            Console.Write("Wybierz opcję: ");
            
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await productService.FetchAndStoreProductsAsync();
                    Console.WriteLine("Dane zaktualizowane.");
                    break;
                case "2":
                    productService.ShowAllProducts();
                    break;
                case "3":
                    productService.DeleteAllProducts();
                    break;
                case "4":
                    Console.Write("Podaj nazwę kategorii: ");
                    string category = Console.ReadLine();
                    productService.ShowProductsByCategory(category);
                    break;
                case "5":
                    Console.Write("Podaj nazwę produktu: ");
                    string name = Console.ReadLine();
                    Console.Write("Podaj cenę produktu: ");
                    string priceInput = Console.ReadLine();
                    Console.Write("Podaj kategorię produktu: ");
                    string categoryInput = Console.ReadLine();
                    if (decimal.TryParse(priceInput, out decimal price))
                    {
                        productService.AddProduct(name, price, categoryInput);
                        Console.WriteLine("Produkt dodany.");
                    }
                    else
                    {
                        Console.WriteLine("Nieudało się dodać produktu. Sprawdź poprawnosc danych.");
                    }
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Niepoprawny wybór, spróbuj ponownie.");
                    break;
            }
        }
    }
}