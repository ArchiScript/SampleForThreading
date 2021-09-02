using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SampleForThreading
{
    class SampleForDeadlock
    {
        public void GetDeadlockSample()
        {
            var locker1 = new object();
            var locker2 = new object();

            ThreadPool.QueueUserWorkItem(_ =>
            {
                lock (locker1)
                {
                    Thread.Sleep(1000);

                    lock (locker2)
                    {
                        Console.WriteLine("I lock Loccer2");
                    }
                }
            });

            lock (locker2)
            {
                Thread.Sleep(1000);
                lock (locker1)
                {
                    Console.WriteLine("I lock Loccer1");
                }
            }

        }
    }
}

