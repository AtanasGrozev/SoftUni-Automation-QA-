



int number = int.Parse(Console.ReadLine());
int evenDigitSum = 0;


while (number > 0)
{
    for (int i = 0; i < number; i++)
    {
        int digit = number % 10;
        int currentNUm = number / 10;
        number = currentNUm;
        if (digit % 2 == 0)
        {
            int factorial = 1;
            for (int j = 1; j <= digit; j++)
            {
                factorial = factorial * j;

            }
            evenDigitSum = factorial + evenDigitSum;
        }
    }
}
Console.WriteLine(evenDigitSum);