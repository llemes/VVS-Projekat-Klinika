using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public class PregledNormalniBuilder : IPregledNormalniBuilder
    {
        private PregledNormalni _pregled;
        private List<Lijek> _lijekovi = new List<Lijek>();
        private Terapija _terapija;
        
        public void ConstructLijekovi(string naziv, double kolicina, string mjera, int frekvencijaUzimanja, string mjeraFrekvencijeUzimanja)
        {
            Lijek temp = new Lijek();
            temp.Naziv = naziv;
            temp.Kolicina = kolicina;
            temp.MjeraLijeka = mjera;
            temp.FrekvencijaUzimanja = frekvencijaUzimanja;
            temp.MjeraFrekvencijeUzimanja = mjeraFrekvencijeUzimanja;
            _lijekovi.Add(temp);
        }
        public void ConstructTerapijaKratkorocna(DateTime datumPotpisivanja, DateTime krajTerapije)
        {
            _terapija = new TerapijaKratkorocna();

            (_terapija as TerapijaKratkorocna).KrajTerapije = krajTerapije;
            _terapija.DatumPotpisivanja = datumPotpisivanja;
            _terapija.Lijekovi = _lijekovi;

        }
        public void ConstructPregledNormalni(DateTime vrijemePregleda, decimal cijenaPregleda, string naziv, string misljenjeDoktora, string rezultat)
        {
            _pregled = new PregledNormalni(vrijemePregleda, cijenaPregleda, naziv);
            _pregled.MisljenjeDoktora = misljenjeDoktora;
            _pregled.Rezultat = new Bolest(rezultat, true);
            _pregled.Terapija = _terapija;
        }

        public void ConstructTerapijaDugorocna(DateTime datumPotpisivanja)
        {
            _terapija = new TerapijaDugorocna();
            _terapija.DatumPotpisivanja = datumPotpisivanja;
            _terapija.Lijekovi = _lijekovi;
        }

        public PregledNormalni GetPregled()
        {
            // redoslijed bacanja exceptiona je ujedno i redoslijed kojim bi se trebale kreirati komponente
            // or is it

            if (_lijekovi == null)
            {
                throw new InvalidOperationException("Lijekovi nisu dodani");
            }

            if (_terapija == null)
            {
                throw new InvalidOperationException("Terapija nije dodana");
            }

            if (_pregled == null)
            {
                throw new InvalidOperationException("Pregled nije kreiran");
            }

            return _pregled;
        }
    }
}
