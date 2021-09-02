using System;
using System.Threading;


namespace SampleForThreading
{
    class Program
    {
        static void Main(string[] args)
        {
            object locker = new object();
            /*var figureCalc = new FigureCalculator(locker);
            figureCalc.PrintFigures();
            ThreadPool.QueueUserWorkItem(_ =>
            {
                for (int i = 0; i < 100; i++)
                {
                    lock (locker)
                    {
                        figureCalc.figures.Add(new Figure { SideCount = i });
                    }
                    Thread.Sleep(2000);
                }
            });*/


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
                Thread.Sleep(1000);
                average = sum / arr.Length;
                Console.WriteLine($"среднее арифметическое {sum} / {arr.Length} = {average}");
            });

            Console.ReadLine();



        }
    }
}
