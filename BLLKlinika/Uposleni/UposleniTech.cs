using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public class UposleniTech : Uposleni  // jadnik koji će ovo održavat xd
    {
        private const int _BAZNA_PLATA = 1400;
        public Ordinacija Ordinacija { get; set; }
        public UposleniTech(string ime, string prezime)
        {
            DataValidator.ValidateName(ime, prezime);
            Ime = ime;
            Prezime = prezime;
            Plata = _BAZNA_PLATA;
        }
    }
}
