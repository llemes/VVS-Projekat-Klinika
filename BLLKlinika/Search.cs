using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public static partial class EvidencijaPacijenata
    {
        public static int GetPacijentId(string key)
        {
            Pacijent temp = _pacijenti.Find(p => p.Ime.Contains(key) || p.Prezime.Contains(key));

            return temp.Id;
        }
    }
}
