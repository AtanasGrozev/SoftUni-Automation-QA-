int numbersOfCommands = int.Parse(Console.ReadLine());
Dictionary<string, List<string>> registerSmartDevice = new Dictionary<string, List<string>>();

for (int i = 0; i < numbersOfCommands; i++)
{
    List<string> input = Console.ReadLine().Split(" ").ToList();

    string name = input[1];
    string id = input[2];
    if (input.Contains("register"))

    {
        if (registerSmartDevice.ContainsKey(name))
        {
            if (registerSmartDevice[name].Contains(id))
            {
                Console.WriteLine($"ERROR: {id} is already registered for {name}.");
            }
            else
            {
                registerSmartDevice[name].Add(id);
                Console.WriteLine($"{name} registered {id} successfully.");

            }
        }
        else
        {
            registerSmartDevice.Add(name, new List<string>());
            registerSmartDevice[name].Add(id);
            Console.WriteLine($"{name} registered {id} successfully.");
        }
    }
    if (input.Contains("unregister"))
    {
        if (!registerSmartDevice.ContainsKey(name) || !registerSmartDevice[name].Contains(id))
        {       
            Console.WriteLine($"ERROR: {id} not found for {name}");        

        }
        else if (registerSmartDevice[name].Contains(id))
        {
            registerSmartDevice[name].Remove(id);
            //registerSmartDevice.Remove(name);
          
            Console.WriteLine($"{name} unregistered {id} successfully");
        }

    }
 

}
foreach (var pair in registerSmartDevice)
{
    if (pair.Value.Count > 0)
    {
        Console.WriteLine($"{pair.Key} => {string.Join(" ", pair.Value)}");
    }
  
}
