using System;
using System.Threading;

namespace SampleForThreading
{
    class Program
    {
        static void Main(string[] args)
        {/*
            var t = new Thread(PrintX);
            t.Start();
            while (true)
            {
                Console.WriteLine("Y");
            }
        }

        private static void PrintX()
        {
            while (true)
            {
                Console.WriteLine("X");
            }
        }*/
            object locker = new object();
            var figureCalc = new FigureCalculator(locker);
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
            });
    }
}
