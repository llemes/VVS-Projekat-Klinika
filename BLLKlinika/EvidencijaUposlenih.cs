using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BLLKlinika
{
    public static class EvidencijaUposlenih
    {
        private static List<Uposleni> _uposleni = new List<Uposleni>();
        private static int _idGeneratorUposleni = 0;
        private static string _defaultPassword = "password"; // safest system in the world (no time for hash)

        private static string GenerateUsername(string ime, string prezime, int id)
        {
            return (ime[0] + prezime + id).ToLower();
        }

        public static Uposleni GetUposleniById(int idUposlenog)
        {
            if(idUposlenog < 0 || idUposlenog >= _idGeneratorUposleni)
            {
                throw new ArgumentException("Uposleni sa id " + idUposlenog + " ne postoji");
            }
            return _uposleni.Find(p => p.Id == idUposlenog);
        }

        #region Dodavanje uposlenih

        public static int DodajCistac(string ime, string prezime)
        {
            UposleniCistac temp = new UposleniCistac(ime, prezime);
            temp.Id = _idGeneratorUposleni++;
            temp.Username = GenerateUsername(ime, prezime, temp.Id);
            temp.Password = _defaultPassword;
            _uposleni.Add(temp);
            return temp.Id;
        }

        public static int DodajDoktor(string ime, string prezime, int idOrdinacije)
        {
            UposleniDoktor temp = new UposleniDoktor(ime, prezime, idOrdinacije);
            temp.Id = _idGeneratorUposleni++;
            temp.Username = GenerateUsername(ime, prezime, temp.Id);
            temp.Password = _defaultPassword;
            _uposleni.Add(temp);
            return temp.Id;
        }
        public static int DodajTech(string ime, string prezime)
        {
            UposleniTech temp = new UposleniTech(ime, prezime);
            temp.Id = _idGeneratorUposleni++;
            temp.Username = GenerateUsername(ime, prezime, temp.Id);
            temp.Password = _defaultPassword;
            _uposleni.Add(temp);
            return temp.Id;
        }
        public static int DodajTehnicar(string ime, string prezime)
        {
            UposleniTehnicar temp = new UposleniTehnicar(ime, prezime);
            temp.Id = _idGeneratorUposleni++;
            temp.Username = GenerateUsername(ime, prezime, temp.Id);
            temp.Password = _defaultPassword;
            _uposleni.Add(temp);
            return temp.Id;
        }
        public static int DodajUprava(string ime, string prezime)
        {
            UposleniUprava temp = new UposleniUprava(ime, prezime);
            temp.Id = _idGeneratorUposleni++;
            temp.Username = GenerateUsername(ime, prezime, temp.Id);
            temp.Password = _defaultPassword;
            _uposleni.Add(temp);
            return temp.Id;
        }
        #endregion

        public static void UpdatePassword(int idUposlenog, string password)
        {
            Uposleni temp = GetUposleniById(idUposlenog);
            temp.Password = password;
        }
        public static void ObracunajPlate() // !!!
        {
            foreach(Uposleni x in _uposleni)
            {
                if(x is UposleniDoktor)
                {
                    (x as UposleniDoktor).ObracunBonusa();
                }
            }
        }
    }
}
