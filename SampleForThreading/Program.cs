using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;


namespace SampleForThreading
{
    //public delegate void Action();

    class Program

    {   public static Dictionary<int, Action> concDict = new Dictionary<int, Action>();
        //public static Action actiondel;
        public static ConcurrentQueue<Task> tasks = new ConcurrentQueue<Task>();
        //public delegate void Action();
        static void Main(string[] args)
        {
            
            //  object locker = new object();
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

            /* long[] arr = new long[100000000];
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

             Console.ReadLine();*/




            Test test = new Test();
            Action run = test.Print;
            //Action u = () => Console.WriteLine("djsklfj");
            //actiondel = test.Print;
            /*Task tsk = new Task(run);
            Task tsk1 = new Task(test.Print);*/
            //Task tsk2 = new Task(u);
            /*tsk.Start();
            tsk1.Start();
            // tsk.Wait();
            tsk1.Wait();
            //Thread.Sleep(50);
            Task.WaitAny(tsk, tsk1);*/
            //Parallel.Invoke(Meth1, Meth2);

            //Task[] tasksArr = new Task[6];

            //ConcurrentQueue<Task> tasks = new ConcurrentQueue<Task>();

            /*for (int i = 0; i < tasksArr.Length; i++)
            {
                tasksArr[i] = new Task(test.Print);
                tasks.Enqueue(tasksArr[i]);
            }*/
            /*foreach (var item in tasksArr)
            {
                Parallel.Invoke(Meth3, Meth2, Meth1, run);
            }*/
            //tasks.Enqueue(tsk2);


            //tasks.Enqueue(new Task(() => Console.WriteLine("hiyou2")));
            //actiondel += () => Console.WriteLine($"New Action works {Task.CurrentId}");
            //AddMeth(run);
            //AddMeth(() => Meth3());
            AddMeth(() => Console.WriteLine($"ура {Task.CurrentId}"));
            AddMeth(() => Console.WriteLine($"охренеть"));
            //Parallel.ForEach(tasks, new ParallelOptions { MaxDegreeOfParallelism = 6 }, x => run());

            Parallel.For(0, tasks.Count, x => concDict[(int)Task.CurrentId]());
            


        }
        static void Meth1()
        {
            Console.WriteLine("Выполеняется метод1");
        }
        static void Meth2()
        {
            Console.WriteLine("Выполеняется метод2");
        }

        static void Meth3()
        {
            Console.WriteLine($"Выполняется метод 3");
        }
        public static void AddMeth(Action action)
        {

            var a = action;
            tasks.Enqueue(new Task(action));
            concDict.Add((int)Task.CurrentId, action);
            //actiondel += action;
            
        }


    }
    public class Test
    {
        public void Print()
        {
            Console.WriteLine($"Action works {Task.CurrentId}");
        }

    }
}
