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
    static int receiptNo = 1;

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

            
            int existingIndex = -1;
            int currentQty = 0;

            for (int i = 0; i < cartCount; i++)
            {
                if (cart[i].Id == selected.Id)
                {
                    existingIndex = i;
                    currentQty = qty[i];
                    break;
                }
            }

            int totalRequested = currentQty + quantity;

            if (totalRequested > selected.Stock)
            {
                Console.WriteLine("Not enough stock!");
                continue;
            }

            if (existingIndex != -1)
                qty[existingIndex] += quantity;
            else
            {
                cart[cartCount] = selected;
                qty[cartCount++] = quantity;
            }

            Console.WriteLine("Item added to cart.");

            
            {
                Console.WriteLine("\n=== NEXT ACTION ===");
                Console.WriteLine("[A] Add More Items");
                Console.WriteLine("[V] View Cart / Checkout");
                Console.WriteLine("[Q] Quit Program");
                Console.Write("Select option: ");

                string action = Console.ReadLine().ToUpper();

                if (action == "A")
                {
                    break; 
                }
                else if (action == "V")
                {
                    while (true)
                    {
                        Console.WriteLine("\n=== YOUR SHOPPING CART ===");
                        Console.WriteLine("1. View Items");
                        Console.WriteLine("2. Update Quantity");
                        Console.WriteLine("3. Remove Item");
                        Console.WriteLine("4. Clear Cart");
                        Console.WriteLine("5. Proceed to Checkout");
                        Console.Write("Choose option: ");

                        int op;
                        if (!int.TryParse(Console.ReadLine(), out op)) continue;

                        if (op == 1)
                        {
                            if (cartCount == 0)
                            {
                                Console.WriteLine("Cart is empty.");
                                continue;
                            }

                            for (int i = 0; i < cartCount; i++)
                                Console.WriteLine((i + 1) + ". " + cart[i].Name + " x" + qty[i]);
                        }
                        else if (op == 2)
                        {
                            Console.Write("Item #: ");
                            int i;
                            if (!int.TryParse(Console.ReadLine(), out i) || i < 1 || i > cartCount) continue;
                            i--;

                            Console.Write("New quantity: ");
                            int nq;
                            if (!int.TryParse(Console.ReadLine(), out nq) || nq <= 0) continue;

                            if (nq > cart[i].Stock)
                            {
                                Console.WriteLine("Not enough stock.");
                                continue;
                            }

                            qty[i] = nq;
                            Console.WriteLine("Updated.");
                        }
                        else if (op == 3)
                        {
                            Console.Write("Item #: ");
                            int i;
                            if (!int.TryParse(Console.ReadLine(), out i) || i < 1 || i > cartCount) continue;
                            i--;

                            for (; i < cartCount - 1; i++)
                            {
                                cart[i] = cart[i + 1];
                                qty[i] = qty[i + 1];
                            }
                            cartCount--;

                            Console.WriteLine("Removed.");
                        }
                        else if (op == 4)
                        {
                            cartCount = 0;
                            Console.WriteLine("Cart cleared.");
                        }
                        else if (op == 5)
                        {
                            double total = 0;

                            Console.WriteLine("\n=== FINAL RECEIPT ===");
                            for (int i = 0; i < cartCount; i++)
                            {
                                double sub = cart[i].GetTotal(qty[i]);
                                total += sub;
                                Console.WriteLine(cart[i].Name + " x" + qty[i] + " = ₱" + sub);
                            }

                            double discount = total >= 5000 ? total * 0.10 : 0;
                            double finalTotal = total - discount;

                            Console.WriteLine("Subtotal: ₱" + total);
                            Console.WriteLine("Discount: ₱" + discount);
                            Console.WriteLine("Final Total: ₱" + finalTotal);

                            double payment;
                            while (true)
                            {
                                Console.Write("Enter payment: ");
                                if (double.TryParse(Console.ReadLine(), out payment) && payment >= finalTotal)
                                    break;

                                Console.WriteLine("Insufficient payment.");
                            }

                            Console.WriteLine("Change: ₱" + (payment - finalTotal));
                            Console.WriteLine("Receipt #: " + receiptNo++);
                            Console.WriteLine("Date: " + DateTime.Now);

                            
                            for (int i = 0; i < cartCount; i++)
                                cart[i].Stock -= qty[i];

                            cartCount = 0;

                            Console.WriteLine("Checkout complete.");
                            return;
                        }
                    }
                }
                else if (action == "Q")
                {
                    Console.WriteLine("Exiting program...");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                }
            }
        }
    }
}
