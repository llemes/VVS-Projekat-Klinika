using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLLKlinika;

namespace UnitTestKlinika
{
    [TestClass]
    public class EvidencijaOrdinacijaTestClass
    {
        static int id1, id2, id3, id4;
        [ClassInitialize]
        public static void Populate(TestContext testContext)
        {
            id1 = EvidencijaOrdinacija.DodajOrdinaciju("radiologija");
            id2 = EvidencijaOrdinacija.DodajOrdinaciju("hirurgija");
            id3 = EvidencijaOrdinacija.DodajOrdinaciju("ortoped");
            id4 = EvidencijaOrdinacija.DodajOrdinaciju("kardiolog");
        }
        [TestMethod]
        public void DodajOrdinaciju()
        {
            Assert.AreEqual("hirurgija", EvidencijaOrdinacija.GetOrdinacijaById(id2).Naziv);
            Assert.IsNotNull(EvidencijaOrdinacija.GetOrdinacijaById(id2).OpsluzeniPacijenti);
            Assert.IsNotNull(EvidencijaOrdinacija.GetOrdinacijaById(id2).RedCekanja);
            Assert.IsNotNull(EvidencijaOrdinacija.GetOrdinacijaById(id2).Aparati);
        }

        [TestMethod]
        public void KreirajAparat()
        {
            int idAparata = EvidencijaOrdinacija.DodajAparat(id4, "EKG");
            Aparat temp = EvidencijaOrdinacija.Get(idAparata, id4);
            Assert.AreEqual("EKG", temp.Naziv);
        }
        [TestMethod]
        public void EvidentirajRadAparata()
        {
            int idAparata = EvidencijaOrdinacija.DodajAparat(id4, "EKG");

            DateTime uključen = DateTime.Now;
            EvidencijaOrdinacija.EvidentirajRadAparata(id4, idAparata, uključen, uključen.AddHours(3));

            Aparat temp = EvidencijaOrdinacija.Get(idAparata, id4);

            Assert.AreEqual(uključen, temp.AktivnoVrijeme[temp.AktivnoVrijeme.Count - 1].Item1);
            Assert.IsTrue(temp.Aktivan());
        }

        [TestMethod]
        public void DodajPacijenta()
        {
            int pid1, pid2, pid3;
            pid1 = EvidencijaPacijenata.AddPacijent("Caleb", "McLaughlin");
            pid2 = EvidencijaPacijenata.AddPacijent("Natalia", "Dyer");
            pid3 = EvidencijaPacijenata.AddPacijent("Charlie", "Heaton");

            EvidencijaPacijenata.DodajPodatkePacijenta(id2, new DateTime(1997, 1, 13), Spol.Zenski, "", BracnoStanje.Nevjencan);
            EvidencijaPacijenata.DodajPodatkePacijenta(id3, new DateTime(1994, 2, 6), Spol.Muski, "", BracnoStanje.Nevjencan);

            EvidencijaOrdinacija.DodajPacijenta(id1, pid1);
            EvidencijaOrdinacija.DodajPacijenta(id1, pid2);
            EvidencijaOrdinacija.DodajPacijenta(id2, pid2);
            EvidencijaOrdinacija.DodajPacijenta(id4, pid3);

            //Assert.AreEqual(2, EvidencijaOrdinacija.Get(id1).RedCekanja.Count);
        }

        [TestMethod]
        public void OpsluziPacijenta()
        {
            int pid1, pid2, pid3;
            pid1 = EvidencijaPacijenata.AddPacijent("Caleb", "McLaughlin");
            pid2 = EvidencijaPacijenata.AddPacijent("Natalia", "Dyer");
            pid3 = EvidencijaPacijenata.AddPacijent("Charlie", "Heaton");

            EvidencijaPacijenata.DodajPodatkePacijenta(id2, new DateTime(1997, 1, 13), Spol.Zenski, "", BracnoStanje.Nevjencan);
            EvidencijaPacijenata.DodajPodatkePacijenta(id3, new DateTime(1994, 2, 6), Spol.Muski, "", BracnoStanje.Nevjencan);

            EvidencijaOrdinacija.DodajPacijenta(id1, pid1);
            EvidencijaOrdinacija.DodajPacijenta(id1, pid2);
            
            EvidencijaOrdinacija.OpsluziPacijenta(id1);

            //Assert.AreEqual(1, EvidencijaOrdinacija.Get(id1).RedCekanja.Count);
            //Assert.AreEqual(1, EvidencijaOrdinacija.Get(id1).OpsluzeniPacijenti.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void OpsluziNepostojecegPacijenta()
        {
            int pid1, pid2, pid3;
            pid1 = EvidencijaPacijenata.AddPacijent("Caleb", "McLaughlin");
            pid2 = EvidencijaPacijenata.AddPacijent("Natalia", "Dyer");
            pid3 = EvidencijaPacijenata.AddPacijent("Charlie", "Heaton");

            EvidencijaPacijenata.DodajPodatkePacijenta(id2, new DateTime(1997, 1, 13), Spol.Zenski, "", BracnoStanje.Nevjencan);
            EvidencijaPacijenata.DodajPodatkePacijenta(id3, new DateTime(1994, 2, 6), Spol.Muski, "", BracnoStanje.Nevjencan);

            EvidencijaOrdinacija.DodajPacijenta(id1, pid1);
            EvidencijaOrdinacija.DodajPacijenta(id1, pid2);
            EvidencijaOrdinacija.DodajPacijenta(id2, pid2);
            EvidencijaOrdinacija.DodajPacijenta(id4, pid3);

            EvidencijaOrdinacija.OpsluziPacijenta(id3);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetIdNajslobodnijeOrdinacijeNoMatch()
        {
            EvidencijaOrdinacija.GetIdNajslobodnijeOrdinacije("urologija");
        }

        [TestMethod]
        public void GetIdNajslobodnijeOrdinacije()
        {
            int pid1, pid2, pid3;
            pid1 = EvidencijaPacijenata.AddPacijent("Caleb", "McLaughlin");
            pid2 = EvidencijaPacijenata.AddPacijent("Natalia", "Dyer");
            pid3 = EvidencijaPacijenata.AddPacijent("Charlie", "Heaton");

            int idRadiologija = EvidencijaOrdinacija.DodajOrdinaciju("dummy");
            int idRadiologija1 = EvidencijaOrdinacija.DodajOrdinaciju("dummy1");

            EvidencijaPacijenata.DodajPodatkePacijenta(id2, new DateTime(1997, 1, 13), Spol.Zenski, "", BracnoStanje.Nevjencan);
            EvidencijaPacijenata.DodajPodatkePacijenta(id3, new DateTime(1994, 2, 6), Spol.Muski, "", BracnoStanje.Nevjencan);

            EvidencijaOrdinacija.DodajPacijenta(idRadiologija1, pid1);
            EvidencijaOrdinacija.DodajPacijenta(idRadiologija1, pid2);
            EvidencijaOrdinacija.DodajPacijenta(idRadiologija, pid2);
            EvidencijaOrdinacija.DodajPacijenta(idRadiologija, pid3);
            EvidencijaOrdinacija.DodajPacijenta(idRadiologija, pid1);

            Assert.AreEqual(idRadiologija1, EvidencijaOrdinacija.GetIdNajslobodnijeOrdinacije("dummy1"));
        }
    }
}
