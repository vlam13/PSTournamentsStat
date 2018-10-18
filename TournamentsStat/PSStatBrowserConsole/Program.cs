using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PSStatBrowserConsole.PSAction;

namespace PSStatBrowserConsole
{
    class Program
    {
        public enum Commands { all, balance, show, exit };

        static List<PSAction> _infoSrc = new List<PSAction>();

        static void Main(string[] args)
        {
            try
            {
                StreamReader fileReader = File.OpenText("History.csv");

                while (!fileReader.EndOfStream)
                {
                    ParseLine(fileReader.ReadLine());
                }

                Console.WriteLine($"{_infoSrc.Count} events loaded");
            }
            catch (Exception)
            {
                Console.WriteLine("File not found or wrong!");
            }

            bool exit = false;

            while (!exit)
            {
                try
                {
                    PrintPrompt();
                    var input = Console.ReadLine();
                    args = input.Split(' ');
                    string cmd = args[0].Trim();

                    if (Enum.TryParse(cmd, out Commands command))
                    {
                        switch (command)
                        {
                            case Commands.all:
                                PrintAllActions();
                                break;
                            case Commands.balance:
                                Console.WriteLine($"Current Balance: {balance}");
                                break;
                            case Commands.show:
                                Show(args);
                                break;
                            case Commands.exit:
                                exit = true;
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Unknow command.");
                        Console.WriteLine("Commands list:");

                        foreach (var com in Enum.GetValues(typeof(Commands)))
                        {
                            Console.WriteLine(com.ToString());
                        }
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static void PrintAllActions()
        {
            foreach (var item in _infoSrc)
            {
                Console.WriteLine(item);
            }
        }

        private static void Show(string[] args)
        {
            if (args.Count() < 2)
            {
                Console.WriteLine("Command argument is missing");
                Console.Write("{ ");
                foreach (var com in Enum.GetValues(typeof(ActionType)))
                {
                    Console.Write(com.ToString() + " ");
                }
                Console.Write("}");
                Console.WriteLine();
                return;
            }

            string argm = args[1].Trim();

            if (Enum.TryParse(argm, out ActionType actType))
            {
                var actions = _infoSrc.Where(x => x.Type == actType);

                foreach (var act in actions)
                {
                    Console.WriteLine(act);
                }

                Console.WriteLine($"{actions.Count()} entries");
            }
            else
                Console.WriteLine("Unknowm argument");
        }

        public static void PrintPrompt()
        {
            Console.Write("> ");
        }

        //static float buyIn = 0;
        //static float income = 0;
        //static float deposit = 0;
        static float balance = 0;

        private static bool ParseLine(string line)
        {
            var attributes = line.Split(',');

            if (!DateTime.TryParse(attributes[0], out DateTime time))
                return false;

            var newEvent = PSAction.GetEntity(attributes[1]);

            newEvent.InfoString = line;
            newEvent.Time = time;

            var amount = attributes[5].Trim('"').Replace('.', ',');
            float.TryParse(amount, out float val);

            newEvent.Amount = val;

            _infoSrc.Add(newEvent);

            float.TryParse(attributes[8].Trim('"').Replace('.', ','), out balance);

            newEvent.Balance = balance;

            return true;
        }
    }
}
