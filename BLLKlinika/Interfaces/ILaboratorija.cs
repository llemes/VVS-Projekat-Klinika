using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    interface ILaboratorija
    {
        string KrvnaGrupa(int idPacijenta);
        double BrojEritrocita(int idPacijenta);
        double BrojLeukocita(int idPacijenta);
    }
}
