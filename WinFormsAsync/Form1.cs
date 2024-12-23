using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        CancellationTokenSource? _cancellationTokenSource;

        public Form1()
        {
            InitializeComponent();
        }

        #region events

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Task.Run(() => {
                //NOTE:inside Tasks we cannot make changes in UI components(generate exception);
                //     use invoke
                //     example Text1.Invoke( () => Text1.Text= "xpto");

            } //, cancellationToken
            );

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _cancellationTokenSource?.Cancel();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //TODO: verificar se ainda n�o foi feito o dispose ou n�o � null
            _cancellationTokenSource?.Cancel();
        }

        private async void buttonSequencial_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = _cancellationTokenSource.Token;

            BlockButtons(true);

            //In this case, process 1 is carried out first and only when this is finished does process 2 begin.
            //Neste caso, o processo 1 � executado primeiro e somente quando este � finalizado � que o processo 2 come�a.
            //Is NOT multi thread !!!

            try
            {

                Debug.WriteLine("Send order to begin Process1");
                Debug.WriteLine("Send order to begin Process2");

                Debug.WriteLine("before line Process1");
                string res1 = await ProcessOneAsync(cancellationToken);

                Debug.WriteLine("before line Process2");
                string res2 = await ProcessTwoAsync(cancellationToken);

                Debug.WriteLine("Processe1 and Process2 finished.");

            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("Tasks cancelled or time cancelled.");
            }
            finally
            {
                _cancellationTokenSource?.Dispose();
            }

            BlockButtons(false);
        }

        private async void buttonSameTime_Click(object sender, EventArgs e)
        {

            _cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = _cancellationTokenSource.Token;

            BlockButtons(true);

            //Both at same time

            try
            {
                Debug.WriteLine("Send order to Process One Begin");
                Task<string> t1 = ProcessOneAsync(cancellationToken);

                Debug.WriteLine("Send order to Process Two Begin");
                Task<string> t2 = ProcessTwoAsync(cancellationToken);

                //https://www.youtube.com/watch?v=gW19LaAYczI

                //WHY both same time ?!?!??!!
                await t1; //v1 result OK - Run all at same time!!!!!!!!!!!!!!! ?!??!?!
                await t2; //v1 result OK - Run all at same time !!!!!!!!!!!!!! ?!?!??! 

                //await Task.WhenAll(t1, t2); //OK - Run all at same time 
                //Task.WaitAll(t1, t2);       //NOT ok - block the WinForm !!!!!!!

                string res1 = t1.Result;
                string res2 = t2.Result;

                Debug.WriteLine("Proces One and Process Two finished");

            }
            catch (OperationCanceledException)
            {
                Debug.WriteLine("Tasks cancelled or time cancelled.");
            }
            finally
            {
                _cancellationTokenSource.Dispose();
            }

            BlockButtons(false);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource?.Cancel();
        }

        private async void buttonExample_Click(object sender, EventArgs e)
        {
            TimeSpan timeout = new TimeSpan(0, 0, 25);

            //class variavel because is needed in event form.cancel and form.closeing or closed
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenSource.CancelAfter(timeout);


            BlockButtons(true);

            try
            {
                Debug.WriteLine("Example Started.");

                var cancellationToken = _cancellationTokenSource.Token;

                //Task t1 = this.ExempleWithCancellationAsync(_cancellationTokenSource.Token);
                //await t1;

                //or

                await ExempleWithCancellationAsync(cancellationToken);

                Debug.WriteLine("Example Complited.");

            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine("Tasks cancelled: timed cancelled." + ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                _cancellationTokenSource?.Dispose();
                BlockButtons(false);
            }
        }

        private void buttonTestUnlocked_Click(object sender, EventArgs e)
        {
            Guid guid = Guid.NewGuid();
            textBox1.Text = guid.ToString();
        }

        #endregion

        #region Visual handler 

        private void BlockButtons(bool block)
        {
            buttonSequencial.Enabled = !block;
            buttonSameTime.Enabled = !block;
            buttonCancel.Enabled = block;
            buttonExample.Enabled = !block;

            if (block)
                Cursor = Cursors.AppStarting;
            else
                Cursor = Cursors.Default;
        }

        #endregion

        #region Async processes

        private async Task<string> ProcessOneAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine($"Process One Started, thread={Thread.CurrentThread.ManagedThreadId}");

            for (int i = 1; i <= 25; i++)
            {
                Debug.WriteLine($"\"Process One Step: {i}");

                label3.Text = i.ToString();

                await Task.Delay(500, cancellationToken);
            }

            Debug.WriteLine("Process One Finished");

            return "Process One Completed";
        }

        private async Task<string> ProcessTwoAsync(CancellationToken cancellationToken)
        {
            Debug.WriteLine($"Process Two Stared, thread={Thread.CurrentThread.ManagedThreadId}");

            for (int i = 1; i <= 15; i++)
            {
                Debug.WriteLine($"Process Two Step: {i}");

                this.label4.Text = i.ToString();

                //Note: In case of _cancellationTokenSource.Cancel
                //      the Task.Delay generate a Exception 
                await Task.Delay(1000, cancellationToken); 
            }

            Debug.WriteLine("Process Two Finished");

            return "Process Two Completed";
        }

        private async Task ExempleWithCancellationAsync(CancellationToken cancellationToken = default)
        {
            //https://stackoverflow.com/questions/16063520/how-do-you-create-an-asynchronous-method-in-c
            //Best away to method async in various net searchs , reconfirm !!!
            //await Task.Yield();

            Debug.WriteLine($"Example: START");

            //try
            //{

            for (int i = 0; i < 1000; i++)
            {
                Debug.WriteLine($"Example: {i}");

                cancellationToken.ThrowIfCancellationRequested();
                //or
                //if (cancellationToken.IsCancellationRequested)
                //    throw new TaskCanceledException(task);
                //or
                if (cancellationToken.IsCancellationRequested)
                    break;

                //Note: In case of _cancellationTokenSource.Cancel
                //      the Task.Delay generate a Exception
                await Task.Delay(1000, cancellationToken);

                //simple way to turn async method 
                //await Task.Run(() =>
                //{
                ////cancellationToken.ThrowIfCancellationRequested();
                //}, cancellationToken);  //do nothing
            }

            //}
            //catch (OperationCanceledException ex) 
            //{
            //    if (cancellationToken.IsCancellationRequested)
            //        throw;
            //    else
            //        throw;
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}

            Debug.WriteLine($"Example: END");
        }

        #endregion
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

