using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automower
{
    class Feld
    {
        public int maehanzahl { get; set; }
        public bool isrand { get; set; }
        public bool isinside { get; set; }

        public Feld(bool Isrand, bool Isinside)
        {
            isrand = Isrand;
            isinside = Isinside;
        }

        public void maehen()
        {
            maehanzahl++;
        }
    }
}
