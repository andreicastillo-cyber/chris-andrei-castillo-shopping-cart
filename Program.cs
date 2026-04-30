using System;

class Product
{
    public int Id;
    public string Name;
    public double Price;
    public int Stock;

    public double GetTotal(int qty) { return Price * qty; }
}

class Program
{
    static void Main()
    {
        Product[] products = {
            new Product { Id = 1, Name = "Table", Price = 2500, Stock = 60 },
            new Product { Id = 2, Name = "Chair", Price = 500, Stock = 350 },
            new Product { Id = 3, Name = "Table Cloth", Price = 200, Stock = 400 }
        };

        Product[] cart = new Product[10];
        int[] qty = new int[10];
        int cartCount = 0;

        while (true)
        {
            Console.WriteLine("\n=== STORE MENU ===");
            for (int i = 0; i < products.Length; i++)
                Console.WriteLine(products[i].Id + ". " + products[i].Name +
                    " - ₱" + products[i].Price + " (Stock: " + products[i].Stock + ")");

            Console.Write("Enter product number: ");
            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > products.Length)
            {
                Console.WriteLine("Invalid product.");
                continue;
            }

            Console.Write("Enter quantity: ");
            int quantity;
            if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity.");
                continue;
            }

            Product selected = products[choice - 1];

            if (quantity > selected.Stock)
            {
                Console.WriteLine("Not enough stock.");
                continue;
            }

            cart[cartCount] = selected;
            qty[cartCount++] = quantity;

            Console.WriteLine("Item added to cart.");
        }
    }
}
