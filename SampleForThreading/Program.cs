using System;
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
            decimal average = 0;
            long sum2 = 0;
            decimal average2 = 0;
           
            // ------------- реализация 1 

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i;
            }

            /*for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
            }

            average = sum / arr.Length;*/


            // Console.WriteLine($"среднее арифметическое {sum} / {arr.Length} = {average}");

            //------------- реализация 2 с многопоточностью

            /* long ind = 0;
             long ind2 = 0;

             //----------------- Вариант а -----------------------

             var res1 = Parallel.For<long>(0, arr.Length, () => 0,
                 (i, state, locsum) =>
                 {

                     ind = 0;
                     ind += i;
                     average = (i != 0) ? sum / arr[i] : 0;
                     return locsum += arr[i];

                 },
                 (locsum) =>
                 {
                     lock (locker)
                     {
                         sum += locsum;

                         Console.WriteLine($" {locsum} {sum} {average} {ind} ");
                         average = sum / arr.Length;
                     }
                 }
                 );
             Console.WriteLine($"{sum} {average}");

             //----------------- Вариант б interlocked -----------------------

             var res2 = Parallel.For<long>(0, arr.Length, () => 0,
                 (i, state, locsum) =>
                 {
                     ind2 = 0;
                     ind2 += arr[i];
                     average2 = (i != 0) ? sum2 / arr[i] : 0;
                     return locsum += arr[i];
                 },
                 (locsum) =>
                 {
                     Interlocked.Add(ref sum2, locsum);
                     average2 = sum / arr.Length;
                     Console.WriteLine($"Лок: {locsum} Общ: {sum2} Сред: {average2} Инд: {ind2}");
                 }
                  );



             Console.WriteLine($"{sum2} {average2}");*/


            //==================== JobExecutor вызов ==========================================
            var rand = new Random();
            var executor = new JobExecutor();
            int count = 0;
            while (true)
            {
                executor.Add(() => { Console.WriteLine($"Восхищение! {count}"); Thread.Sleep(rand.Next(200, 1500)); });
                count += 1;
                if (count==20)
                {
                    executor.Stop();
                }
                
            }
            



            //==================== Regex =============================


            /*string input = "http://ya.ru/api?r=1&x=23";
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
            Console.WriteLine(isValid); // 776 - False*/








        }
        

    }
}
