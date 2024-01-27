using System;
using System.Threading.Tasks;

namespace AsyncExample6
{
    internal static class Program5
    {
        // https://www.youtube.com/watch?v=LdkoxF_2k8Y 
        // https://www.youtube.com/watch?v=u8VkhwV0LLk 

        //https://www.youtube.com/watch?v=TKc5A3exKBQ
        //https://www.youtube.com/watch?v=F9_8MJbsnzg
        //https://www.youtube.com/watch?v=zQMNFEz5IVU
        //https://www.youtube.com/watch?v=O1Tx-k4Vao0  
        //https://www.youtube.com/watch?v=b5dyPJ3zyRg

        public static async Task Main()
        {
            Console.WriteLine($"MyTask Created");
            var myTasks = new MyTasks();

            Console.WriteLine($"Call Counter 1 Async");
            Task<string> task1 = myTasks.MyCounterRunAsync(10, 1000);

            Console.WriteLine($"Call Counter 2 Async");
            Task<string> task2 = myTasks.MyCounterRunAsync(8, 2500);

            string res1 = await task1; //OK1
            Console.WriteLine($"A task1 finished");
            string res2 = await task2; //OK1
            Console.WriteLine($"A task2 finished");

            //await Task.WhenAll(task1, task2); OK2

            Console.WriteLine($"Final Counter 1 Async:{res1}");
            Console.WriteLine($"Final Counter 2 Async:{res2}");
        }

        public static void MainB()
        {
            Console.WriteLine($"MyTask Created");
            var myTasks = new MyTasks();

            Console.WriteLine($"Call Counter 1 Async");
            Task<string> task1 = myTasks.MyCounterRunAsync(10, 1000);

            Console.WriteLine($"Call Counter 2 Async");
            Task<string> task2 = myTasks.MyCounterRunAsync(8, 2500);

            Task.WaitAll(task1, task2); //OK

            string res1 = task1.Result; //OK1
            Console.WriteLine($"A task1 finished");
            string res2 = task2.Result; //OK1
            Console.WriteLine($"A task2 finished");


            Console.WriteLine($"Final Counter 1 Async:{res1}");
            Console.WriteLine($"Final Counter 2 Async:{res2}");
        }

    }
}