using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public abstract class Pregled
    {
        public int Id { get; set; }
        public DateTime VrijemePregleda { get; set; }
        public decimal Cijena { get; set; }
        public string Naziv { get; set; }
        public Pregled(DateTime vrijemePregleda, decimal cijena, string naziv)
        {
            VrijemePregleda = vrijemePregleda;
            Cijena = cijena;
            Naziv = naziv;
        }
    }
}
