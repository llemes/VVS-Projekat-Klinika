using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public class PregledNormalni : Pregled
    {
        public string MisljenjeDoktora { get; set; }
        public Bolest Rezultat { get; set; }
        public Terapija Terapija { get; set; }

        public PregledNormalni(DateTime vrijemePregleda, decimal cijena, string naziv) : base(vrijemePregleda, cijena, naziv)
        {
            
        }
    }
}
