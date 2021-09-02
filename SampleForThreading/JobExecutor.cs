using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SampleForThreading
{
    class JobExecutor : IJobExecutor
    {
        public int Amount { get; }
        public void Start(int maxConcurrent)
        {
            Task[] taskArray = new Task[maxConcurrent];
            foreach (var task in taskArray)
            {
                task.Start();
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
