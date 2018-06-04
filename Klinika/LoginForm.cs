using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLLKlinika;

namespace Klinika
{
    public partial class LoginForm : Form
    {
        private static int id1, id2, id3;
        private static int ids1, ids2, ids3;
        
        public static void Populate()
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

            EvidencijaPacijenata.Get(id2);
            EvidencijaPacijenata.Get(id3);

            EvidencijaPacijenata.EvidentirajPorodicnoZdravstvenoStanje(id3, "otac", "šizofrenija"); // idkkkkk
            EvidencijaPacijenata.EvidentirajAlergiju(id2, "kikiriki", true);
            EvidencijaPacijenata.EvidentirajBolest(id1, "bolest", false);

            int idPregleda = EvidencijaPacijenata.ZakaziSistematskiPregled(id1);
            List<string> potrebniPregledi = EvidencijaPacijenata.PotrebniPreglediSistematski(id1, idPregleda);

            EvidencijaPacijenata.DodajHitniPregled(id1, DateTime.Now, 10, "defibrilacija", true, "-");

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

            idSistematskog = EvidencijaPacijenata.ZakaziSistematskiPregled(id);
            EvidencijaPacijenata.ObaviStavkuSistematskog(id, idSistematskog, DateTime.Now, "ok", true, TipSistematskog.Opci);
            EvidencijaPacijenata.ObaviStavkuSistematskog(id, idSistematskog, DateTime.Now, "ok", true, TipSistematskog.Neuropsihijatar);
            EvidencijaPacijenata.ObaviStavkuSistematskog(id, idSistematskog, DateTime.Now, "ok", true, TipSistematskog.Psiholog);
            EvidencijaPacijenata.ObaviStavkuSistematskog(id, idSistematskog, DateTime.Now, "ok", true, TipSistematskog.Oftamolog);

            EvidencijaPacijenata.PlacanjeGotovinaIspostaviRacun(id);

            EvidencijaPacijenata.PlacanjeGotovinaIzvrsiPlacanje(id);

            EvidencijaPacijenata.DodajHitniPregled(id1, DateTime.Now, 10, "defibrilacija", true, "-");
            EvidencijaPacijenata.DodajHitniPregled(id1, DateTime.Now, 10, "defibrilacija", true, "-");
            EvidencijaPacijenata.DodajHitniPregled(id1, DateTime.Now, 10, "defibrilacija", true, "-");
            EvidencijaPacijenata.PlacanjeRateIspostaviRacun(id1);
            EvidencijaPacijenata.PlacanjeRateIzvrsiPlacanje(id1);
            EvidencijaPacijenata.PlacanjeRateIzvrsiPlacanje(id1);
            EvidencijaPacijenata.PlacanjeRateIzvrsiPlacanje(id1);
            EvidencijaPacijenata.PlacanjeRateIzvrsiPlacanje(id1);
            EvidencijaPacijenata.PlacanjeRateIzvrsiPlacanje(id1);
            EvidencijaPacijenata.PlacanjeRateIzvrsiPlacanje(id1);

            EvidencijaPacijenata.PlacanjeRateIspostaviRacun(id1);

            EvidencijaPacijenata.DodajHitniPregled(id2, DateTime.Now, 10, "reanimacija", true, "-");
            EvidencijaPacijenata.DodajHitniPregled(id2, DateTime.Now, 10, "reanimacija", true, "-");
            EvidencijaPacijenata.DodajHitniPregled(id2, DateTime.Now, 10, "reanimacija", true, "-");
            
        }

        public LoginForm()
        {
            InitializeComponent();

            EvidencijaOrdinacija.DodajOrdinaciju("radiologija");
            EvidencijaOrdinacija.DodajOrdinaciju("kardiologija");
            EvidencijaOrdinacija.DodajOrdinaciju("hirurgija");
            EvidencijaOrdinacija.DodajOrdinaciju("ortopedija");
            EvidencijaOrdinacija.DodajOrdinaciju("neurologija");
            EvidencijaOrdinacija.DodajOrdinaciju("opća");


            Populate();
        }
        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Text;


        }
    }
}
