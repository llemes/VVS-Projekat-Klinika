using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    // ovu klasu nasljeđuju samo korisnici sistema
    // prvobitno se zvala korisnik
    // i'm so conflicted about naming these
    public abstract class Uposleni
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public virtual int Plata { get; set; }
    }
}
