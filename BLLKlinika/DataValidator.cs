using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace BLLKlinika
{
    public static class DataValidator
    {
        private static Regex _regex;
        public static void ValidateName(string ime, string prezime)
        {
            _regex = new Regex("^[a-zA-Z ]*$");

            if(!_regex.IsMatch(ime))
            {
                throw new ArgumentException("Ime nije očekivanog formata");
            }

            if(!_regex.IsMatch(prezime))
            {
                throw new ArgumentException("Prezime nije očekivanog formata");
            }

        }
    }
}
