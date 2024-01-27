
using System.Threading.Tasks;

namespace DemoApplication5;

//Alguem disse que async Task deveria ser desde o ponto inicial até ao fim  (ponto a ponto)
//ou seja incluir "public static async Task Main()"
//Na minha opinião deve ser apenas ponto a ponto para Winform e afins para que a thread UI continuo livre
//"private async void button1_Click(object sender, EventArgs e)"
//na app console ou APIRes ter awaits em todos metodos parece-me complicar

public class ProgramAsync4
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
        //do other process  
    }

    private static void TwoProcesses()
    {
        Task task1 = Task.Run(Process1);
        Task task2 = Task.Run(Process2);


        Console.WriteLine("Waiting for tasks to complete.");

        //task1;                        //Version3 OK e roda as duas tasks ao mesmo tempo se a task1 terminar primeiro passa ao await seguinte
        //task2;                        //Version3 OK
        Task.WaitAll(task1, task2); //Version2 OK

        Console.WriteLine($"Tasks Completed.");
    }

    private static void Process1()
    {
        int i;
        for (i = 0; i < 25; i++)
        {
            Thread.Sleep(1000);
            Console.WriteLine("Process 1 - iteration {0}", i);
        }
        Console.WriteLine("Process 1 complete");

    }

    private static void Process2()
    {
        int i;
        for (i = 0; i < 15; i++)
        {
            Thread.Sleep(2000);
            Console.WriteLine("Process 2 - iteration {0}", i);
        }

        Console.WriteLine("Process 2 complete");
    }
}

