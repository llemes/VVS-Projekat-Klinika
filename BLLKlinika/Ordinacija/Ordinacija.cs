using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public class Ordinacija
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public List<Aparat> Aparati { get; set; }
        public List<Pacijent> RedCekanja { get; set; }
        public List<Pacijent> OpsluzeniPacijenti { get; set; }
        public Ordinacija(string naziv)
        {
            Naziv = naziv;
            Aparati = new List<Aparat>();
            RedCekanja = new List<Pacijent>();
            OpsluzeniPacijenti = new List<Pacijent>();
        }
    }
}
