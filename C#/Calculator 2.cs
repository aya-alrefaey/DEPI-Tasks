using System;

namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello!\nInput the first number:");
            int x = Convert.ToInt32(Console.ReadLine().ToUpper());
            Console.WriteLine("Input the second number:");
            int y = Convert.ToInt32(Console.ReadLine().ToUpper());
            Console.WriteLine("What do you want to do with those numbers?\n[A]dd\n[S]ubtract\n[M]ultiply");
            string c = Console.ReadLine().ToUpper();
            if (c == "A")
                Console.WriteLine($"{x} + {y} = {x + y}\n");
            else if (c == "S")
                Console.WriteLine($"{x} - {y} = {x - y}\n");
            else if (c == "M")
                Console.WriteLine($"{x} * {y} = {x * y}\n");
            else
                Console.WriteLine("Invalid Option ");
            Console.WriteLine("Press any key to close");
            Console.ReadKey();
        }
    }
}
