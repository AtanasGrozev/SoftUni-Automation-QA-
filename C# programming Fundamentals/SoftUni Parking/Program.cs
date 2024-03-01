int numberOfCommands = int.Parse(Console.ReadLine());

Dictionary<string, string> registerUsers = new();

for (int i = 0; i < numberOfCommands; i++)
{
    List<string> commands = Console.ReadLine().Split(" ").ToList();
    //  register Jony AA4132BB

    if (commands.Contains("register"))
    { 
        List<string> nameAndLicense = commands.Skip(1).ToList();
        string name = string.Join(",", nameAndLicense[0]); // IVO[0] 
        string license = string.Join(",", nameAndLicense[1]); // X2688KW[1]


        if (!registerUsers.ContainsKey(name))
        {
            registerUsers.Add(name, license);
            Console.WriteLine($"{name} registered {license} successfully");
        }
        else
        {
            Console.WriteLine($"ERROR: already registered with plate number {nameAndLicense[1]}");
        }
    }
    else if (commands.Contains("unregister"))
    {
        List<string> removeName = commands.Skip(1).ToList();
        string unregistername = removeName[0];
        if (registerUsers.ContainsKey(unregistername))
        {
            registerUsers.Remove(removeName[0]);
            Console.WriteLine($"{unregistername} unregistered successfully");
        }
        else
        {
            Console.WriteLine($"ERROR: user {unregistername} not found");
        }
    }
}
foreach (KeyValuePair<string, string> user in registerUsers)
{
    Console.WriteLine($"{user.Key} => {user.Value}");

}
