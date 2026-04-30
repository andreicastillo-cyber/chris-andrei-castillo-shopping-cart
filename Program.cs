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

    // Deduct stock
    for (int i = 0; i < cartCount; i++)
        cart[i].Stock -= qty[i];

    cartCount = 0;

    Console.WriteLine("Checkout complete.");
    return;
}
