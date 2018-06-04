using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLLKlinika; // neyy kako povezati sa dll-om ako je samo dodana referenca na njega u projektu koji se testira

namespace UnitTestKlinika
{
    [TestClass]
    public class EvidencijaUposlenihTestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetInvalidId()
        {
            Uposleni u1 = EvidencijaUposlenih.Get(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetEmptyList()
        {
            Uposleni u2 = EvidencijaUposlenih.Get(1000);
        }

        [TestMethod]
        public void DodajCistac()
        {
            int id = EvidencijaUposlenih.DodajCistac("Joyce", "Byers");
            Uposleni temp = EvidencijaUposlenih.Get(id);
            Assert.AreEqual(800, temp.Plata);
            Assert.IsInstanceOfType(temp, typeof(UposleniCistac));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DodajCistacInvalid()
        {
            EvidencijaUposlenih.DodajCistac("Tyr1", "");
            EvidencijaUposlenih.DodajCistac("", "123");
        }

        [TestMethod]
        public void DodajDoktor()
        {
            int idOrdinacije = EvidencijaOrdinacija.DodajOrdinaciju("radioloska");
            //Assert.AreEqual(0, idOrdinacije);
            Assert.AreEqual("radioloska", EvidencijaOrdinacija.Get(idOrdinacije).Naziv);

            int id = EvidencijaUposlenih.DodajDoktor("David", "Harbour", idOrdinacije);
            Uposleni temp = EvidencijaUposlenih.Get(id);
            Assert.AreEqual(1500, temp.Plata);
            Assert.IsInstanceOfType(temp, typeof(UposleniDoktor));
        }

        [TestMethod]
        public void DodajTech()
        {
            int id = EvidencijaUposlenih.DodajTech("Finn", "Wolfhard");
            Uposleni temp = EvidencijaUposlenih.Get(id);
            Assert.AreEqual(1400, temp.Plata);
            Assert.IsInstanceOfType(temp, typeof(UposleniTech));
        }

        [TestMethod]
        public void DodajTehnicar()
        {
            int id = EvidencijaUposlenih.DodajTehnicar("Millie Bobby", "Brown");
            Uposleni temp = EvidencijaUposlenih.Get(id);
            Assert.AreEqual(1200, temp.Plata);
            Assert.IsInstanceOfType(temp, typeof(UposleniTehnicar));
        }

        [TestMethod]
        public void DodajUprava()
        {
            int id = EvidencijaUposlenih.DodajUprava("Gaten", "Matarazzo");
            Uposleni temp = EvidencijaUposlenih.Get(id);
            Assert.AreEqual(2000, temp.Plata);
            Assert.IsInstanceOfType(temp, typeof(UposleniUprava));
        }

        [TestMethod]
        public void ObracunajPlate()
        {
            int idO = EvidencijaOrdinacija.DodajOrdinaciju("dummyOrdinacija");
            int idD = EvidencijaUposlenih.DodajDoktor("David", "Harbour", idO);
            int idP = EvidencijaPacijenata.AddPacijent("imenko", "prezimenko");
            // lets make him a regular
            EvidencijaPacijenata.DodajHitniPregled(idP, DateTime.Now, 10, "dummy pregled1", true, "-");
            EvidencijaPacijenata.DodajHitniPregled(idP, DateTime.Now, 10, "dummy pregled2", true, "-");
            EvidencijaPacijenata.DodajHitniPregled(idP, DateTime.Now, 10, "dummy pregled3", true, "-");
            EvidencijaPacijenata.DodajHitniPregled(idP, DateTime.Now, 10, "dummy pregled4", true, "-");

            EvidencijaUposlenih.ObracunajPlate(); // test je li sve uvezano kako treba (ima li nullreference)
        }
    }
}
