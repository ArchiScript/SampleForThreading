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

        private static bool stopAdd;
        public CancellationTokenSource cancellationTokenSource;
        public ConcurrentQueue<Task> tasks = new ConcurrentQueue<Task>();
        public int Amount { get; private set; }
        public void Start(int maxConcurrent)
        {
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

                        x.Start(); Console.WriteLine($"{x.Id} Executing ..");
                      Console.WriteLine($"_____________{x.Status}");
                      
                  }
              });
                              
                Parallel.ForEach(tasks, opts, x =>
                {
                    if (x.Status == TaskStatus.RanToCompletion)
                    {

                        tasks.TryDequeue(out x);
                        Console.WriteLine($"{x.Id} Complete...  в очереди {tasks.Count}");

                    }
                });
               
            }

        }
        public void Stop()
        {
            stopAdd = true;
            
            //cancellationTokenSource.Cancel();
        }
        public void Add(Action action)
        {
            if (stopAdd == false)
            {
                var rand = new Random();
                Task newTask = new Task(action);
                tasks.Enqueue(newTask);
                Console.WriteLine($" Задание {newTask.Id} добавлено в очередь, количество заданий в очереди {tasks.Count}");

               Start(5);
                Thread.Sleep(rand.Next(400, 1600));
            }
            
        }
        public void Clear()
        {
            tasks.Clear();
        }
    }
}
