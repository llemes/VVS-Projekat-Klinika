using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public interface IPregledNormalniBuilder
    {
        void ConstructLijekovi(string naziv, double kolicina, string mjera, int frekvencijaUzimanja, string mjeraFrekvencijeUzimanja);
        void ConstructTerapijaKratkorocna(DateTime datumPotpisivanja, DateTime krajTerapije); 
        void ConstructTerapijaDugorocna(DateTime datumPotpisivanja); 
        void ConstructPregledNormalni(DateTime vrijemePregleda, decimal cijenaPregleda, string naziv, string misljenjeDoktora, string rezultat); 
        PregledNormalni GetPregled();
    }
}
