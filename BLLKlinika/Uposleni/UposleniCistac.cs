using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public class UposleniCistac : Uposleni
    {
        private const int _BAZNA_PLATA = 800;
        public UposleniCistac(string ime, string prezime)
        {
            DataValidator.ValidateName(ime, prezime);
            Ime = ime;
            Prezime = prezime;
            Plata = _BAZNA_PLATA;
        }
        
    }
}
