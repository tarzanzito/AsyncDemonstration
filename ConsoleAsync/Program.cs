
using System.Diagnostics;
using System.Threading.Tasks;

namespace DemoApplication1;

public class Program1
{
    public static async Task Main()
    {
        Console.WriteLine("App Started....");

        //await TwoProcessesV1Async();               //OK mas só quando terminado primeiro await é que fas o segundo await

        await TwoProcessesV2Async();                  //OK - faz os dois await ao mesmo tempo
        //await Task.WhenAll(TwoProcessesV2Async());  //OK
        
        Console.WriteLine("App Finished....");
        Console.ReadKey();
    }

    public static void MainB()
    {
        Console.WriteLine("App Started....");

        TwoProcessesV1Async().Wait() ;//OK mas só quando terminado primeiro await é que fas o segundo await

        TwoProcessesV2Async().Wait(); //OK - faz os dois await ao mesmo tempo

        //Task.WaitAll(TwoProcessesV2Async()); //OK a thread fica parada aqui até concluir
        //Task.Run(async () => { await TwoProcessesV2Async(); }).Wait();// OK too

        Console.WriteLine("App Finished....");
        Console.ReadKey();
    }

    //private async void button1_Click(object sender, EventArgs e)
    private static async Task TwoProcessesV1Async() 
    {
        //neste caso faz primeiro o await1 e quando terminar faz o await2
        //NÃO é multi thread !!!

        Console.WriteLine("dar ordem de fritar batatas");
        string res1 = await FritarBatatasAsync();
        
        Console.WriteLine("dar ordem de cozer ovos");
        string res2 = await CozerOvosAsync();

        Console.WriteLine($"Batatas acabadas e ovos acabados, pode servir prato, {res1}, {res2}");
    }

    //private async void button2_Click(object sender, EventArgs e)
    private static async Task TwoProcessesV2Async()
    {
        //faz os dois ao mesmo tempo
        //é multi thread !!!

        Console.WriteLine("dar ordem de fritar batatas");
        Task<string> t1 = FritarBatatasAsync();

        Console.WriteLine("dar ordem de cozer ovos");
        Task<string> t2 = CozerOvosAsync();

        await t1; //v1 result OK
        await t2; //v1 result OK

        //await Task.WhenAll(t1, t2); //v2 result OK
        //Task.WaitAll(t1, t2);       //block the Window

        string res1 = t1.Result;
        string res2 = t1.Result;

        Console.WriteLine($"Batatas acabadas e ovos acabados, pode servir prato, {res1}, {res2}");
    }

    private static async Task<string> FritarBatatasAsync()
    {
        Console.WriteLine($"FritarBatatas Stared, thread={Thread.CurrentThread.ManagedThreadId}");

        for (int i = 0; i < 20; i++)
        {
            Console.WriteLine($"FritarBatatas passo: {i}");
            await Task.Delay(1000);
        }

        Console.WriteLine("FritarBatatas Finished");

        return "FritarBatatasAsync-Completed";
    }

    private static async Task<string> CozerOvosAsync()
    {
        Console.WriteLine($"CozerOvos Stared, thread={Thread.CurrentThread.ManagedThreadId}");

        for (int i = 0; i < 12; i++)
        {
            Console.WriteLine($"CozerOvos passo: {i}");
            await Task.Delay(1500);
        }

        Console.WriteLine("CozerOvos Finished");

        return "CozerOvosAsync-Completed";
    }


}

//https://www.nuget.org/packages/Nito.AsyncEx
//https://github.com/StephenCleary/AsyncEx
//AsyncContext.Run(() => MainAsync(args));

//https://msmvps.com/blogs/jon_skeet/
