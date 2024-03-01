    namespace Boiling_Water
    {
        internal class Program
        {
            static void Main(string[] args)
            {
                string ticketType = Console.ReadLine();
            double studenPrice = 1.00;
            double regularPrice = 1.60;




            if (ticketType == "student")
            {
                Console.WriteLine($"${studenPrice:F2}");
            }
            else if (ticketType == "regular")
            {
                Console.WriteLine($"${regularPrice:F2}");
            }
            else
                Console.WriteLine("Invalid ticket type!");
           
           
            }
        }
    }