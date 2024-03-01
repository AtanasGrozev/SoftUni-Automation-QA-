using System.Diagnostics.CodeAnalysis;
using System.Formats.Asn1;
using System.Transactions;

namespace Speed_Info
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string type = Console.ReadLine();

            if (type == "square")
            {
                double a = double.Parse(Console.ReadLine());
                double b = double.Parse(Console.ReadLine());
                double area = a * b;
                Console.WriteLine($"{area:f2}");
            }
            else if (type == "rectangle")
            {
                double a = double.Parse(Console.ReadLine());
                double b = double.Parse(Console.ReadLine());
                double area = a * b;
                Console.WriteLine($"{area:f2}"); ;

            }
            else if(type == "circle")
            {
                double rad = double.Parse(Console.ReadLine());
                double area = rad * rad * Math.PI;
                Console.WriteLine($"{area:F2}");
            }
            else 
                    {
                Console.WriteLine("Invalid figure.");
            }

        }
    }
}