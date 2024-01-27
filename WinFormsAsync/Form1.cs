using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //neste caso faz primeiro o await1 e quando terminar faz o await2
            //nao é multi thread !!!

            BlockButtons(true);

            Debug.WriteLine("Init function");

            Debug.WriteLine("dar ordem de fritar batatas");
            Debug.WriteLine("dar ordem de cozer ovos");

            string res1 = await FritarBatatasAsync();
            string res2 = await CozerOvosAsync();

            Debug.WriteLine("Batatas acabadas e ovos acabados, pode servir prato");

            BlockButtons(false);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            //faz os dois ao mesmo tempo

            BlockButtons(true);

            Debug.WriteLine("Init function");

            Debug.WriteLine("dar ordem de fritar batatas");
            Task<string> t1 = FritarBatatasAsync();

            Debug.WriteLine("dar ordem de cozer ovos");
            Task<string> t2 = CozerOvosAsync();

            await t1; //v1 result OK
            await t2; //v1 result OK

            //await Task.WhenAll(t1, t2); //v2 result OK
            //Task.WaitAll(t1, t2); //block the Window

            string res1 = t1.Result;
            string res2 = t1.Result;

            Debug.WriteLine("Batatas acabadas e ovos acabados, pode servir prato");

            BlockButtons(false);
        }

        private void BlockButtons(bool block)
        {
            button1.Enabled = !block;
            button2.Enabled = !block;

            if (block)
                Cursor = Cursors.AppStarting;
            else
                Cursor = Cursors.Default;
        }

        private async Task<string> FritarBatatasAsync()
        {
            Debug.WriteLine($"FritarBatatas Stared, thread={Thread.CurrentThread.ManagedThreadId}");

            for (int i = 0; i < 20; i++)
            {
                Debug.WriteLine($"FritarBatatas passo: {i}");

                this.label3.Text = i.ToString();

                await Task.Delay(1000);
            }

            Debug.WriteLine("FritarBatatas Finished");

            return "FritarBatatasAsync-Completed";
        }

        private async Task<string> CozerOvosAsync()
        {
            Debug.WriteLine($"CozerOvos Stared, thread={Thread.CurrentThread.ManagedThreadId}");

            for (int i = 0; i < 12; i++)
            {
                Debug.WriteLine($"CozerOvos passo: {i}");

                this.label4.Text = i.ToString();

                await Task.Delay(2000);
            }

            Debug.WriteLine("CozerOvos Finished");

            return "CozerOvosAsync-Completed";
        }

 
    }
}


//var tokenSource = new CancellationTokenSource();
//var token = tokenSource.Token;

//public async Task<int> FritarBatatasTask()
//{
//    Task<int> t1 = new Task<int>(async () => 
//    {
//        Debug.WriteLine("FritarBatatas Started");

//        for (int i = 0; i < 10; i++)
//        {
//            Debug.WriteLine($"FritarBatatas passo: {i}");
//            await Task.Delay(1000);
//        }

//        Debug.WriteLine("FritarBatatas Finished");

//        return 100;
//    });

//    Debug.WriteLine("FritarBatatas Order to Start");
//    t1.Start();

//    //Task<int> t1 = Task.Run(() =>
//    //{
//    //    Debug.WriteLine("FritarBatatas Stared");

//    //    for (int i = 0; i < 10; i++)
//    //    {
//    //        Debug.WriteLine($"FritarBatatas passo: {i}");
//    //        Task.Delay(1000).Wait();
//    //    }

//    //    Debug.WriteLine("FritarBatatas Finished");

//    //    return 100;
//    //});

//    return t1.Result;
//}



//public async Task<int> CozerOvosTask()
//{
//    Task<int> t1 = new Task<int>(async () =>
//    {
//        Debug.WriteLine("CozerOvos Started");

//        for (int i = 0; i < 10; i++)
//        {
//            Debug.WriteLine($"CozerOvos passo: {i}");
//            await Task.Delay(2000);
//        }

//        Debug.WriteLine("CozerOvos Finished");

//        return 200;
//    });

//    Debug.WriteLine("CozerOvos Order to Start");
//    t1.Start();

//    return t1;
//}

