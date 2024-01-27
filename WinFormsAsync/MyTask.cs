using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncExample1
{
    //alguem me disse que era preferivel Task.Run em vez de new Task + task.Start()
 
    internal class MyTasks
    {
        #region public

        public string MyCounter(int total, int pause)
        {
            Console.WriteLine($"MyTask.Counter1:BEFORE START");
            string ret = Counter(total, pause);
            Console.WriteLine($"MyTask.Counter1:AFTER START");
            return ret;
        }

        #endregion

        #region public async

        public async Task DoSomeThing()
        {
            await Task.Delay(3000);
            Console.WriteLine("DoSomeThing Completed");
        }

        public async Task<string> MyCounterRunAsync(int total, int pause, CancellationToken token)
        {
            Console.WriteLine($"    MyTask.MyCounterRunAsync: Before RUN: {total.ToString("00")}:{pause.ToString("0000")}");

            Task<string> task = null;
            try
            {
                task = Task<string>.Run(async () => await CounterAsync(total, pause, token));
            }
            catch (Exception ex)
            {
            string aa = ex.Message; 
            }
            
            Console.WriteLine($"    MyTask.MyCounterRunAsync: After  RUN: {total.ToString("00")}:{pause.ToString("0000")}");

            return await task;
        }

        //public async Task<string> MyCounter1StartAsync(int total)
        //{
        //    Task<string> task = Task.Factory.StartNew(()=> Counter1b(total), CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);


        //   // Task<string> task = new Task<string>(() => await Counter1(total));
        //    Console.WriteLine($"MyTask.Counter 1 Async:BEFORE START");
        //    //task.Start();
        //    //Console.WriteLine($"MyTask.Counter 1 Async:AFTER START");

        //   //Task<string> task = Task<string>.Run(async () => await Counter1(total));
        //    //Console.WriteLine($"    MyTask.MyCounter1Async: RUN");

        //    return await task;
        //}


        #endregion

        #region private

        private async Task<string> CounterAsync(int total, int pause, CancellationToken token)
        {
            Console.WriteLine($"        MyTask.Counter: START LOOP: {total.ToString("00")}:{pause.ToString("0000")}");

            try
            {
                int i;
                for (i = 0; i < total; i++)
                {
                    token.ThrowIfCancellationRequested();

                    //if (token.IsCancellationRequested)
                    //{
                    //    return $"CounterAsync: Is Canceled";
                    //}

                  //  try
                  //  {
                        await Task.Delay(pause, token); //.ContinueWith(tsk => tsk.Exception == default);
                  //  }
                  //  catch { }

                    Console.WriteLine($"        MyTask.Counter: {total.ToString("00")}:{pause.ToString("0000")} - {i.ToString()}");
                }

                Console.WriteLine($"        MyTask.Counter: END LOOP: {total.ToString("00")}:{pause.ToString("0000")}");

                return $"CounterAsync: Total processed: {i.ToString()}";
            }
            catch(Exception ex)
            {
            string aa=ex.Message;
                return $"CounterAsync";
            }

        }

        private string Counter(int total, int pause)
        {
            int i;
            for (i = 0; i < total; i++)
            {
                Task.Delay(pause).Wait();
                Console.WriteLine($"MyTask.MyCounter:{i.ToString()}");

            }
            return $"Counter1: Total processed: {i.ToString()}";
        }



        //private async Task<string> Counter2(int total)
        //{
        //    Console.WriteLine($"        MyTask.Counter2:START");
        //    int i = 0;
        //    for (i = 0; i < total; i++)
        //    {
        //        await Task.Delay(TimeSpan.FromMilliseconds(2000));
        //        Console.WriteLine($"        MyTask.Counter2:{i.ToString()}");

        //    }
        //    Console.WriteLine($"        MyTask.Counter2:END");
        //    return $"Counter2: Total processed: {i.ToString()}"; i.ToString();
        //}

        //private string Counter2b(int total)
        //{
        //    int i = 0;
        //    for (i = 0; i < total; i++)
        //    {
        //        Task.Delay(TimeSpan.FromMilliseconds(5000)).Wait();
        //        Console.WriteLine($"    MyTask.MyCounter2:{i.ToString()}");

        //    }
        //    return $"Counter2: Total processed: {i.ToString()}"; ;
        //}

        #endregion
    }
}
