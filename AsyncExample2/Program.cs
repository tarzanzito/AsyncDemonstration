
namespace DemoApplication2;

//Alguem disse que async Task deveria ser desde o ponto inicial até ao fim  (ponto a ponto)
//ou seja incluir "public static async Task Main()"
//Na minha opinião deve ser apenas ponto a ponto para Winform e afins para que a thread UI continue livre
//"private async void button1_Click(object sender, EventArgs e)"
//na app console ou APIRes ter awaits em todos metodos parece-me complicar

public class ProgramAsync2
{
    public static async Task Main()
    {
        Console.WriteLine("App Started....");
        await SomeProcessAsync();  //pára aqui até terminarem as duas tasks
        Console.WriteLine("App Finished....");
    }

    public static void MainB()
    {
        Console.WriteLine("App Started....");
        Task.WaitAll(SomeProcessAsync());  //pára aqui até terminarem as duas tasks
        Console.WriteLine("App Finished....");
    }

    private static async Task SomeProcessAsync() 
    {
        await TwoProcessesAsync();
        //do other process  
    }

    private static async Task TwoProcessesAsync()
    {
        Task<string> task1 = Task<string>.Run(async () =>
        {
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine("Task 1 - iteration {0}", i);
                await Task.Delay(1000);

            }
            Console.WriteLine("Task 1 complete");
            return "task 1: 15";
        });

        Task<string> task2 = Task<string>.Run(async () =>
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Task 2 - iteration {0}", i);
                await Task.Delay(2500);
            }
            Console.WriteLine("Task 2 complete");

            return "task 2: 10";
        });

        Console.WriteLine("Waiting for tasks to complete.");

        //await Task.WhenAll(task1, task2); //Version1 OK

        await task1;                        //Version2 OK, in debug it stays on this line running both tasks at the same time.
                                            //if task1 finishes first, it goes to the next await
        await task2;                        //Version2 OK

        //only moves to this line when both tasks finish (Version1 or Version2)
        string res1 = task1.Result;        
        Console.WriteLine("Result Task 1 obtained");

        string res2 = task2.Result;
        Console.WriteLine("Result Task 2 obtained");

        Console.WriteLine($"Tasks Completed.: {res1} - {res2}");
    }
}