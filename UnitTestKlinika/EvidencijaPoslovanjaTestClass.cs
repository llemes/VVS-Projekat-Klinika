using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLLKlinika;

namespace UnitTestKlinika
{
    [TestClass]
    public class EvidencijaPoslovanjaTestClass
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NajveciIzvorPrihodaEmpty()
        {
            EvidencijaPoslovanja.NajveciIzvorPrihoda();
        }
        [TestMethod]
        public void NajveciIzvorPrihoda()
        {
            EvidencijaPacijenata.TestMode = true;

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

            int idJosJedan = EvidencijaPacijenata.AddPacijent("imenko", "prezimenko");
            EvidencijaPacijenata.DodajPodatkePacijenta(idJosJedan, new DateTime(2005, 4, 16), Spol.Zenski, "-", BracnoStanje.Nevjencan);

            EvidencijaPacijenata.DodajHitniPregled(idJosJedan, DateTime.Now, 5, "-", true, "-");
            EvidencijaPacijenata.PlacanjeGotovinaIspostaviRacun(idJosJedan);
            EvidencijaPacijenata.PlacanjeGotovinaIzvrsiPlacanje(idJosJedan);

            Assert.AreEqual("Sadie", EvidencijaPoslovanja.NajveciIzvorPrihoda().Ime);

            EvidencijaPacijenata.TestMode = false;
        }
    }
}
