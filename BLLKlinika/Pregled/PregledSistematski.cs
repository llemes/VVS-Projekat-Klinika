using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public class PregledSistematski
    {
        public List<PregledSistematskiStavka> Pregledi { get; set; }
        public int Id { get; set; }
        public PregledSistematski()
        {
            Pregledi = new List<PregledSistematskiStavka>();
        }

        public void PregledEvidencija(DateTime vrijemePregleda, string rezultatPregleda, bool uspjesanPregled, TipSistematskog tipPregleda)
        {
            Pregledi.Add(new PregledSistematskiStavka(vrijemePregleda, rezultatPregleda, uspjesanPregled, tipPregleda));
        }


        public List<string> PotrebniPregledi()
        {
            List<string> rez = new List<string>();
            var enumValues = Enum.GetValues(typeof(TipSistematskog));

            foreach (var value in enumValues)
            {
                var match = Pregledi.FirstOrDefault(listItem => listItem.TipSistematskog.Equals(value));

                if (match == null)
                {
                    rez.Add(value.ToString());
                }

            }

            return rez;
        }

        public bool PregledUspjesan()
        {
            var sviUspjesni = Pregledi.FirstOrDefault(listItem => listItem.UspjesanPregled == false);

            if (sviUspjesni == null)
            {
                return true;
            }

            return false;
        }

        // code coverage vs unused code
        // hihihi

        //public bool PreglediOdradjeni()
        //{
        //    var enumValues = Enum.GetValues(typeof(TipSistematskog));
        //    foreach (var value in enumValues)
        //    {
        //        var match = Pregledi.FirstOrDefault(listItem => listItem.TipSistematskog.Equals(value));
        //        if (match == null)
        //        {
        //            throw new InvalidOperationException("Nije urađen " + value.ToString() + "pregled");
        //        }
        //    }
        //    return true;
        //}
    }
}
