using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public class Pacijent
    {
        public int BrojPosjeta { get; set; }
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public Spol Spol { get; set; }
        public string AdresaStanovanja { get; set; }
        public BracnoStanje BracnoStanje { get; set; }
        public DateTime DatumPrijema { get; set; }
        public Karton Karton { get; set; }
        public bool Redovni
        {
            get
            {
                return BrojPosjeta > 3;
            }
        }
        public PregledNormalniBuilder PregledNormalniBuilder { get; set; } // the plot thickens
        public FiskalniRacun FiskalniRacun { get; set; }

        public Pacijent()
        {
            PregledNormalniBuilder = new PregledNormalniBuilder();
        }
    }
}
