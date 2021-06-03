using System;
using System.Runtime.Serialization.Formatters;
using System.Threading;

namespace Experiments
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press Enter to begin");
            Console.ReadKey();
            var tw = new ThreadWorker<int>(Count);

            var result = tw.Start(200);
            
            Console.WriteLine($"result: {result}");
            
            Console.WriteLine($"Ошибки: {tw.Exception}\nУспех: {tw.IsSuccess}\nПровал:{tw.IsFaulted}");
        }

        public static int Count(object n)
        {
            var num = (int) n;
            var result = 0;
            for (var i = 0; i < num; i++)
            {
                result += i;
                Console.Write("-");
                Thread.Sleep(10);
            }

            return result;
        }
    }
}
