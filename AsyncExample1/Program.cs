
using System.Threading.Tasks;

namespace DemoApplication1;

public class Program1
{
    public static void Main()
    {
        Console.WriteLine("App Started....");
        SomeProcess();
        Console.WriteLine("App Finished....");
    }

    private static void SomeProcess() 
    {
        TwoProcesses();
        //do other process After terminate tasks  
    }


    private static void TwoProcesses()
    {
        Task task1 = Task.Run(async () =>
        {
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine("Task 1 - iteration {0}", i);
                await Task.Delay(1000);

            }
            Console.WriteLine("Task 1 complete");
        });

        Task task2 = Task.Run(async () =>
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Task 2 - iteration {0}", i);
                await Task.Delay(2000);
            }
            Console.WriteLine("Task 2 complete");
        });

        Console.WriteLine("Waiting for tasks to complete.");

        //solve the tasks here and don't need async await
        //wait for the end of all tasks
        Task.WaitAll(task1, task2);
        ;
        Console.WriteLine("Tasks Completed.");
    }
}