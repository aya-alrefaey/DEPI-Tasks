using System.Runtime.CompilerServices;

namespace session_try
{
    public delegate double del(double num);
    internal class Program
    {
        static void Main(string[] args)
        {
            double x = 10;
            double y = 5;
            Console.WriteLine($"Operations on {x} and {y}");
            Console.WriteLine($"Adding      : {x.calc(y, s => x + y)}");
            Console.WriteLine($"Subtraction : {x.calc(y, s => x - y)}");
            Console.WriteLine($"Multiply    : {x.calc(y, s => x * y)}");
            Console.WriteLine($"Division    : {x.calc(y, s => x / y)}");

        }
       
    }
    public static class double_extension
    {
        public static double calc(this double b, double a, del d)
        {
            return d(a);
        }
    }
}
