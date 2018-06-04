using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public class UposleniDoktor : Uposleni
    {
        private const int _BAZNA_PLATA = 1500;
        private const int _BONUS_NOVI = 10;
        private const int _BONUS_REDOVNI = 20;
        public Ordinacija Ordinacija { get; set; }
        public UposleniDoktor(string ime, string prezime, int idOrdinacije)
        {
            DataValidator.ValidateName(ime, prezime);
            Ordinacija = EvidencijaOrdinacija.GetOrdinacijaById(idOrdinacije);
            Ime = ime;
            Prezime = prezime;
            Plata = _BAZNA_PLATA;
        }
        public void ObracunBonusa() // make sure this gets called tho
        {
            int counter = 0;

            if(Ordinacija.OpsluzeniPacijenti == null)
            {
                return;
            }

            foreach(Pacijent p in Ordinacija.OpsluzeniPacijenti)
            {
                if(p.DatumPrijema.Date == DateTime.Now.Date)
                {
                    if(p.Redovni)
                    {
                        Plata += _BONUS_REDOVNI;
                    }
                    else
                    {
                        Plata += _BONUS_NOVI;
                    }
                    
                    if(++counter == 20)
                    {
                        break;
                    }
                }
            }
        }
    }
}
