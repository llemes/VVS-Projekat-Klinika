using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLKlinika
{
    public static partial class EvidencijaPacijenata // single responsibility LUL
    {
        private static List<Pacijent> _pacijenti = new List<Pacijent>();
        private static int _idGeneratorPacijent = 0;
        private static int _idGeneratorPregledSistematski = 0;

        public static bool TestMode = false;

        public static Pacijent Get(int idPacijenta)
        {
            if(idPacijenta < 0 || idPacijenta > _idGeneratorPacijent)
            {
                throw new ArgumentException("Pacijent sa id " + idPacijenta + " ne postoji");
            }
            return _pacijenti.Find(p => p.Id == idPacijenta);
        }

        #region Kreiranje Stub Pacijenta 

        public static int AddPacijentStub(string ime, string prezime) // ostalo je isto
        {
            DataValidator.ValidateName(ime, prezime);

            PacijentStub tempPacijent = new PacijentStub();

            // instanciranja
            tempPacijent.Karton = new Karton();
            tempPacijent.Karton.Pregledi = new List<Pregled>();
            tempPacijent.Karton.PreglediSistematski = new List<PregledSistematski>();
            tempPacijent.Karton.Bolesti = new List<Bolest>();
            tempPacijent.Karton.Alergije = new List<Alergija>();
            tempPacijent.Karton.ZdravstvenaHistorijaPorodice = new List<Tuple<string, string>>();

            tempPacijent.FiskalniRacun = new FiskalniRacun();

            tempPacijent.Ime = ime;
            tempPacijent.Prezime = prezime;
            tempPacijent.Id = _idGeneratorPacijent++;
            _pacijenti.Add(tempPacijent);

            return tempPacijent.Id;
        }

        #endregion

        #region KreiranjePacijenta
        public static int AddPacijent(string ime, string prezime)
        {
            DataValidator.ValidateName(ime, prezime);

            Pacijent tempPacijent = new Pacijent();

            // instanciranja
            tempPacijent.Karton = new Karton();
            tempPacijent.Karton.Pregledi = new List<Pregled>();
            tempPacijent.Karton.PreglediSistematski = new List<PregledSistematski>();
            tempPacijent.Karton.Bolesti = new List<Bolest>();
            tempPacijent.Karton.Alergije = new List<Alergija>();
            tempPacijent.Karton.ZdravstvenaHistorijaPorodice = new List<Tuple<string, string>>();

            tempPacijent.FiskalniRacun = new FiskalniRacun();

            tempPacijent.Ime = ime;
            tempPacijent.Prezime = prezime;
            tempPacijent.Id = _idGeneratorPacijent++;
            _pacijenti.Add(tempPacijent);

            return tempPacijent.Id;
        }
        public static void DodajPodatkePacijenta(int idPacijenta, DateTime datumRodjenja, Spol spol, string adresaStanovanja, BracnoStanje bracnoStanje)
        {
            Pacijent temp = Get(idPacijenta);
            temp.DatumRodjenja = datumRodjenja;
            temp.Spol = spol;
            temp.AdresaStanovanja = adresaStanovanja;
            temp.BracnoStanje = bracnoStanje;
        }
        #endregion

        #region Anamneza
        
        public static void EvidentirajPorodicnoZdravstvenoStanje(int idPacijenta, string clanPorodice, string bolest)
        {
            Pacijent temp = Get(idPacijenta);
            temp.Karton.Porodica(new Tuple<string, string>(clanPorodice, bolest));
        }
        public static void EvidentirajAlergiju(int idPacijenta, string nazivAlergije, bool aktivnaAlergija)
        {
            Pacijent temp = Get(idPacijenta);
            Alergija tempAlergija = new Alergija();
            tempAlergija.Naziv = nazivAlergije;
            tempAlergija.Aktivna = aktivnaAlergija;
            temp.Karton.Add(tempAlergija);
        }
        public static void EvidentirajBolest(int idPacijenta, string nazivBolesti, bool aktivnaBolest)
        {
            Pacijent tempPacijent = Get(idPacijenta);
            Bolest tempBolest = new Bolest(nazivBolesti, aktivnaBolest);
            tempPacijent.Karton.Add(tempBolest);
        }

        #endregion
        
        #region Sistematski
        public static int ZakaziSistematskiPregled(int idPacijenta)
        {
            Pacijent tempPacijent = Get(idPacijenta);
            PregledSistematski tempPregledSistematski = new PregledSistematski();
            tempPregledSistematski.Id = _idGeneratorPregledSistematski++;

            tempPacijent.Karton.PreglediSistematski.Add(tempPregledSistematski);
            tempPacijent.FiskalniRacun.DodajStavku(tempPregledSistematski);

            return tempPregledSistematski.Id;
        }
        public static void ObaviStavkuSistematskog(int idPacijenta, int idPregledSistematskiEvidencija, DateTime vrijemePregleda, string rezultatPregleda, bool uspjesanPregled, TipSistematskog tipPregleda)
        {
            if(idPregledSistematskiEvidencija < 0 || idPregledSistematskiEvidencija >= _idGeneratorPregledSistematski)
            {
                throw new ArgumentException("Sistematski pregled sa id " + idPregledSistematskiEvidencija + " ne postoji");
            }

            Pacijent tempPacijent = Get(idPacijenta);
            
            try
            {
                PregledSistematski tempPregledSistematskiEvidencija = null;
                tempPregledSistematskiEvidencija = tempPacijent.Karton.PreglediSistematski.Find(p => p.Id == idPregledSistematskiEvidencija);

                if(tempPregledSistematskiEvidencija == null)
                {
                    throw new ArgumentException("Pacijent sa id " + idPacijenta + " nije zakazao pregled sa id " + idPregledSistematskiEvidencija);
                }

                tempPregledSistematskiEvidencija.PregledEvidencija(vrijemePregleda, rezultatPregleda, uspjesanPregled, tipPregleda);
                tempPacijent.FiskalniRacun.DodajStavku(tempPregledSistematskiEvidencija);
                tempPacijent.BrojPosjeta++;
            }
            catch(ArgumentNullException e)
            {
                throw new ArgumentException("Pacijent sa id " + idPacijenta + " nije zakazao pregled sa id " + idPregledSistematskiEvidencija);
            }
        }
        public static List<string> PotrebniPreglediSistematski(int idPacijenta, int idPregledSistematskiEvidencija)
        {
            if (idPregledSistematskiEvidencija < 0 || idPregledSistematskiEvidencija > _idGeneratorPregledSistematski)
            {
                throw new ArgumentException("Sistematski pregled sa id " + idPregledSistematskiEvidencija + " ne postoji");
            }

            Pacijent tempPacijent = Get(idPacijenta);
            PregledSistematski temp = tempPacijent.Karton.PreglediSistematski.Find(p => p.Id == idPregledSistematskiEvidencija);
            return temp.PotrebniPregledi();
        }
        #endregion

        #region Hitni pregled 

        public static void DodajHitniPregled(int idPacijenta, DateTime vrijemePregleda, decimal cijena, string naziv, bool prvaPomocUradjena, string prvaPomocRazlog)
        {
            Pacijent temp = Get(idPacijenta);
            PregledHitni tempPregled = new PregledHitni(vrijemePregleda, cijena, naziv, prvaPomocUradjena, prvaPomocRazlog);

            temp.Karton.Add(tempPregled);
            temp.FiskalniRacun.DodajStavku(tempPregled);
            temp.BrojPosjeta++;
        }

        public static void DodajHitniPregledSmrt(int idPacijenta, DateTime vrijemePregleda, decimal cijena, string naziv, bool prvaPomocUradjena, string prvaPomocRazlog, DateTime vrijemeSmrti, string uzrokSmrti, DateTime terminObdukcije)
        {
            Pacijent temp = Get(idPacijenta);
            PregledHitni tempPregled = new PregledHitni(vrijemePregleda, cijena, naziv, prvaPomocUradjena, prvaPomocRazlog);
            tempPregled.SmrtEvidencija(vrijemeSmrti, uzrokSmrti, terminObdukcije);

            temp.Karton.Add(tempPregled);
            temp.FiskalniRacun.DodajStavku(tempPregled); // i think we got a loophole here
        }

        #endregion

        #region Normalni pregled

        public static void DodajNormalniPregled(int idPacijenta)
        {
            Pacijent temp = Get(idPacijenta);

            temp.Karton.Add(temp.PregledNormalniBuilder.GetPregled());
            temp.Karton.Bolesti.Add(temp.PregledNormalniBuilder.GetPregled().Rezultat);
            temp.FiskalniRacun.DodajStavku(temp.PregledNormalniBuilder.GetPregled());
            temp.BrojPosjeta++;
        }
        public static void KreirajNormalniPregled(int idPacijenta)
        {
            Pacijent temp = Get(idPacijenta);
            temp.PregledNormalniBuilder = new PregledNormalniBuilder();
        }
        public static void DodajLijekUTerapiju(int idPacijenta, string nazivLijeka, double kolicina, string mjera, int frekvencijaUzimanja, string mjeraFrekvencijeUzimanja)
        {
            Pacijent temp = Get(idPacijenta);
            temp.PregledNormalniBuilder.ConstructLijekovi(nazivLijeka, kolicina, mjera, frekvencijaUzimanja, mjeraFrekvencijeUzimanja);   
        }
        public static void DodajKratkorocnuTerapiju(int idPacijenta, DateTime datumPotpisivanja, DateTime krajTerapije)
        {
            Pacijent temp = Get(idPacijenta);
            temp.PregledNormalniBuilder.ConstructTerapijaKratkorocna(datumPotpisivanja, krajTerapije);
        }
        public static void DodajDugorocnuTerapiju(int idPacijenta, DateTime datumPotpisivanja)
        {
            Pacijent temp = Get(idPacijenta);
            temp.PregledNormalniBuilder.ConstructTerapijaDugorocna(datumPotpisivanja);
        }
        public static void DodajDetaljeNormalnogPregleda(int idPacijenta, DateTime vrijemePregleda, decimal cijenaPregleda, string nazivPregleda, string misljenjeDoktora, string nazivBolesti)
        {
            Pacijent temp = Get(idPacijenta);
            temp.PregledNormalniBuilder.ConstructPregledNormalni(vrijemePregleda, cijenaPregleda, nazivPregleda, misljenjeDoktora, nazivBolesti);
        }

        #endregion

        #region Plaćanje pregleda

        public static string PlacanjeRateIspostaviRacun(int idPacijent)
        {
            Pacijent temp = Get(idPacijent);
            string odrezak = temp.FiskalniRacun.OdrezakRate(idPacijent);
            return odrezak;
        }
        public static void PlacanjeRateIzvrsiPlacanje(int idPacijent)
        {
            Pacijent temp = Get(idPacijent);
            if(TestMode)
            {
                EvidencijaPoslovanja.EvidentirajTransakciju(DateTime.Now.AddMonths(-1), temp, temp.FiskalniRacun.PlatiRatu());
            }
            else
            {
                EvidencijaPoslovanja.EvidentirajTransakciju(DateTime.Now, temp, temp.FiskalniRacun.PlatiRatu());
            }
        }

        public static string PlacanjeGotovinaIspostaviRacun(int idPacijenta)
        {
            Pacijent temp = Get(idPacijenta);
            string odrezak = temp.FiskalniRacun.OdrezakGotovina(idPacijenta);
            return odrezak;
        }
        public static void PlacanjeGotovinaIzvrsiPlacanje(int idPacijenta)
        {
            Pacijent temp = Get(idPacijenta);
            if(TestMode)
            {
                EvidencijaPoslovanja.EvidentirajTransakciju(DateTime.Now.AddMonths(-1), temp, temp.FiskalniRacun.PlatiGotovina());
            }
            else
            {
                EvidencijaPoslovanja.EvidentirajTransakciju(DateTime.Now, temp, temp.FiskalniRacun.PlatiGotovina());
            }
        }

        #endregion
    }
}
