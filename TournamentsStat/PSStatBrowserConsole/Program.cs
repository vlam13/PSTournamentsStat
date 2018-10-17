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
                StreamReader fileReader = File.OpenText("History.csv");
                //int counter = 0;

                while (!fileReader.EndOfStream)
                {
                    string line = fileReader.ReadLine();
                    //Console.WriteLine(line);
                    ParseLine(line);
                }

                Console.WriteLine($"{_infoSrc.Count} events loaded");

                //Console.WriteLine($"{count} lines");
                //Console.WriteLine($"Buy In: {buyIn}");
                //Console.WriteLine($"Income: {income}");
                //Console.WriteLine($"Profit: {income + buyIn}");
                //Console.WriteLine($"Deposit: {deposit

                bool exit = false;

                while(!exit)
                {
                    PrintPrompt();
                    var cmd = Console.ReadLine();

                    switch (cmd)
                    {
                        case "all":
                            PrintAllActions();
                            break;
                        case "balance":
                            Console.WriteLine($"Current Balance: {balance}");
                            break;
                        case "deposit":
                            GiveDepositInfo();
                            break;
                        case "tinfo":
                            ShowTournaments();
                            break;
                        case "exit":
                        case "e":
                            exit = true;
                            break;
                        default:
                            Console.WriteLine("Unknow command. Type 'exit' or 'e' to exit.");
                            break;
                    }
                }
            }
            catch(Exception)
            {
                Console.WriteLine("File not found or wrong!");
            }
        }

        private static void PrintAllActions()
        {
            foreach (var item in _infoSrc)
            {
                Console.WriteLine(item);
            }
        }

        private static void ShowTournaments()
        {
            var tournaments = _infoSrc.Where(x => x is Tournament);
            //int buyIn = 0;
            foreach (var tournm in tournaments)
            {
                Console.WriteLine(tournm);
            }

            Console.WriteLine($"Overall tournamets count:{tournaments.Count()}");
        }

        private static void GiveDepositInfo()
        {
            var depos = _infoSrc.Select(x => x).Where(x => x.GetType() == typeof(Deposit));
            Console.WriteLine($"Was made {depos.Count()} deposit");
            foreach (var dep in depos)
                Console.WriteLine(dep);

            Console.WriteLine($"Maximum: {depos.Max(x => x.Amount)}");
            Console.WriteLine($"Minimum: {depos.Min(x => x.Amount)}");
            Console.WriteLine($"Sum: {depos.Sum(x => x.Amount)}");
        }

        public static void PrintPrompt()
        {
            Console.Write("> ");
        }

        static float buyIn = 0;
        static float income = 0;
        static float deposit = 0;
        static float balance = 0;

        static List<Entity> _infoSrc = new List<Entity>();

        private static bool ParseLine(string line)
        {
            var attributes = line.Split(',');

            if (!DateTime.TryParse(attributes[0], out DateTime time))
                return false;

            var newEvent = Entity.GetEntity(attributes[1]);

            if (newEvent == null)
                return false;

            newEvent.InfoString = line;
            newEvent.Time = time;

            var amount = attributes[5].Trim('"').Replace('.', ',');
            float.TryParse(amount, out float val);

            newEvent.Amount = val;

            _infoSrc.Add(newEvent);

            float.TryParse(attributes[8].Trim('"').Replace('.',','), out balance);

            newEvent.Balance = balance;

            return true;
        }

        private static bool _ParseLine(string line)
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
