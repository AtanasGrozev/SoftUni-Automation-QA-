Dictionary<string, List<string>> companyNameAndId = new();

List<string> commands = Console.ReadLine().Split(" ").ToList();

while ( commands[0] != "End")
{

    string nameCompany = commands[0];
    string id = commands[2];
  
    if (companyNameAndId.ContainsKey(nameCompany))
    {

        if (!companyNameAndId[nameCompany].Contains(id)) 
        {
        //If the name is already in the dictionary, add the new id to the existing list
        companyNameAndId[nameCompany].Add(id); // важно да се запомни! 

        }        
    }    
    else
    {
        companyNameAndId.Add(nameCompany, new() { id });
    }
    
    List<string> commandsInWhile = Console.ReadLine().Split(" ").ToList();
    commands = commandsInWhile;
}
foreach (KeyValuePair<string, List<string>> pair in companyNameAndId)
{
    Console.WriteLine($"{pair.Key} \n-- {string.Join("\n-- ", pair.Value)}");

}