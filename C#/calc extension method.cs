namespace tryyy
{
    public delegate double del(double a, double b);
    internal class Program
    {
        static void Main(string[] args)
        {
            double x = 10;
            double y = 5;
            Console.WriteLine($"Operations on {x} and {y}");
            Console.WriteLine($"Adding      : {x.calc(y, (x, y) => x + y)}");
            Console.WriteLine($"Subtraction : {x.calc(y, (x, y) => x - y)}");
            Console.WriteLine($"Multiply    : {x.calc(y, (x, y) => x * y)}");
            Console.WriteLine($"Division    : {x.calc(y, (x, y) => x / y)}");


        }

    }
    public static class double_extension
    {
        public static double calc(this double b, double a, del d)
        {
            return d(b,a);
        }
    }
}
