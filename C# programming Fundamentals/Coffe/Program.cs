static void TotalPrice(string product, int quantity)
{   

    double result = 0;

    if (product == "coffee")
    {
        double priceCoffee = 1.50;
        result = quantity * priceCoffee;
        Console.WriteLine($"{result:F2}");

    }
    else if (product == "water")
    {
        double priceWater = 1.00;
        result = quantity * priceWater;
        Console.WriteLine($"{result:F2}");

    }
    else if (product == "coke")
    {
        double priceCoke = 1.40;
        result = quantity * priceCoke;
        Console.WriteLine($"{result:F2}");

    }
    else if (product == "snacks")
    {
        double priceSnacks = 2.00;
        result = quantity * priceSnacks;
        Console.WriteLine($"{result:F2}");

    }
}
string product = Console.ReadLine();
int quantity = int.Parse(Console.ReadLine());
TotalPrice(product, quantity);  
