using System.Collections.Generic;

namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello!");
            string c = "";
            while (c!="E")
            {
                Console.WriteLine("What do you want to do ?\n[A]dd\n[S]ubtract\n[M]ultiply\n[E]xit");
                c = Console.ReadLine().ToUpper();
                 if (c == "E")
                {
                    Console.WriteLine("Thank You For Using Our Calculator ...");
                    break;
                }
                else if (c!="A"&& c != "S"&& c != "M")
                {
                    Console.WriteLine("invalid option");
                    continue;
                }
                Console.WriteLine("Input the first number:");
                int x = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Input the second number:");
                int y = Convert.ToInt32(Console.ReadLine());
                if (c == "A")
                    Console.WriteLine($"{x} + {y} = {x + y}\n");
                else if (c == "S")
                    Console.WriteLine($"{x} - {y} = {x - y}\n");
                else if (c == "M")
                    Console.WriteLine($"{x} * {y} = {x * y}\n");
                
            
                   
            }
            
          
        }
    }
}
