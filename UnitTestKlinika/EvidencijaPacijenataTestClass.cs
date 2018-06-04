using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLLKlinika;
using System.Collections.Generic;

namespace UnitTestKlinika
{
    [TestClass]
    public class EvidencijaPacijenataTestClass
    {
        private static int id1, id2, id3;
        private static int ids1, ids2, ids3;

        public TestContext TestContext { get; set; }

        [ClassInitialize]
        public static void Populate(TestContext testContext)
        {
            id1 = EvidencijaPacijenata.AddPacijent("Caleb", "McLaughlin");
            id2 = EvidencijaPacijenata.AddPacijent("Natalia", "Dyer");
            id3 = EvidencijaPacijenata.AddPacijent("Charlie", "Heaton");

            ids1 = EvidencijaPacijenata.AddPacijentStub("Joe", "Keery");
            ids2 = EvidencijaPacijenata.AddPacijentStub("Noah", "Schnapp");
            ids3 = EvidencijaPacijenata.AddPacijentStub("Dacre", "Montgomery");

            EvidencijaPacijenata.DodajPodatkePacijenta(id1, new DateTime(2001, 10, 13), Spol.Muski, "-", BracnoStanje.Nevjencan);
            EvidencijaPacijenata.DodajPodatkePacijenta(id2, new DateTime(1997, 1, 13), Spol.Zenski, "-", BracnoStanje.Nevjencan);
            EvidencijaPacijenata.DodajPodatkePacijenta(id3, new DateTime(1994, 2, 6), Spol.Muski, "-", BracnoStanje.Nevjencan);
            EvidencijaPacijenata.DodajPodatkePacijenta(ids1, new DateTime(1992, 4, 24), Spol.Muski, "-", BracnoStanje.Nevjencan);
            EvidencijaPacijenata.DodajPodatkePacijenta(ids2, new DateTime(2005, 10, 3), Spol.Muski, "-", BracnoStanje.Nevjencan);
            EvidencijaPacijenata.DodajPodatkePacijenta(ids3, new DateTime(1994, 11, 22), Spol.Muski, "-", BracnoStanje.Nevjencan);
        }

        #region Kreiranje pacijenta i anamneza

        [TestMethod]
        public void AddPacijent()
        {            
            Assert.AreEqual("Dyer", EvidencijaPacijenata.Get(id2).Prezime);
            Assert.AreEqual("Charlie", EvidencijaPacijenata.Get(id3).Ime);
        }
        [TestMethod]
        public void AddPacijentDetalji()
        {
            Assert.AreEqual(new DateTime(1997, 1, 13), EvidencijaPacijenata.Get(id2).DatumRodjenja);
        }
        [TestMethod]
        public void Anamneza() // u suštini test na nullreference
        {
            EvidencijaPacijenata.EvidentirajPorodicnoZdravstvenoStanje(id3, "otac", "šizofrenija"); // idkkkkk
            EvidencijaPacijenata.EvidentirajAlergiju(id2, "kikiriki", true);
            EvidencijaPacijenata.EvidentirajBolest(id1, "bolest", false);
        }

        #endregion

        // heh

        #region Sistematski

