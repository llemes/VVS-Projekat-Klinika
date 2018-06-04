using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public class LaboratorijaMock : ILaboratorija
    {
        public double BrojEritrocita(int idPacijenta)
        {
            // šta sad s ovim podacima xD
            return 5000000;
        }

        public double BrojLeukocita(int idPacijenta)
        {
            return 8000;
        }

        public string KrvnaGrupa(int idPacijenta)
        {
            return "AB+";
        }
    }
}
