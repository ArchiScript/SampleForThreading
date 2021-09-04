using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;


namespace SampleForThreading
{
    //public delegate void Action();

    class Program
    {
        //public static ConcurrentQueue<Task> tasks = new ConcurrentQueue<Task>();

        static void Main(string[] args)
        {

            object locker = new object();

            //========= ДЗ Генерировать и считать среднее арифм =====================

            long[] arr = new long[100000000];
            long[] arr2 = new long[10000000];
            long sum = 0;
            decimal average;

            // ------------- реализация 1 

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i;
            }

            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
            }

            average = sum / arr.Length;


            Console.WriteLine($"среднее арифметическое {sum} / {arr.Length} = {average}");

            //------------- реализация 2 с многопоточностью


            ThreadPool.QueueUserWorkItem(_ =>
            {
                lock (locker)
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        arr[i] = i;
                    }
                }

                Thread.Sleep(1000);
            });
            ThreadPool.QueueUserWorkItem(_ =>
            {
                lock (locker)
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        sum += arr[i];
                    }
                }
               
                average = sum / arr.Length;
                Console.WriteLine($"среднее арифметическое {sum} / {arr.Length} = {average}");
            });
            Thread.Sleep(1000);
            //Console.ReadLine();


            //Parallel.ForEach(tasks, new ParallelOptions { MaxDegreeOfParallelism = 6 }, x => x.Start());

            //==================== JobExecutor вызов ==========================================

            var executor = new JobExecutor();

            executor.Add(() => Console.WriteLine("Восхищение!"));

            for (int i = 0; i < 5; i++)
            {
                executor.Add(() => Console.WriteLine($"Вперед товарищи, это задание {Task.CurrentId}"));
            }
            // executor.Start(8);
            Console.WriteLine($"На данный момент в очереди {executor.Amount} задач");
            executor.Clear();


            executor.Add(() => Console.WriteLine("Восхищение!"));

            executor.Start(8);
            Console.WriteLine($"На данный момент в очереди {executor.Amount} задач");
        }

    }
}
