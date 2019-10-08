using System;
using System.IO;
using Newtonsoft.Json;

namespace csv_benchmark
{
    class Program
    {
        public static string csvPath = "../../../../InfoJanssen.csv";

        static void Main(string[] args)
        {
            Console.WriteLine("reading file...");
            var csv = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), csvPath));

            Console.WriteLine("parsing csv");
            var obj = CSV.ProcessCSV(csv);

            Console.WriteLine("saving json");
            File.WriteAllText("csv.json", JsonConvert.SerializeObject(obj.Rows, Formatting.Indented));
            Console.WriteLine("done");
        }
    }
}
