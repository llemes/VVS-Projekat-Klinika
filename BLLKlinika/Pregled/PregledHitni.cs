using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public class PregledHitni : Pregled
    {
        // info o prvoj pomoći
        public bool PrvaPomocUradjena { get; set; }
        public string PrvaPomocRazlog { get; set; }
        public DateTime SmrtVrijeme { get; set; }
        public string SmrtUzrok { get; set; }
        public DateTime SmrtObdukcijaTermin { get; set; }

        public PregledHitni(DateTime vrijemePregleda, decimal cijena, string naziv, bool prvaPomocUradjena, string prvaPomocRazlog) : base(vrijemePregleda, cijena, naziv)
        {
            PrvaPomocUradjena = prvaPomocUradjena;
            PrvaPomocRazlog = prvaPomocRazlog;
        }

        public void SmrtEvidencija(DateTime vrijemeSmrti, string uzrokSmrti, DateTime terminObdukcije)
        {
            SmrtVrijeme = vrijemeSmrti;
            SmrtUzrok = uzrokSmrti;
            SmrtObdukcijaTermin = terminObdukcije;
        }


    }
}
