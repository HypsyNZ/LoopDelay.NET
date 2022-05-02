using System;
using LoopDelay.NET;

namespace LoopTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Test in debug mode

            int test = 0;
            long start = DateTime.UtcNow.Ticks;
            while (Loop.Delay(start, 100, 3000).Result)
            {
                Console.WriteLine(test.ToString());
                test++;
            };

            Console.ReadLine();
        }
    }
}
