﻿using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;


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



            //==================== Regex =============================


            string input = "http://ya.ru/api?r=1&x=23";
            string pattern = @"\?((\w+?={1}.+)\&?)*";

            Match match = Regex.Match(input, pattern);
            Console.WriteLine(match.Value);
            string subpattern = @"\?";
            var repMatch = Regex.Replace(match.Value, subpattern, "");
            Console.WriteLine(repMatch);
            var paramsArr = repMatch.Split("&");
            Console.WriteLine($"{paramsArr[0]} \n{paramsArr[1] }");

            //+373 77767852 / 0 373 77767852 / 00 373 77767852
            string numberPattern = @"^(\+|(00)|0)\s?((373)|\(373\))\s?(\({1})?77(5|7|8|9){1}(\){1})?\s?([0-9]\s?){5}$";
            //77767852, 0 (777) 67852)
            string numberPattern2 = @"^0?\s?(\({1})?77(5|7|8|9){1}(\){1})?\s?([0-9]\s?){5}$";

            string phoneNum1 = "+373 777 74567";
            string phoneNum2 = "0 777 74567";
            string phoneNum3 = "00 (373) 777 74567";
            string phoneNum5 = "00 (373) 776 74567";
            bool isValid = Regex.Match(phoneNum5, numberPattern2).Success;
            Console.WriteLine(isValid); // 776 - False

        }

    }
}
