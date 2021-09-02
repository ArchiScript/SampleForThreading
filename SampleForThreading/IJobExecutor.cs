using System;
using System.Collections.Generic;
using System.Text;

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
