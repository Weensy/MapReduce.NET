#define TRACE
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using MapReduce.Analysis.Finance;
using MapReduce.Storage.Configurations;


namespace MapReduce.Clients
{
    class Program
    {
        /// <summary>
        /// Main function
        /// </summary>
        static void Main(string[] args)
        {
            #if TRACE
            var timer = new Stopwatch();
            timer.Start();
            #endif

            IEnumerable<MapReduce.Core.KeyValuePair<String, String>> source = null;
            ReadInput(out source);

            var mapReduce = new CalculateQuotesSpreadJob(new QuoteMappingStrategy()); 
            var results = mapReduce.MapReduce(source, mapReduce.Map, mapReduce.Reduce).ToList();

            WriteOutput(results);

            #if TRACE
            timer.Stop();
            Console.WriteLine(); Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Elapsed (ms): {0}", TimeSpan.FromTicks(timer.ElapsedTicks).Milliseconds);
            #endif

            Console.ReadKey();
        }



        /// <summary>
        /// Конфигурация работы
        /// </summary>
        private static readonly JobConfiguration JobConfiguration = new JobConfiguration()
        {
            StorageConfiguration = new StorageConfiguration()
            {
                InputFolder = @"X:\Applications\MapReduce.NET\MapReduce.Implementation\App_Data\1m",
                OutputFolder = ""
            }
        };

        /// <summary>
        /// Чтение входных параметров
        /// </summary>
        /// <typeparam name="TSource">Тип элементов исходной коллекции</typeparam>
        private static void ReadInput<TSource>(out IEnumerable<TSource> input)
            where TSource : MapReduce.Core.IKeyValuePair<String, String>, new()
        {
            input = new QuotesRepository(JobConfiguration)
                        .Get()
                        .Select(d => new TSource { Key = d.Id, Value = d.Content });
        }

        /// <summary>
        /// Запись выходных данных в выходной поток
        /// </summary>
        /// <typeparam name="TResult">Тип элементов результирующей коллекции</typeparam>
        private static void WriteOutput<TResult>(IEnumerable<TResult> results)
            where TResult : MapReduce.Core.IKeyValuePair<String, Decimal>
        {
            foreach (var result in results.OrderByDescending(r => r.Value))
                Console.WriteLine("{0}: {1}", result.Key, result.Value);
        }
    }
}
