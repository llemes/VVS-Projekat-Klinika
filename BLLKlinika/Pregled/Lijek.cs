using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public class Lijek
    {
        public string Naziv { get; set; }
        public double Kolicina { get; set; }
        public string MjeraLijeka { get; set; } // primjer: tableta, ampula, kasika sirupa - nezahvalno za enum 
        public int FrekvencijaUzimanja { get; set; }
        public string MjeraFrekvencijeUzimanja { get; set; } // primjer: dnevno, sedmično, mjesečno
    }
}
