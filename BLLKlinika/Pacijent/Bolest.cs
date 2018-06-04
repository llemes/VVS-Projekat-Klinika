using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public class Bolest // aka ova zadaća
    {
        public string Naziv { get; set; }
        public bool Aktivna { get; set; }
        
        public Bolest(string naziv, bool aktivna)
        {
            Naziv = naziv;
            Aktivna = aktivna;
        }
    }
}
