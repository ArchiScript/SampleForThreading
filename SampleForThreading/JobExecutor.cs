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
        static ConcurrentQueue<Task> tasks = new ConcurrentQueue<Task>();
        public int Amount { get; private set; }
        public void Start(int maxConcurrent)
        {
            
            Task[] taskArray = new Task[Amount];
            foreach (var task in taskArray)
            {
                tasks.Enqueue(task);
            }
            Amount = tasks.Count;
            ParallelOptions opts = new ParallelOptions { MaxDegreeOfParallelism = maxConcurrent };

            Parallel.ForEach(tasks, opts, x => x.Start());
            Thread.Sleep(200);

        }
        public void Stop() { }
        public void Add(Action action)
        {
            tasks.Enqueue(new Task(action));
        }
        public void Clear()
        {
            tasks.Clear();
            Thread.Sleep(100);
        }
    }
}
