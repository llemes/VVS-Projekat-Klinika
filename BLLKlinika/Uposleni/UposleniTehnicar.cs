using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public class UposleniTehnicar : Uposleni
    {
        private const int _BAZNA_PLATA = 1200;
        public Ordinacija Ordinacija { get; set; } // može biti null za šalteruše
        public UposleniTehnicar(string ime, string prezime)
        {
            DataValidator.ValidateName(ime, prezime);
            Ime = ime;
            Prezime = prezime;
            Plata = _BAZNA_PLATA;
        }
    }
}
