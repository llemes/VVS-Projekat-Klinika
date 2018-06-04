using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public class ObracunCijene
    {
        private static int _AGE_LIMT_CHILD = 16; // for convenience
        public static decimal ObracunUkupnogIznosa(FiskalniRacun fiskalniRacun)
        {
            decimal rez = 0;

            foreach (Pregled p in fiskalniRacun.AktivniPregledi)
            {
                rez += p.Cijena;
            }

            foreach (PregledSistematski p in fiskalniRacun.AktivniSistematskiPregledi)
            {
                foreach (PregledSistematskiStavka pp in p.Pregledi)
                {
                    rez += pp.Cijena;
                }
            }

            return rez;
        }
        public static void PlacanjeGotovina(int idPacijenta)
        {
            Pacijent temp = EvidencijaPacijenata.GetPacijentById(idPacijenta);

            foreach(Pregled pregled in temp.FiskalniRacun.AktivniPregledi)
            { 
                if (temp.Redovni)
                {
                    pregled.Cijena = pregled.Cijena - 0.1m * pregled.Cijena;
                }

                DateTime now = DateTime.Now;
                int age = now.Year - temp.DatumRodjenja.Year;
                if (now < temp.DatumRodjenja.AddYears(age)) age--;

                if (age < _AGE_LIMT_CHILD)
                {
                    pregled.Cijena = pregled.Cijena - 0.5m * pregled.Cijena;
                }
            }

            foreach (PregledSistematski pregledi in temp.FiskalniRacun.AktivniSistematskiPregledi)
            {
                foreach(PregledSistematskiStavka pregled in pregledi.Pregledi)
                {
                    if (temp.Redovni)
                    {
                        pregled.Cijena = pregled.Cijena - 0.1m * pregled.Cijena;
                    }

                    DateTime now = DateTime.Now;
                    int age = now.Year - temp.DatumRodjenja.Year;
                    if (now < temp.DatumRodjenja.AddYears(age)) age--;

                    if (age < _AGE_LIMT_CHILD)
                    {
                        pregled.Cijena = pregled.Cijena - 0.5m * pregled.Cijena;
                    }
                }
            }
        }
        public static void PlacanjeRate(int idPacijenta)
        {
            Pacijent temp = EvidencijaPacijenata.GetPacijentById(idPacijenta);

            foreach (Pregled pregled in temp.FiskalniRacun.AktivniPregledi)
            {
                if (!temp.Redovni)
                {
                    pregled.Cijena = pregled.Cijena + 0.15m * pregled.Cijena;
                }

                DateTime now = DateTime.Now;
                int age = now.Year - temp.DatumRodjenja.Year;
                if (now < temp.DatumRodjenja.AddYears(age)) age--;

                if (age < _AGE_LIMT_CHILD)
                {
                    pregled.Cijena = pregled.Cijena - 0.4m * pregled.Cijena;
                }
            }

            foreach (PregledSistematski pregledi in temp.FiskalniRacun.AktivniSistematskiPregledi)
            {
                foreach (PregledSistematskiStavka pregled in pregledi.Pregledi)
                {
                    if (temp.Redovni)
                    {
                        pregled.Cijena = pregled.Cijena + 0.15m * pregled.Cijena;
                    }

                    DateTime now = DateTime.Now;
                    int age = now.Year - temp.DatumRodjenja.Year;
                    if (now < temp.DatumRodjenja.AddYears(age)) age--;

                    if (age < _AGE_LIMT_CHILD)
                    {
                        pregled.Cijena = pregled.Cijena - 0.4m * pregled.Cijena;
                    }
                }
            }
        }
    }
}
