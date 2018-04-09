using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSStatBrowserConsole.Entities
{
    abstract class Entity
    {
        public int ID { get; set; }

        public DateTime Time { get; set; }

        public float Amount { get; set; }

        public float Balance { get; set; }

        public static Entity GetEntity(string action)
        {
            if (action.Contains("Tournament"))
                return new Tournament();

            if (action.Contains("Deposit"))
                return new Deposit();

            if (action.Contains("Reward"))
                return new Reward();

            return null;
        }
    }
}
