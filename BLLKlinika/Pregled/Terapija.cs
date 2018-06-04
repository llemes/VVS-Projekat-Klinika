using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public abstract class Terapija
    {
        public List<Lijek> Lijekovi { get; set; }
        public DateTime DatumPotpisivanja { get; set; }

    }
}
