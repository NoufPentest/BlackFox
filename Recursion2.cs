using System;

namespace ConsoleApplication6
{
    class Recursion2
    {
        private static char[] c = { '0', '9', 'a', 'z' };

        static void Main(string[] args)
        {
            Method1(c[0], c[0]);
            Console.Read();
        }
        
        static void Method1(char a, char b)
        {
            if (b > c[3])
            {
                if (a < c[1] || (a > c[1] && a < c[3]))
                    Method1(NextChar(a), c[0]);
                else if (a == c[3])
                    return;
                else
                    Method1(c[2], c[0]);
            }
                
            
            else if (b > c[1] && b < c[2])
                Method1(a, c[2]);

            else
            {
                Console.WriteLine(a + "" + b);
                Method1(a, NextChar(b));
            }
        }
        
        static char NextChar(char x)
        {
            return (char)((int)x + 1);
        }

    }
}