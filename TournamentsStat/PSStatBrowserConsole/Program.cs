using PSStatBrowserConsole.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSStatBrowserConsole
{
    class Program
    {
        public List<Tournament> TrnmtsList { get; set; }

        static void Main(string[] args)
        {
            try
            {
                FileStream dataFile = File.Open("History.csv", FileMode.Open);
                StreamReader strReader = new StreamReader(dataFile);

                int count = 0;
                strReader.ReadLine();

                while (!strReader.EndOfStream)
                {
                    var line = strReader.ReadLine();
                    if (ParseLine(line))
                    {
                        count++;
                        Console.WriteLine(line);
                    }
                }

                Console.WriteLine($"{count} lines");
                Console.WriteLine($"Buy In: {buyIn}");
                Console.WriteLine($"Income: {income}");
                Console.WriteLine($"Profit: {income + buyIn}");
                Console.WriteLine($"Deposit: {deposit}");
            }
            catch(Exception e)
            {
                Console.WriteLine("File not found or wrong!");
            }
        }

        static float buyIn = 0;
        static float income = 0;
        static float deposit = 0;

        private static bool ParseLine(string line)
        {
            var dep = line.Contains("Deposit");

            if (!line.Contains("Tournament") && !dep)
                return false;

            var attributes = line.Split(',');

            //if (!attributes[2].Contains("Spin & Go"))
            //    return false;

            var value = attributes[5].Trim('"');
            float res = 0;

            if (float.TryParse(value, out res))
            {
                if (dep)
                    deposit += res / 100;
                else if (res < 0)
                    buyIn += res/100;
                else
                    income += res/100;
            }
            else
                return false;

            return true;
        }
    }
}
