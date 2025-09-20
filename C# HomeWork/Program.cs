class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter first number:");

        if (!Int32.TryParse(Console.ReadLine(), out var a))
        {
            Console.WriteLine("Not a number!");
            return;
        }

        Console.WriteLine("Enter second number:");

        if (!Int32.TryParse(Console.ReadLine(), out var b))
        {
            Console.WriteLine("Not a number!");
            return;
        }
    
        Console.WriteLine("Enter operator (&, | or ^):");

        var s1 = Console.ReadLine();

        if (s1.Length != 1 || (s1[0] != '&' && s1[0] != '|' && s1[0] != '^'))
        {
            Console.WriteLine("Error! Wrong operator!");
            return;
        }
     
        int c = 0;

        switch (s1[0])
        {
            case '&':
                c = a & b; 
                break;
            case '|':
                c = a | b; 
                break;
            case '^':
                c = a ^ b; 
                break;
        }

        string Result = Convert.ToString(c, 10);
        string binaryResult = Convert.ToString(c, 2);
        string hexResult = Convert.ToString(c, 16);

        Console.WriteLine("Decimal: " + Result);
        Console.WriteLine("Binary: " + binaryResult);
        Console.WriteLine("Hexadecimal: 0x" + hexResult);
    }
}
