using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public class Aparat
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public List<Tuple<DateTime, DateTime>> AktivnoVrijeme { get; set; } 
        public Aparat(string nazivAparata)
        {
            Naziv = nazivAparata;
            AktivnoVrijeme = new List<Tuple<DateTime, DateTime>>();
        }
        public bool Aktivan()
        {
            Tuple<DateTime, DateTime> temp = AktivnoVrijeme[AktivnoVrijeme.Count - 1];
            return temp.Item1 <= DateTime.Now && temp.Item2 >= DateTime.Now;
        }
    }
}
