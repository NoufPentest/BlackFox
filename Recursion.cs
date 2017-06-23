using System;

namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {
            Method1(1);
            Method2(55);
            Console.Read();
        }

        static void Method1(int i)
        {
            if (i == 56) return;
            Console.WriteLine(i);
            Method1(i + 1);
        }
        static void Method2(int i)
        {
            if (i == 0) return;
            Console.WriteLine(i);
            Method2(i - 1);
        }
    }
}
