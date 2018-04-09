using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSStatBrowserConsole.Entities
{
    class Deposit : Entity
    {
        public override string ToString()
        {
            return $"{Time} - Deposit: {Amount}, Balance: {Balance}";
        }
    }
}
