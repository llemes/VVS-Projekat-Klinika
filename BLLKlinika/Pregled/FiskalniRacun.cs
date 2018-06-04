using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public class FiskalniRacun 
    {
        private const int _BROJ_RATA = 6;
        public List<Pregled> AktivniPregledi = new List<Pregled>();
        public List<PregledSistematski> AktivniSistematskiPregledi = new List<PregledSistematski>();
        public decimal NeplaceniIznos { get; set; }
        public decimal NeplaceniIznosPocetni { get; set; }
        public void DodajStavku(Pregled pregled)
        { 
            AktivniPregledi.Add(pregled);
        }
        public void DodajStavku(PregledSistematski pregled) // keep track (future expenses potentially can break this)
        {
            try
            {
                int tempIndex = AktivniSistematskiPregledi.FindIndex(p => p.Id == pregled.Id);
                if(tempIndex > -1)
                {
                    AktivniSistematskiPregledi[tempIndex] = pregled;
                }
                else
                {
                    AktivniSistematskiPregledi.Add(pregled);
                }
            }
            catch (ArgumentNullException e)
            {
                AktivniSistematskiPregledi.Add(pregled);
            }          
        }
        public string OdrezakGotovina(int idPacijenta)
        {
            TotalGotovina(idPacijenta);

            string rez = DateTime.Now.ToString() + Environment.NewLine;

            foreach(Pregled p in AktivniPregledi)
            {
                rez += p.Naziv + " " + p.Cijena + Environment.NewLine;
            }

            foreach (PregledSistematski p in AktivniSistematskiPregledi)
            {
                foreach (PregledSistematskiStavka pp in p.Pregledi)
                {
                    rez += pp.TipSistematskog.ToString() + " " + pp.Cijena;
                }
            }

            NeplaceniIznos = ObracunCijene.ObracunUkupnogIznosa(this);
            rez += "total: " + NeplaceniIznos;

            return rez;
        }

        public string OdrezakRate(int idPacijenta)
        {
            TotalRate(idPacijenta);

            string rez = DateTime.Now.ToString() + Environment.NewLine;

            foreach (Pregled p in AktivniPregledi)
            {
                rez += p.Naziv + " " + p.Cijena + Environment.NewLine;
            }

            foreach (PregledSistematski p in AktivniSistematskiPregledi)
            {
                foreach (PregledSistematskiStavka pp in p.Pregledi)
                {
                    rez += pp.TipSistematskog.ToString() + " " + pp.Cijena + Environment.NewLine;
                }
            }

            NeplaceniIznos = ObracunCijene.ObracunUkupnogIznosa(this);

            rez += "total: " + NeplaceniIznos + Environment.NewLine;
            rez += "iznos rate: " + NeplaceniIznos / _BROJ_RATA + Environment.NewLine;
            rez += "broj rata: " + _BROJ_RATA + Environment.NewLine;

            return rez;
        }
        public decimal PlatiRatu()
        {
            decimal iznos = NeplaceniIznosPocetni / _BROJ_RATA;
            NeplaceniIznos -= iznos;
            if(NeplaceniIznos <= 0)
            {
                NeplaceniIznos = 0;
                NeplaceniIznosPocetni = 0;
                AktivniPregledi = new List<Pregled>();
                AktivniSistematskiPregledi = new List<PregledSistematski>();
            }
            return iznos;
        }
        public decimal PlatiGotovina()
        {
            decimal iznos = NeplaceniIznos;
            NeplaceniIznos = 0;
            AktivniPregledi = new List<Pregled>();
            AktivniSistematskiPregledi = new List<PregledSistematski>();
            return iznos;
        }
        public decimal TotalRate(int idPacijenta) // obračun cijene - must get called
        {
            ObracunCijene.PlacanjeRate(idPacijenta);
            NeplaceniIznos = ObracunCijene.ObracunUkupnogIznosa(this);
            NeplaceniIznosPocetni = NeplaceniIznos;
            return NeplaceniIznos;
        }
        public decimal TotalGotovina(int idPacijenta)
        {
            ObracunCijene.PlacanjeGotovina(idPacijenta);
            NeplaceniIznos = ObracunCijene.ObracunUkupnogIznosa(this);
            NeplaceniIznosPocetni = NeplaceniIznos;
            return NeplaceniIznos;
        }
    }
}
