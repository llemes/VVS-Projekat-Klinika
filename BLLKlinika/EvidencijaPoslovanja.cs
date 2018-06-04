using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public static class EvidencijaPoslovanja
    {
        private static List<Transakcija> _transakcije = new List<Transakcija>();

        public static void EvidentirajTransakciju(DateTime datumTransakcije, Pacijent platio, decimal iznos)
        {
            _transakcije.Add(new Transakcija(datumTransakcije, platio, iznos));
        }

        public static decimal IznosTransakcijaUProteklomMjesecu(Pacijent p)
        {
            decimal iznos = 0;

            foreach(Transakcija t in _transakcije)
            {
                if(t.DatumTransakcije.Month == DateTime.Now.AddMonths(-1).Month)
                {
                    if(p.Equals(t.Platio))
                    {
                        iznos += t.Iznos;
                    }
                }
            }

            return iznos;
        }

        public static Pacijent NajveciIzvorPrihoda()
        {
            Pacijent rez = null;
            decimal maxPrihod = 0;

            foreach(var t in _transakcije)
            {
                if (t.DatumTransakcije.Month == DateTime.Now.AddMonths(-1).Month)
                {
                    decimal temp = IznosTransakcijaUProteklomMjesecu(t.Platio);
                    if (temp > maxPrihod)
                    {
                        rez = t.Platio;
                        maxPrihod = temp;
                    }
                }
            }

            if(rez == null)
            {
                throw new InvalidOperationException("Nije bilo transakcija u proteklom mjesecu");
            }

            return rez;
                
        }
    }
}
