using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public interface ITerapije
    {
        Karton historijaPacijenta();
        List<Terapija> prikaziRanijeTerapije();
        Terapija trenutnaTerapija();
    }
}
