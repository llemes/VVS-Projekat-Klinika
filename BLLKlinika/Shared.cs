using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public enum Spol { Muski, Zenski };
    public enum BracnoStanje { Nevjencan, Vjencan }; // what is language even
    public enum TipSistematskog { Oftamolog, Neuropsihijatar, Psiholog, Opci};

    public static class MyConvert
    {
        public static TipSistematskog ToTipSistematskog(string s)
        {
            if(s.Equals("Oftamolog"))
            {
                return TipSistematskog.Oftamolog;
            }
            if(s.Equals("Psiholog"))
            {
                return TipSistematskog.Psiholog;
            }
            if(s.Equals("Neuropsihijatar"))
            {
                return TipSistematskog.Neuropsihijatar;
            }

            return TipSistematskog.Opci; 
            
            // nije nullable a nema defaultnu vrijednost
            // bad design but whatever
            // koristi se samo za xml

        }
    }

}
