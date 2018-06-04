using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public class Transakcija
    {
        public DateTime DatumTransakcije { get; set; }
        public Pacijent Platio { get; set; }
        public decimal Iznos { get; set; }
        public Transakcija(DateTime datumTransakcije, Pacijent platio, decimal iznos)
        {
            DatumTransakcije = datumTransakcije;
            Platio = platio;
            Iznos = iznos;
        }
    }
}
