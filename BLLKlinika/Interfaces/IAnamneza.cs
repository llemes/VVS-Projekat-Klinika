using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public interface IAnamneza
    {
        void Add(Bolest bolest);
        void Add(Alergija alergija);
        void Porodica(Tuple<string, string> bolestClanaPorodice); // 1. string - član porodice, 2. string - bolest
    }
}
