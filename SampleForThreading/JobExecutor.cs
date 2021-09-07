using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace SampleForThreading
{
    class JobExecutor : IJobExecutor
    {
        public CancellationTokenSource cancellationTokenSource;
        public static ConcurrentQueue<Task> tasks = new ConcurrentQueue<Task>();
        public int Amount { get; private set; }
        public void Start(int maxConcurrent)
        {


            ParallelOptions opts = new ParallelOptions { MaxDegreeOfParallelism = maxConcurrent };
            cancellationTokenSource = new CancellationTokenSource();
            opts.CancellationToken = cancellationTokenSource.Token;

            try
            {
                Parallel.ForEach(tasks, opts, x =>
                {
                    if (x.Status == TaskStatus.Created)
                    {
                        x.Start(); Console.WriteLine($"{x.Id} Started ..");
                        opts.CancellationToken.ThrowIfCancellationRequested();
                    }

                });
            }
            catch (OperationCanceledException e)
            {

                Console.WriteLine(e.Message);
            }
            finally
            {
                cancellationTokenSource.Dispose();
            }


            Parallel.ForEach(tasks, opts, x =>
            {
                if (x.Status == TaskStatus.RanToCompletion)
                {
                    Thread.Sleep(200);
                    tasks.TryDequeue(out x);
                    Console.WriteLine($"{x.Id} Complete.");
                }
            });


        }
        public void Stop() {
            /*Parallel.ForEach(tasks, null, x =>
            {
                if (x.Status != TaskStatus.RanToCompletion)
                {
                    x.Dispose() ;              
                }

            });*/
            cancellationTokenSource.Cancel();
        }
        public void Add(Action action)
        {
            var rand = new Random();
            Task newTask = new Task(action);
            tasks.Enqueue(newTask);
            Console.WriteLine($"Задание добавлено в очередь, количество заданий в очереди {tasks.Count}");
            Thread.Sleep(rand.Next(400, 2000));
            Start(6);
        }
        public void Clear()
        {
            tasks.Clear();

        }
    }
}
