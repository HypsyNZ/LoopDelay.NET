using LoopDelay.NET;
using System;
using System.Threading.Tasks;

namespace LoopTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                // Test Loop Delay with No Await
                await Task.Run(() =>
                {
                    int test = 0;
                    long start = DateTime.UtcNow.Ticks;
                    while (Loop.Delay(start, 85, 1000).Result)
                    {
                        Console.WriteLine("No Await: " + test.ToString());
                        test++;
                    };
                });

                // Test Loop Delay with Await
                await Task.Run(async () =>
                {
                    int test = 0;
                    long start = DateTime.UtcNow.Ticks;
                    while (await Loop.Delay(start, 85, 1000))
                    {
                        Console.WriteLine("Await: " + test.ToString());
                        test++;
                    };
                });

                // Test Loop Delay with Await and Expire Action
                await Task.Run(async () =>
                {
                    int test = 0;
                    long start = DateTime.UtcNow.Ticks;
                    while (await Loop.Delay(start, 85, 500, (() =>
                    {
                        Console.WriteLine("EVENT EXPIRED ACTION");
                    })))
                    {
                        Console.WriteLine("Expire Test: " + test.ToString());
                        test++;
                    };
                });
            });

            Console.ReadLine();
        }
    }
}
