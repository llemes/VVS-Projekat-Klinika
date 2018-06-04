using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public class Karton : IAnamneza
    {
        public List<Pregled> Pregledi { get; set; }
        public List<PregledSistematski> PreglediSistematski { get; set; }
        public List<Bolest> Bolesti { get; set; }
        public List<Alergija> Alergije { get; set; }
        public List<Tuple<string, string>> ZdravstvenaHistorijaPorodice { get; set; }

        public void Add(Pregled pregled)
        {
            Pregledi.Add(pregled);
        }
        public void Add(PregledSistematski pregled)
        {
            PreglediSistematski.Add(pregled);
        }
        public void Add(Bolest bolest)
        {
            Bolesti.Add(bolest);
        }
        public void Add(Alergija alergija)
        {
            Alergije.Add(alergija);
        }
        public void Porodica(Tuple<string, string> bolestClanaPorodice)
        {
            ZdravstvenaHistorijaPorodice.Add(bolestClanaPorodice);
        }

    }
}
