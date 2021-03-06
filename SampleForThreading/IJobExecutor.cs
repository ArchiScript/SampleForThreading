using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SampleForThreading
{
   public interface IJobExecutor
    {

        int Amount { get; }
        void Start(int maxConcurrent);
        void Stop();
        void Add(Action action);
        void Clear();
    }
}
