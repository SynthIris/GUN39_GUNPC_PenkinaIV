class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter first number:");

        if (!Int32.TryParse(Console.ReadLine(), out var firstNumber))
        {
            Console.WriteLine("Not a number!");
            return;
        }

        Console.WriteLine("Enter second number:");

        if (!Int32.TryParse(Console.ReadLine(), out var secondNumber))
        {
            Console.WriteLine("Not a number!");
            return;
        }
    
        Console.WriteLine("Enter operator (&, | or ^):");

        string operatorInput = Console.ReadLine();

        if (operatorInput.Length != 1)
        {
            Console.WriteLine("Error! Please enter exactly one character");
            return;
        }
     
        int result = 0;

        switch (operatorInput[0])
        {
            case '&':
                result = firstNumber & secondNumber; 
                break;
            case '|':
                result = firstNumber | secondNumber; 
                break;
            case '^':
                result = firstNumber ^ secondNumber; 
                break;
            default:
                Console.WriteLine("Error! Wrong operator. Please use &, | or ^");
                return;
        }

        string Result = Convert.ToString(result, 10);
        string binaryResult = Convert.ToString(result, 2);
        string hexResult = Convert.ToString(result, 16);

        Console.WriteLine("Decimal: " + Result);
        Console.WriteLine("Binary: " + binaryResult);
        Console.WriteLine("Hexadecimal: 0x" + hexResult);
    }
}
