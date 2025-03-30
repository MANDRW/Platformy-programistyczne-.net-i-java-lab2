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
            Console.WriteLine("3 - Wyświetl produkty według kategorii");
            Console.WriteLine("4 - Wyjście");
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
                    Console.Write("Podaj nazwę kategorii: ");
                    string category = Console.ReadLine();
                    productService.ShowProductsByCategory(category);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Niepoprawny wybór, spróbuj ponownie.");
                    break;
            }
        }
    }
}