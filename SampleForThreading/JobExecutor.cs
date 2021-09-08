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
        public static bool stopAdd;
        public CancellationTokenSource cancellationTokenSource;
        public ConcurrentQueue<Task> tasks = new ConcurrentQueue<Task>();
        public int Amount { get; private set; }
        public void Start(int maxConcurrent)
        {
            Amount = tasks.Count;
            cancellationTokenSource = new CancellationTokenSource();
            ParallelOptions opts = new ParallelOptions
            {
                MaxDegreeOfParallelism = maxConcurrent,
                CancellationToken = cancellationTokenSource.Token
            };

            if (tasks.Count > 0)
            {

                Parallel.ForEach(tasks, opts, x =>
              {
                  if (x.Status == TaskStatus.Created)
                  {
                      //opts.CancellationToken.ThrowIfCancellationRequested();

                      x.Start();
                      Console.WriteLine($" {x.Id}  __________ {x.Status}.. stop = {stopAdd} ");
                      Console.WriteLine($" {x.Id}  __________ {x.Status}.. stop = {stopAdd} \n");
                  }
              });

                Parallel.ForEach(tasks, opts, x =>
                {
                    if (x.Status == TaskStatus.RanToCompletion)
                    {
                        tasks.TryDequeue(out x);
                        Console.WriteLine($" {x.Id} {x.Status}...  в очереди {tasks.Count}  stop = {stopAdd}");

                    }
                });
            }
        }
        public void Stop()
        {
            stopAdd = true;
            Console.WriteLine(" -- STOP --");
            Console.WriteLine($" stop = {stopAdd}");
            var ar = tasks.ToArray();
            Start(6);
            Task.WaitAll(ar);
            //cancellationTokenSource.Cancel();
        }
        public void Add(Action action)
        {
            var rand = new Random();
            if (stopAdd == false)
            {
                Task newTask = new Task(action);
                tasks.Enqueue(newTask);
                Console.WriteLine($"\n  Задание {newTask.Id} добавлено в очередь ______ {newTask.Status}, количество заданий в очереди {tasks.Count}");
                
                Start(6); Thread.Sleep(rand.Next(100, 600));
            }
           
        }
        public void Clear()
        {
            tasks.Clear();
        }
    }
}
