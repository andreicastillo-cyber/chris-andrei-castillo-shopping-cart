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

// CART MENU
Console.WriteLine("\n=== CART MENU ===");
Console.WriteLine("1.View 2.Update 3.Remove 4.Clear");
Console.Write("Choose: ");
