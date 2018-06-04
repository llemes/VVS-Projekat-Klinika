using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public class PregledSistematskiStavka
    {
        public DateTime VrijemePregleda { get; set; }
        public decimal Cijena { get; set; }
        public string RezultatPregleda { get; set; }
        public bool UspjesanPregled { get; set; }
        public TipSistematskog TipSistematskog { get; set; }
        public PregledSistematskiStavka(DateTime vrijemePregleda, string rezultatPregleda, bool uspjesanPregled, TipSistematskog tipSistematskog) 
        {
            VrijemePregleda = vrijemePregleda;
            RezultatPregleda = rezultatPregleda;
            UspjesanPregled = uspjesanPregled;
            TipSistematskog = tipSistematskog;

            switch(tipSistematskog)
            {
                case TipSistematskog.Oftamolog:
                    Cijena = 30;
                    break;
                case TipSistematskog.Opci:
                    Cijena = 15;
                    break;
                case TipSistematskog.Neuropsihijatar:
                    Cijena = 20;
                    break;
                case TipSistematskog.Psiholog:
                    Cijena = 20;
                    break;
            }

        }
    }
}
