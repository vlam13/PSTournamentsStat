using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSStatBrowserConsole
{
    class PSAction
    {
        public PSAction(ActionType action)
        {
            Type = action;
        }

        public string InfoString { get; set; }

        public enum ActionType { Tournament, Deposit, Reward, Zoom, Chest, Deal, Other}

        public int ID { get; set; }

        public DateTime Time { get; set; }

        public ActionType Type { get; set; }

        public string Description { get; set; }

        public float Amount { get; set; }

        public float Balance { get; set; }

        public static PSAction GetEntity(string action)
        {
            if (action.Contains("Tournament"))
                return new PSAction(ActionType.Tournament);

            if (action.Contains("Deposit"))
                return new PSAction(ActionType.Deposit);

            if (action.Contains("Deal"))
                return new PSAction(ActionType.Reward);

            if (action.Contains("Zoom"))
                return new PSAction(ActionType.Zoom);

            if (action.Contains("Chest"))
                return new PSAction(ActionType.Chest);

            return new PSAction(ActionType.Other);
        }

        public override string ToString()
        {
            return InfoString;
        }
    }
}
