
using System.Threading.Tasks;

namespace DemoApplication3;

//Alguem disse que async Task deveria ser desde o ponto inicial até ao fim  (ponto a ponto)
//ou seja incluir "public static async Task Main()"
//Na minha opinião deve ser apenas ponto a ponto para Winform e afins para que a thread UI continuo livre
//"private async void button1_Click(object sender, EventArgs e)"
//na app console ou APIRes ter awaits em todos metodos parece-me complicar

public class ProgramAsync3
{
    public static async Task Main()
    {
        Console.WriteLine("App Started....");
        await SomeProcessSync();
        Console.WriteLine("App Finished....");
    }

    public static void MainB()
    {
        Console.WriteLine("App Started....");
        Task.WaitAll(SomeProcessSync());  //pára aqui até terminarem as duas tasks
        Console.WriteLine("App Finished....");
    }

    private static async Task SomeProcessSync()
    {
        await TwoProcessesAsync();
        //do other process  
    }

    private static async Task TwoProcessesAsync()
    {
        Task task1 = Task.Run(async () => await Process1Async()); //example4 is more simple
        Task task2 = Task.Run(async () => await Process2Async()); //example4 is more simple


        Console.WriteLine("Waiting for tasks to complete.");

        //await Task.WhenAll(task1, task2); //Version2 OK
        await task1;                        //Version3 OK e roda as duas tasks ao mesmo tempo se a task1 terminar primeiro passa ao await seguinte
        await task2;                        //Version3 OK

        Console.WriteLine($"Tasks Completed.");
    }

    private static async Task Process1Async()
    {
        int i;
        for (i = 0; i < 25; i++)
        {
            await Task.Delay(1000);
            Console.WriteLine("Process 1 - iteration {0}", i);
        }
        Console.WriteLine("Process 1 complete");
    }

    private static async Task Process2Async()
    {
        int i;
        for (i = 0; i < 15; i++)
        {
            await Task.Delay(2000);
            Console.WriteLine("Process 2 - iteration {0}", i);
        }

        Console.WriteLine("Process 2 complete");
    }
}

