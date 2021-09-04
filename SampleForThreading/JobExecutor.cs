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
        static ConcurrentQueue<Task> tasksQueue = new ConcurrentQueue<Task>();
        public int Amount { get; }
        public void Start(int maxConcurrent)
        {

            Task[] taskArray = new Task[Amount];
            foreach (var task in taskArray)
            {
                tasksQueue.Enqueue(task);
            }

            foreach (var t in tasksQueue)
            {
                t.Start();
            }
            ParallelOptions opts = new ParallelOptions { MaxDegreeOfParallelism = maxConcurrent };
            /* Parallel.For(0, opts, )
             {

             }*/


        }
        public void Stop()
        {
        }
        public void Add(Action action)
        {
            //tasksQueue.Enqueue(new Task(action));
        }
        public void Clear()
        {

        }
    }
}