        [TestMethod]
        public void ZakaziSistematskiPregled()
        {
            int idPregleda = EvidencijaPacijenata.ZakaziSistematskiPregled(id1);
            List<string> potrebniPregledi = EvidencijaPacijenata.PotrebniPreglediSistematski(id1, idPregleda);
            Assert.AreEqual("Oftamolog", potrebniPregledi[0]);
            Assert.AreEqual("Neuropsihijatar", potrebniPregledi[1]);
            Assert.AreEqual("Psiholog", potrebniPregledi[2]);
            Assert.AreEqual("Opci", potrebniPregledi[3]);
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", @"|DataDirectory|\\podaci.xml", "PregledStavka", DataAccessMethod.Sequential), DeploymentItem("podaci.xml"), TestMethod]
        public void ObaviSistematskiPregled()
        {
            int idPregleda = EvidencijaPacijenata.ZakaziSistematskiPregled(id2);
            
            EvidencijaPacijenata.ObaviStavkuSistematskog(id2,
                idPregleda,
                DateTime.Now,
                TestContext.DataRow["RezultatPregleda"].ToString(),
                Convert.ToBoolean(TestContext.DataRow["UspjesanPregled"]),
                MyConvert.ToTipSistematskog(TestContext.DataRow["TipSistematskog"].ToString()));

            Assert.AreEqual(3, EvidencijaPacijenata.PotrebniPreglediSistematski(id2, idPregleda).Count, "urađen jedan pregled");

            // nije baš ono što sam zamislila
            // for the record
            // i ovo ispod je radilo

            //Assert.AreEqual(3, EvidencijaPacijenata.PotrebniPreglediSistematski(id2, idPregleda).Count);            
            //EvidencijaPacijenata.ObaviStavkuSistematskog(id2, idPregleda, DateTime.Now, "ok", true, TipSistematskog.Oftamolog);
            //EvidencijaPacijenata.ObaviStavkuSistematskog(id2, idPregleda, DateTime.Now, "ok", true, TipSistematskog.Psiholog);
            //EvidencijaPacijenata.ObaviStavkuSistematskog(id2, idPregleda, DateTime.Now, "ok", true, TipSistematskog.Opci);
            //Assert.AreEqual(0, EvidencijaPacijenata.PotrebniPreglediSistematski(id2, idPregleda).Count);
            //Pacijent temp = EvidencijaPacijenata.Get(id2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ObaviSistematskiPregledWrongId()
        {
            EvidencijaPacijenata.ObaviStavkuSistematskog(id1, 100, DateTime.Now, "ok", true, TipSistematskog.Oftamolog);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ObaviSistematskiPregledSomeoneElsesId()
        {
            int idPregleda1 = EvidencijaPacijenata.ZakaziSistematskiPregled(id1);
            int idPregleda2 = EvidencijaPacijenata.ZakaziSistematskiPregled(id2);

            EvidencijaPacijenata.ObaviStavkuSistematskog(id1, idPregleda2, DateTime.Now, "ok", true, TipSistematskog.Opci);
        }

        #endregion

        #region Pregledi

        [TestMethod]
        public void DodajHitniPregled()
        {
            EvidencijaPacijenata.DodajHitniPregled(id1, DateTime.Now, 10, "defibrilacija", true, "-");
            
            CollectionAssert.AllItemsAreInstancesOfType(EvidencijaPacijenata.Get(id1).Karton.Pregledi, typeof(PregledHitni));
            Assert.IsNotNull((EvidencijaPacijenata.Get(id1).Karton.Pregledi[EvidencijaPacijenata.Get(id1).Karton.Pregledi.Count - 1] as PregledHitni).PrvaPomocUradjena);
        }

        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "|DataDirectory|\\podaci.csv", "podaci#csv", DataAccessMethod.Sequential), DeploymentItem("podaci.csv"), TestMethod]
        public void KreirajNormalniPregled()
        {

            EvidencijaPacijenata.KreirajNormalniPregled(id3);

            EvidencijaPacijenata.DodajLijekUTerapiju(id3, 
                TestContext.DataRow["NazivLijeka"].ToString(), 
                Convert.ToDouble(TestContext.DataRow["KolicinaLijeka"]), 
                TestContext.DataRow["MjeraLijeka"].ToString(),
                Convert.ToInt32(TestContext.DataRow["FrekvencijaUzimanjaLijeka"]),
                TestContext.DataRow["MjeraFrekvencijeUzimanjaLijeka"].ToString());

            EvidencijaPacijenata.DodajKratkorocnuTerapiju(id3, DateTime.Now, DateTime.Now.AddDays(5));
            
            EvidencijaPacijenata.DodajDetaljeNormalnogPregleda(id3, 
                DateTime.Now, 
                Convert.ToInt32(TestContext.DataRow["CijenaPregleda"]), 
                TestContext.DataRow["NazivPregleda"].ToString(), 
                TestContext.DataRow["MisljenjeDoktora"].ToString(),
                TestContext.DataRow["NazivBolesti"].ToString());

            EvidencijaPacijenata.DodajNormalniPregled(id3);

            PregledNormalni pn = EvidencijaPacijenata.Get(id3).Karton.Pregledi.Find(p => (p as PregledNormalni).MisljenjeDoktora == "ok") as PregledNormalni;

            Assert.AreEqual("brufen", pn.Terapija.Lijekovi[0].Naziv);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void KreirajNormalniPregledInvalid()
        {
            EvidencijaPacijenata.DodajDetaljeNormalnogPregleda(id1, DateTime.Now, 10, "pregled", "mišljenje", "rezultat");
            EvidencijaPacijenata.DodajNormalniPregled(id1);
        }

        #endregion

        #region Plaćanje 

        [TestMethod]
        public void PlacanjeSistematskog()
        {
            int id = EvidencijaPacijenata.AddPacijent("Sadie", "Sink");
            EvidencijaPacijenata.DodajPodatkePacijenta(id, new DateTime(2005, 4, 16), Spol.Zenski, "-", BracnoStanje.Nevjencan);

            int idSistematskog = EvidencijaPacijenata.ZakaziSistematskiPregled(id);
            EvidencijaPacijenata.ObaviStavkuSistematskog(id, idSistematskog, DateTime.Now, "ok", true, TipSistematskog.Opci);
            EvidencijaPacijenata.ObaviStavkuSistematskog(id, idSistematskog, DateTime.Now, "ok", true, TipSistematskog.Neuropsihijatar);
            EvidencijaPacijenata.ObaviStavkuSistematskog(id, idSistematskog, DateTime.Now, "ok", true, TipSistematskog.Psiholog);
            EvidencijaPacijenata.ObaviStavkuSistematskog(id, idSistematskog, DateTime.Now, "ok", true, TipSistematskog.Oftamolog);

            EvidencijaPacijenata.PlacanjeRateIspostaviRacun(id);

            EvidencijaPacijenata.PlacanjeRateIzvrsiPlacanje(id);
            EvidencijaPacijenata.PlacanjeRateIzvrsiPlacanje(id);
            EvidencijaPacijenata.PlacanjeRateIzvrsiPlacanje(id);
            EvidencijaPacijenata.PlacanjeRateIzvrsiPlacanje(id);
            EvidencijaPacijenata.PlacanjeRateIzvrsiPlacanje(id);
            EvidencijaPacijenata.PlacanjeRateIzvrsiPlacanje(id);

            Assert.AreEqual(0, EvidencijaPacijenata.Get(id).FiskalniRacun.NeplaceniIznos, "plaćene sve rate");
            StringAssert.Contains(EvidencijaPacijenata.PlacanjeRateIspostaviRacun(id), "total: 0");
            Assert.AreEqual(0, EvidencijaPacijenata.Get(id).FiskalniRacun.AktivniSistematskiPregledi.Count, "broj aktivnih sistematskih pregleda - plaćene rate");

            idSistematskog = EvidencijaPacijenata.ZakaziSistematskiPregled(id);
            EvidencijaPacijenata.ObaviStavkuSistematskog(id, idSistematskog, DateTime.Now, "ok", true, TipSistematskog.Opci);
            EvidencijaPacijenata.ObaviStavkuSistematskog(id, idSistematskog, DateTime.Now, "ok", true, TipSistematskog.Neuropsihijatar);
            EvidencijaPacijenata.ObaviStavkuSistematskog(id, idSistematskog, DateTime.Now, "ok", true, TipSistematskog.Psiholog);
            EvidencijaPacijenata.ObaviStavkuSistematskog(id, idSistematskog, DateTime.Now, "ok", true, TipSistematskog.Oftamolog);

            EvidencijaPacijenata.PlacanjeGotovinaIspostaviRacun(id); 

            EvidencijaPacijenata.PlacanjeGotovinaIzvrsiPlacanje(id);
            
            Assert.AreEqual(0, EvidencijaPacijenata.Get(id).FiskalniRacun.NeplaceniIznos, "plaćeno gotovinom");
            StringAssert.Contains(EvidencijaPacijenata.PlacanjeRateIspostaviRacun(id), "total: 0");
            Assert.AreEqual(0, EvidencijaPacijenata.Get(id).FiskalniRacun.AktivniSistematskiPregledi.Count, "broj aktivnih sistematskih pregleda - plaćeno gotovinom");
        }

        [TestMethod]
        public void PlacanjeRate()
        {
            EvidencijaPacijenata.DodajHitniPregled(id1, DateTime.Now, 10, "defibrilacija", true, "-");
            EvidencijaPacijenata.DodajHitniPregled(id1, DateTime.Now, 10, "defibrilacija", true, "-");
            EvidencijaPacijenata.DodajHitniPregled(id1, DateTime.Now, 10, "defibrilacija", true, "-");

            StringAssert.Contains(EvidencijaPacijenata.PlacanjeRateIspostaviRacun(id1), "defibrilacija");
            
            EvidencijaPacijenata.PlacanjeRateIzvrsiPlacanje(id1);
            Assert.AreNotEqual(0, EvidencijaPacijenata.Get(id1).FiskalniRacun.NeplaceniIznos);
            EvidencijaPacijenata.PlacanjeRateIzvrsiPlacanje(id1);
            EvidencijaPacijenata.PlacanjeRateIzvrsiPlacanje(id1);
            EvidencijaPacijenata.PlacanjeRateIzvrsiPlacanje(id1);
            Assert.AreNotEqual(0, EvidencijaPacijenata.Get(id1).FiskalniRacun.NeplaceniIznos);
            EvidencijaPacijenata.PlacanjeRateIzvrsiPlacanje(id1);
            EvidencijaPacijenata.PlacanjeRateIzvrsiPlacanje(id1);

            Assert.AreEqual(0, EvidencijaPacijenata.Get(id1).FiskalniRacun.NeplaceniIznos);
            StringAssert.Contains(EvidencijaPacijenata.PlacanjeRateIspostaviRacun(id1), "total: 0");
            Assert.AreEqual(0, EvidencijaPacijenata.Get(id1).FiskalniRacun.AktivniPregledi.Count);
        }

        [TestMethod]
        public void PlacanjeGotovina()
        {
            EvidencijaPacijenata.DodajHitniPregled(id2, DateTime.Now, 10, "reanimacija", true, "-");
            EvidencijaPacijenata.DodajHitniPregled(id2, DateTime.Now, 10, "reanimacija", true, "-");
            EvidencijaPacijenata.DodajHitniPregled(id2, DateTime.Now, 10, "reanimacija", true, "-");

            StringAssert.Contains(EvidencijaPacijenata.PlacanjeGotovinaIspostaviRacun(id2), "reanimacija");

            Assert.AreNotEqual(0, EvidencijaPacijenata.Get(id2).FiskalniRacun.NeplaceniIznos);
            EvidencijaPacijenata.PlacanjeGotovinaIzvrsiPlacanje(id2);
            Assert.AreEqual(0, EvidencijaPacijenata.Get(id2).FiskalniRacun.NeplaceniIznos);

        }

        [TestMethod]
        public void PlacanjeObojeRedovni()
        {
            EvidencijaPacijenata.DodajHitniPregled(id2, DateTime.Now, 10, "pregled 1", true, "-");
            EvidencijaPacijenata.DodajHitniPregled(id2, DateTime.Now, 10, "pregled 2", true, "-");
            EvidencijaPacijenata.DodajHitniPregled(id2, DateTime.Now, 10, "pregled 2", true, "-");
            EvidencijaPacijenata.DodajHitniPregled(id2, DateTime.Now, 10, "pregled 2", true, "-");

            EvidencijaPacijenata.PlacanjeRateIspostaviRacun(id2);
            decimal cijena1 = EvidencijaPacijenata.Get(id2).FiskalniRacun.NeplaceniIznos;
            EvidencijaPacijenata.PlacanjeGotovinaIspostaviRacun(id2); ;
            decimal cijena2 = EvidencijaPacijenata.Get(id2).FiskalniRacun.NeplaceniIznos;

            Assert.AreNotEqual(cijena1, cijena2);

        }

        [TestMethod]
        public void PlacanjeObojeNeredovni()
        {
            int id4 = EvidencijaPacijenata.AddPacijent("Cara", "Buono");
            EvidencijaPacijenata.DodajPodatkePacijenta(id4, new DateTime(1974, 3, 1), Spol.Zenski, "-", BracnoStanje.Vjencan);
            EvidencijaPacijenata.DodajHitniPregled(id4, DateTime.Now, 30, "pregled", true, "razlog");

            EvidencijaPacijenata.PlacanjeGotovinaIspostaviRacun(id4);
            decimal cijena1 = EvidencijaPacijenata.Get(id4).FiskalniRacun.NeplaceniIznos;
            EvidencijaPacijenata.PlacanjeRateIspostaviRacun(id4);
            decimal cijena2 = EvidencijaPacijenata.Get(id4).FiskalniRacun.NeplaceniIznos;

            Assert.AreNotEqual(cijena1, cijena2);
        }

        #endregion

        #region Testiranje stubova

        [TestMethod]
        public void HistorijaPacijenta()
        {
            Assert.IsInstanceOfType((EvidencijaPacijenata.Get(ids1) as PacijentStub).historijaPacijenta(), typeof(Karton));
            /*
            EvidencijaPacijenata.DodajHitniPregled(ids1, DateTime.Now, 20, "-", false, "-");
            Assert.AreEqual(1, (EvidencijaPacijenata.Get(ids1) as PacijentStub).historijaPacijenta().Pregledi.Count);
            */
        }

        [TestMethod]
        public void PrikaziRanijeTerapije()
        {
            Assert.IsInstanceOfType((EvidencijaPacijenata.Get(ids2) as PacijentStub).prikaziRanijeTerapije(), typeof(List<Terapija>));

            // *puts hard hat on* time to create some regular exams ;_;
            // update: or NOT

            /*
            EvidencijaPacijenata.KreirajNormalniPregled(ids2);
            EvidencijaPacijenata.DodajLijekUTerapiju(ids2, "xiclav", 1, "tableta", 1, "dnevno");
            EvidencijaPacijenata.DodajKratkorocnuTerapiju(ids2, DateTime.Now, DateTime.Now.AddDays(5));
            EvidencijaPacijenata.DodajDetaljeNormalnogPregleda(ids2, DateTime.Now, 30, "opći pregled", "-", "upala or whatever");
            EvidencijaPacijenata.DodajNormalniPregled(ids2);

            EvidencijaPacijenata.KreirajNormalniPregled(ids2);
            EvidencijaPacijenata.DodajLijekUTerapiju(ids2, "xiclav", 1, "tableta", 1, "dnevno");
            EvidencijaPacijenata.DodajKratkorocnuTerapiju(ids2, DateTime.Now.AddYears(-2), DateTime.Now.AddYears(-2).AddDays(5));
            EvidencijaPacijenata.DodajDetaljeNormalnogPregleda(ids2, DateTime.Now.AddYears(-2), 30, "opći pregled", "-", "upala or whatever");
            EvidencijaPacijenata.DodajNormalniPregled(ids2);

            var temp = (EvidencijaPacijenata.Get(ids2) as PacijentStub).prikaziRanijeTerapije();
            CollectionAssert.AllItemsAreInstancesOfType(temp, typeof(TerapijaKratkorocna));

            StringAssert.StartsWith(EvidencijaPacijenata.Get(ids2).Karton.Bolesti[0].Naziv, "upala");
            */
        }

        [TestMethod]
        public void TrenutnaTerapija()
        {
            Assert.IsInstanceOfType((EvidencijaPacijenata.Get(ids3) as PacijentStub).trenutnaTerapija(), typeof(Terapija));
            /*
            EvidencijaPacijenata.KreirajNormalniPregled(ids3);
            EvidencijaPacijenata.DodajLijekUTerapiju(ids3, "first xiclav", 1, "tableta", 1, "dnevno");
            EvidencijaPacijenata.DodajKratkorocnuTerapiju(ids3, DateTime.Now, DateTime.Now.AddDays(5));
            EvidencijaPacijenata.DodajDetaljeNormalnogPregleda(ids3, DateTime.Now, 30, "upala or whatever", "-", "-");
            EvidencijaPacijenata.DodajNormalniPregled(ids3);

            EvidencijaPacijenata.KreirajNormalniPregled(ids3);
            EvidencijaPacijenata.DodajLijekUTerapiju(ids3, "xiclav", 1, "tableta", 1, "dnevno");
            EvidencijaPacijenata.DodajKratkorocnuTerapiju(ids3, DateTime.Now.AddYears(-2), DateTime.Now.AddYears(-2).AddDays(5));
            EvidencijaPacijenata.DodajDetaljeNormalnogPregleda(ids3, DateTime.Now.AddYears(-2), 30, "upala or whatever", "-", "-");
            EvidencijaPacijenata.DodajNormalniPregled(ids3);

            var temp = (EvidencijaPacijenata.Get(ids3) as PacijentStub).trenutnaTerapija();

            StringAssert.StartsWith(temp.Lijekovi[0].Naziv, "first");
            */
        }

        #endregion

        #region Testiranje Mockane Laboratorije

        [TestMethod]
        public void LabMock()
        {
            // i have no idea what im doin here
            LaboratorijaMock lab = new LaboratorijaMock();

            // normalne vrijednosti? :/

            Assert.IsTrue(lab.BrojEritrocita(id1) >= 4700000 && lab.BrojEritrocita(id1) <= 6100000);
            Assert.IsTrue(lab.BrojLeukocita(id1) >= 4300 && lab.BrojLeukocita(id1) <= 10800);

            Assert.AreEqual("AB+", lab.KrvnaGrupa(id1));
            
        }
        #endregion
    }
}
