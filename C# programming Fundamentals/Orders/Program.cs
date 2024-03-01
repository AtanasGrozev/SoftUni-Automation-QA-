Dictionary<string, List<double>> products = new();
string input = Console.ReadLine();

while (input != "buy")
{
    string[] inputArray = input.Split();
    string product = inputArray[0];
    double price = double.Parse(inputArray[1]);
    double quantity = double.Parse(inputArray[2]);

    if (!products.ContainsKey(product))
    {
        products.Add(product, new List<double>());
        products[product].Add(price);
        products[product].Add(quantity);


    }
    else
    {
        products[product][0] = price;
        products[product][1] += quantity;
    }

    input = Console.ReadLine();
}

foreach (KeyValuePair<string, List<double>> currentProduct in products)
{
    string currentProductname = currentProduct.Key;
    double currentProductPrice = currentProduct.Value[0];
    double currentProductQuantity = currentProduct.Value[1];

    double currentProductAmount = currentProductPrice * currentProductQuantity;

    Console.WriteLine($"{currentProductname} -> {currentProductAmount:F2}");

}