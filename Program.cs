using System;
using System.Collections.Generic;

class Product
{
    public int Id;
    public string Name;
    public double Price;
    public int RemainingStock;

    public void DisplayProduct()
    {
        Console.WriteLine(Id + ". " + Name + " - ₱" + Price + " (Stock: " + RemainingStock + ")");
    }

    public bool HasEnoughStock(int quantity)
    {
        return quantity <= RemainingStock;
    }

    public void DeductStock(int quantity)
    {
        RemainingStock -= quantity;
    }
}

class CartItem
{
    public Product Product;
    public int Quantity;

    public double GetTotal()
    {
        return Product.Price * Quantity;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Product[] products = new Product[]
        {
            new Product { Id = 1, Name = "Table", Price = 2500, RemainingStock = 60 },
            new Product { Id = 2, Name = "Chair", Price = 500, RemainingStock = 350 },
            new Product { Id = 3, Name = "Table Cloth", Price = 200, RemainingStock = 400 }
        };

        List<CartItem> cart = new List<CartItem>();
        char choice = 'Y';

        while (choice == 'Y')
        {
            Console.WriteLine("\n=== PRODUCT MENU ===");
            foreach (Product p in products)
            {
                p.DisplayProduct();
            }

            Console.Write("\nSelect product (1-3): ");
            int productChoice;
            if (!int.TryParse(Console.ReadLine(), out productChoice) ||
                productChoice < 1 || productChoice > products.Length)
            {
                Console.WriteLine("Invalid product choice!");
                continue;
            }

            Product selected = products[productChoice - 1];

            Console.Write("Enter quantity: ");
            int quantity;
            if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity!");
                continue;
            }

            // Find existing item manually (safer than Find for beginners)
            CartItem existingItem = null;
            foreach (CartItem item in cart)
            {
                if (item.Product.Id == selected.Id)
                {
                    existingItem = item;
                    break;
                }
            }

            int totalRequested = quantity;
            if (existingItem != null)
            {
                totalRequested += existingItem.Quantity;
            }

            if (!selected.HasEnoughStock(totalRequested))
            {
                Console.WriteLine("Not enough stock!");
                continue;
            }

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                CartItem newItem = new CartItem();
                newItem.Product = selected;
                newItem.Quantity = quantity;
                cart.Add(newItem);
            }

            Console.WriteLine("\nAdded: " + selected.Name + " x" + quantity);

            Console.Write("Add another item? (Y/N): ");
            choice = Char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();
        }

        // Compute total
        double grandTotal = 0;
        foreach (CartItem item in cart)
        {
            grandTotal += item.GetTotal();
        }

        // Receipt
        Console.WriteLine("\n=== RECEIPT ===");
        foreach (CartItem item in cart)
        {
            Console.WriteLine(item.Product.Name + " x" + item.Quantity + " = ₱" + item.GetTotal());
        }

        Console.WriteLine("Subtotal: ₱" + grandTotal);

        // Discount
        double discount = 0;
        if (grandTotal >= 5000)
        {
            discount = grandTotal * 0.10;
            Console.WriteLine("Discount (10%): ₱" + discount);
        }

        double finalTotal = grandTotal - discount;
        Console.WriteLine("Final Total: ₱" + finalTotal);

        // Deduct stock AFTER checkout
        foreach (CartItem item in cart)
        {
            item.Product.DeductStock(item.Quantity);
        }

        // Show updated stock
        Console.WriteLine("\n=== UPDATED STOCK ===");
        foreach (Product p in products)
        {
            Console.WriteLine(p.Name + " - Remaining: " + p.RemainingStock);
        }

        Console.WriteLine("\nThank you for your purchase!");
    }
}
