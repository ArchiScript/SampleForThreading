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
        public int Amount { get; }
        public void Start(int maxConcurrent)
        {
            /*Task[] taskArray = new Task[maxConcurrent];
            foreach (var task in taskArray)
            {
                task.Start();
            }
            ConcurrentQueue<Task> tasks = new ConcurrentQueue<Task>();
            foreach (var t in tasks)
            {
                t.Start();
            }*/
            Semaphore semaphore = new Semaphore(0, maxConcurrent);

            for (int i = 1; i < Amount; i++)
            {
                Thread t = new Thread(new ParameterizedThreadStart(???));
                t.Start(i);
            }



        }
        public void Stop()
        {
        }
        public void Add(Action action)
        {
        }
        public void Clear()
        {
           
        }
    }
}
