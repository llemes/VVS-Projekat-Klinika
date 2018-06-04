using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public static class EvidencijaOrdinacija
    {
        private static int _idGeneratorOrdinacija = 0;
        private static int _idGeneratorAparat = 0;
        private static List<Ordinacija> _ordinacije = new List<Ordinacija>();
        public static int DodajOrdinaciju(string naziv)
        {
            Ordinacija temp = new Ordinacija(naziv);
            temp.Id = _idGeneratorOrdinacija++;
            _ordinacije.Add(temp);
            return temp.Id;
        }

        public static Aparat Get(int idAparata, int idOrdinacije)
        {
            if(idAparata < 0 || idAparata >= _idGeneratorAparat)
            {
                throw new ArgumentException("Aparat sa id " + idAparata + " ne postoji");
            }
            try
            {
                return GetOrdinacijaById(idOrdinacije).Aparati.Find(p => p.Id == idAparata);
            }
            catch(ArgumentNullException e)
            {
                throw new ArgumentException("Aparat sa id " + idAparata + " ne postoji u ordinaciji " + idOrdinacije);
            }
            
        }
        public static Ordinacija GetOrdinacijaById(int idOrdinacije)
        {
            if(idOrdinacije < 0 || idOrdinacije >= _idGeneratorOrdinacija)
            {
                throw new ArgumentException("Ordinacija sa id " + idOrdinacije + " ne postoji");
            }
            return _ordinacije.Find(p => p.Id == idOrdinacije);
        }

        #region Aparat

        public static int DodajAparat(int idOrdinacije, string nazivAparata)
        {
            Ordinacija tempOrdinacija = GetOrdinacijaById(idOrdinacije);
            Aparat tempAparat = new Aparat(nazivAparata);
            tempAparat.Id = _idGeneratorAparat++;
            tempOrdinacija.Aparati.Add(tempAparat);
            return tempAparat.Id;
        }
        public static void EvidentirajRadAparata(int idOrdinacije, int idAparata, DateTime vrijemeUkljucivanja, DateTime vrijemeIskljucivanja)
        {
            if(vrijemeUkljucivanja > vrijemeIskljucivanja)
            {
                throw new ArgumentException("Vrijeme uključivanja aparata je kasnije od vremena isključivanja");
            }

            Aparat tempAparat = Get(idAparata, idOrdinacije);
            tempAparat.AktivnoVrijeme.Add(new Tuple<DateTime, DateTime>(vrijemeUkljucivanja, vrijemeIskljucivanja));
            
        }


        #endregion

        #region Pacijenti
        public static int GetIdNajslobodnijeOrdinacije(string tipOrdinacije)
        {
            List<Ordinacija> opcije = null;
            try
            {
                opcije = _ordinacije.FindAll(p => p.Naziv.Equals(tipOrdinacije));
            }
            catch(ArgumentNullException e)
            {
                throw new Exception("Ne postoji ordinacija s tim nazivom");
            }

            if(opcije == null || opcije.Count == 0)
            {
                throw new ArgumentException("Ne postoji ordinacija s tim nazivom");
            }

            int minCount = opcije[0].RedCekanja.Count;
            int id = opcije[0].Id;

            foreach(var x in opcije)
            {
                if(x.RedCekanja.Count < minCount)
                {
                    minCount = x.RedCekanja.Count;
                    id = x.Id;
                }
            }

            return id;

        }
        public static int DodajPacijenta(int idOrdinacije, int idPacijenta)
        {
            Ordinacija tempOrdinacija = GetOrdinacijaById(idOrdinacije);
            Pacijent tempPacijent = EvidencijaPacijenata.GetPacijentById(idPacijenta);
            tempOrdinacija.RedCekanja.Add(tempPacijent);
            return tempOrdinacija.RedCekanja.Count();
        }
        public static void OpsluziPacijenta(int idOrdinacije)
        {
            Ordinacija tempOrdinacija = GetOrdinacijaById(idOrdinacije);

            if(tempOrdinacija.RedCekanja.Count == 0)
            {
                throw new ArgumentException("Nema pacijenata u redu čekanja");
            }

            tempOrdinacija.OpsluzeniPacijenti.Add(tempOrdinacija.RedCekanja[0]);
            tempOrdinacija.RedCekanja.RemoveAt(0);
        }
        
        #endregion
    }
}
