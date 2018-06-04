using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public class PacijentStub : Pacijent, ITerapije
    {
        public Karton historijaPacijenta()
        {
            return Karton;
        }

        public List<Terapija> prikaziRanijeTerapije() 
        {
            List<Terapija> rez = new List<Terapija>();

            rez.Add(new TerapijaDugorocna());
            rez.Add(new TerapijaDugorocna());
            rez.Add(new TerapijaDugorocna());
            rez.Add(new TerapijaKratkorocna());
            rez.Add(new TerapijaDugorocna());
            rez.Add(new TerapijaDugorocna());

            return rez;

            // back when i didnt realise what a stub was
            // please be patient i have autism

            /*
            List<Terapija> terapija = new List<Terapija>();

            foreach (Pregled p in Karton.Pregledi)
            {
                if (p is PregledNormalni)
                {
                    if ((p as PregledNormalni).Terapija is TerapijaKratkorocna && ((p as PregledNormalni).Terapija as TerapijaKratkorocna).KrajTerapije <= DateTime.Now)
                    {
                        terapija.Add((p as PregledNormalni).Terapija);
                    }
                    if ((p as PregledNormalni).Terapija is TerapijaDugorocna)
                    {
                        terapija.Add((p as PregledNormalni).Terapija);
                    }
                }
            }

            return terapija;
            */
        }

        public Terapija trenutnaTerapija()
        {
            return new TerapijaKratkorocna();

            /*
            foreach (Pregled p in Karton.Pregledi)
            {
                if (p is PregledNormalni)
                {
                    if ((p as PregledNormalni).Terapija is TerapijaKratkorocna && ((p as PregledNormalni).Terapija as TerapijaKratkorocna).KrajTerapije >= DateTime.Now)
                    {
                        return (p as PregledNormalni).Terapija;
                    }
                    if ((p as PregledNormalni).Terapija is TerapijaDugorocna)
                    {
                         return (p as PregledNormalni).Terapija;
                    }
                }
            }

            throw new InvalidOperationException("Nema trenutnih terapija");
            */
        }
    }
}
