int number = int.Parse(Console.ReadLine());


bool noPrimeNumber = true;




for (int i = 1; i <= number; i++)
{
    int currentNumber = i;
    int digitSum = 0;
    bool IsAllDigitsPrime = true;


    while (currentNumber > 0)
    {
        int digit = currentNumber % 10;
        currentNumber = currentNumber / 10;
        bool isDigitPrime = digit == 2 || digit == 3 || digit == 5 || digit == 7;
        if (isDigitPrime)
        {
            digitSum = digitSum + digit;
        }

        else
        {
            IsAllDigitsPrime = false;
            break;
        }
    }
    if (IsAllDigitsPrime && digitSum % 2 == 0)
    {
        Console.Write(i + " ");
        noPrimeNumber = false;
    }
}
if (noPrimeNumber)
{
    Console.WriteLine("no");
}




