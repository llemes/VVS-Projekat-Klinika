# VVS Projekat: Klinika
.NET biblioteka (dll) za upravljanje privatnom klinikom: zakazivanje pregleda, obračun cijene, zakazivanje sala itd. Sadrži i odgovarajuće unit testove, kako obične, tako i data-driven. U nastavku su opisane klase u sklopu projekta BLLKlinika koje su predviđene za korištenje sistema. Nisu opisane klase koje se koriste interno.

<!-- toc -->
- [Shared.cs](#Shared.cs)
    * [Spol](#Spol)
    * [BracnoStanje](#BracnoStanje)
    * [TipSistematskog](#TipSistematskog)
    * [MyConvert](#MyConvert)
- [DataValidator](#DataValidator)
    * [ValidateName](#ValidateName)
- [EvidencijaPacijenata](#EvidencijaPacijenata)
    * [Metode za anamnezu](#Metode za anamnezu)
    * [Metode za sistematski pregled](#Metode za sistematski pregled)
    * [Metode za hitni pregled](#Metode za hitni pregled)
    * [Metode za normalni pregled](#Metode za normalni pregled)
    * [Metode za plaćanje pregleda](#Metode za plaćanje pregleda)
    * [Pretrage](#Pretrage)
- [EvidencijaOrdinacija](#EvidencijaOrdinacija)
    * [Metode za evidenciju rada aparata](#Metode za evidenciju rada aparata)
    * [Metode za rad s pacijentima](#Metode za rad s pacijentima)
- [EvidencijaPoslovanja](#EvidencijaPoslovanja)
- [EvidencijaUposlenih](#EvidencijaUposlenih)
    * [Metode za dodavanje uposlenih](#Metode za dodavanje uposlenih)
<!-- tocstop -->

## Shared.cs

Sadrži enume koji se koriste kroz cijelo rješenje.

### Spol

```
enum Spol { Muski, Zenski }
```

### BracnoStanje

```
enum BracnoStanje { Nevjencan, Vjencan }
```

### TipSistematskog

```
enum TipSistematskog { Oftamolog, Neuropsihijatar, Psiholog, Opci }
```

### MyConvert

```
static TipSistematskog ToTipSistematskog(string s)
```
Služi za konvertovanje stringa u enum, koristi se pri deserijalizaciji iz XML.

## DataValidator

Klasa koja sadrži sve validacije koje se koriste u projektu.

### ValidateName

```
static void ValidateName(string ime, string prezime)
```
Regex testira na alfanumeričke karaktere. Baca exception tipa `ArgumentException`.

## EvidencijaPacijenata

```
static Pacijent GetPacijentById(int idPacijenta)
```
Vraća pacijenta koji ima odgovarajući ID. Baca exception tipa `ArgumentException`. 

```
static int AddPacijent(string ime, string prezime)
```
Vraća ID novokreiranog pacijenta. Vrši potrebnu validaciju prilikom kreiranja.

```
static void DodajPodatkePacijenta(int idPacijenta, DateTime datumRodjenja, Spol spol, string adresaStanovanja, BracnoStanje bracnoStanje)
```
Ažurira podatke o pacijentu na osnovu proslijeđenog ID.

### Metode za anamnezu

```
static void EvidentirajPorodicnoZdravstvenoStanje(int idPacijenta, string clanPorodice, string bolest)
```
Koristi se za anamnezu. Proslijeđeni član porodice i njegova bolest se dodaju u karton pacijenta sa odgovarajućim ID.

```
static void EvidentirajAlergiju(int idPacijenta, string nazivAlergije, bool aktivnaAlergija)
```
Koristi se za anamnezu. Proslijeđena alergija se dodaje u karton pacijenta.

```
static void EvidentirajBolest(int idPacijenta, string nazivBolesti, bool aktivnaBolest)
```
Koristi se za anamnezu. Proslijeđena bolest se dodaje u karton pacijenta.

### Metode za sistematski pregled

```
static int ZakaziSistematskiPregled(int idPacijenta)
```
Kreira novi sistematski pregled, dodaje ga u karton pacijenta, te ga dodaje kao stavku u fiskalni račun. Nakon kreiranja vraća ID pregleda.

```
static void ObaviStavkuSistematskog(int idPacijenta, int idPregledSistematskiEvidencija, DateTime vrijemePregleda, string rezultatPregleda, bool uspjesanPregled, TipSistematskog tipPregleda)
```
Prima ID pacijenta i ID pregleda. Evidentira obavljeni pregled koji je dio sistematskog, kao i informacije o tipu pregleda koji je obavljen. Pored ovoga, povećava broj posjeta koje je pacijent obavio u klinici. Ovo se kasnije koristi u obračunu cijene.

```
static List<string> PotrebniPreglediSistematski(int idPacijenta, int idPregledSistematskiEvidencija)
```
Vraća listu pregleda koji još nisu obavljeni u sklopu sistematskog, a koji se moraju obaviti da bi se cijena obračunala i sistematski evidentirao u karton.

### Metode za hitni pregled

```
static void DodajHitniPregled(int idPacijenta, DateTime vrijemePregleda, decimal cijena, string naziv, bool prvaPomocUradjena, string prvaPomocRazlog)
```
Kreira novi hitni pregled, dodaje ga u karton pacijenta sa odgovarajućim ID, dodaje kao stavku na fiskalni račun te povećava broj posjeta klinici za pacijenta.

```
static void DodajHitniPregledSmrt(int idPacijenta, DateTime vrijemePregleda, decimal cijena, string naziv, bool prvaPomocUradjena, string prvaPomocRazlog, DateTime vrijemeSmrti, string uzrokSmrti, DateTime terminObdukcije)
```
Evidentiranje hitnog pregleda u slučaju da je pacijent preminuo u toku pregleda.

### Metode za normalni pregled
```
static void DodajNormalniPregled(int idPacijenta)
```
Dodaje pregled u karton pacijenta, kao stavku na fiskalnom računu i povećava broj posjeta klinici. Mora se pozvati nakon što je builder završio sa konstrukcijom objekta; odnosno, nakon što su se pozvale sve metode ispod. Ukoliko objekat nije kreiran kako treba, Builder će baciti exception `InvalidOperationException`.

```
static void KreirajNormalniPregled(int idPacijenta)
```
Prvi korak buildera: kreira se pregled i dodjeljuje pacijentu.

```
static void DodajLijekUTerapiju(int idPacijenta, string nazivLijeka, double kolicina, string mjera, int frekvencijaUzimanja, string mjeraFrekvencijeUzimanja)
```
Drugi korak buildera: dodaju se lijekovi u terapiju pregleda.

```
static void DodajKratkorocnuTerapiju(int idPacijenta, DateTime datumPotpisivanja, DateTime krajTerapije)

static void DodajDugorocnuTerapiju(int idPacijenta, DateTime datumPotpisivanja)
```
Treći korak buildera: dodaje se terapija u pregled.

```
static void DodajDetaljeNormalnogPregleda(int idPacijenta, DateTime vrijemePregleda, decimal cijenaPregleda, string nazivPregleda, string misljenjeDoktora, string nazivBolesti)
```
Četvrti korak buildera: kreira se pregled koji sadrži prethodno kreirane informacije.

### Metode za plaćanje pregleda

#### Plaćanje na rate
```
static string PlacanjeRateIspostaviRacun(int idPacijent)
```
Daje odrezak fiskalnog računa koji sadrži sve neplaćene stavke. Mora se izdati fiskalni račun prije pozivanja metode za plaćanje. Obračun cijene se izvršava prilikom izdavanja fiskalnog računa.

```
static void PlacanjeRateIzvrsiPlacanje(int idPacijent)
```
Plaća se jedna rata. Vodi se evidencija o transakciji.

#### Plaćanje gotovinom
```
static string PlacanjeGotovinaIspostaviRacun(int idPacijenta)
```
Daje odrezak fiskalnog računa koji sadrži sve neplaćene stavke. Mora se izdati fiskalni račun prije pozivanjametode za plaćanje. Obračun cijene se izvršava prilikom izdavanja fiskalnog računa.

```
static void PlacanjeGotovinaIzvrsiPlacanje(int idPacijenta)
```
Plaća se puni iznos. Vodi se evidencija o transakciji.

### Pretrage
```
static int GetPacijentId(string key)
```
Vraća ID pacijenta čije ime ili prezime sadrži string proslijeđen kao parametar. Pretraga je case sensitive.

## EvidencijaOrdinacija
Klasa za rad sa ordinacijama.

```
static int DodajOrdinaciju(string naziv)
```
Dodaje se nova ordinacija u sistem.

```
static Aparat Get(int idAparata, int idOrdinacije)
```
Vraća aparat iz ordinacije. Baca exception tipa `ArgumentException`.

```
static Ordinacija GetOrdinacijaById(int idOrdinacije)
```
Vraća ordinaciju sa odgovarajućim ID. Baca exception tipa `ArgumentException`.

### Metode za evidenciju rada aparata
```
static int DodajAparat(int idOrdinacije, string nazivAparata)
```
Dodaje novi aparat u sistem.

```
static void EvidentirajRadAparata(int idOrdinacije, int idAparata, DateTime vrijemeUkljucivanja, DateTime vrijemeIskljucivanja)
```
Evidentira vrijeme kada je aparat bio aktivan. Baca exception tipa `ArgumentException`.

### Metode za rad s pacijentima
```
static int GetIdNajslobodnijeOrdinacije(string tipOrdinacije)
```
Vraća ID ordinacije sa najslobodnijim rasporedom za određeni tip ordinacije. Baca exception tipa `ArgumentNullException` ili `ArgumentException`.

```
static int DodajPacijenta(int idOrdinacije, int idPacijenta)
```
Dodaje pacijenta u red čekanja ordinacije. Ovo ima uticaja na metodu iznad.

```
static void OpsluziPacijenta(int idOrdinacije)
```
Uklanja pacijenta koji je na redu iz reda čekanja, evidentira pacijenta u historiju opsluženih pacijenata ordinacije. Baca exception tipa `ArgumentException`.

## EvidencijaPoslovanja
Klasa za praćenje poslovanja klinike.

```
static void EvidentirajTransakciju(DateTime datumTransakcije, Pacijent platio, decimal iznos)
```
Evidentira se transakcija (obično plaćanje pregleda od strane pacijenta).

```
static decimal IznosTransakcijaUProteklomMjesecu(Pacijent p)
```
Izvještaj o ukupnom iznosu transakcija u proteklom mjesecu.

```
static Pacijent NajveciIzvorPrihoda()
```
Izvještaj o pacijentu koji čini najveći izvor prihoda ove klinike. Baca exception tipa `InvalidOperationException`.

## EvidencijaUposlenih
Klasa za rad sa uposlenicima klinike.

```
static Uposleni GetUposleniById(int idUposlenog)
```
Vraća uposlenika sa odgovarajućim ID. Baca exception tipa `ArgumentException`.

```
static void UpdatePassword(int idUposlenog, string password)
```
Ažurira password uposlenika sa odgovarajućim ID.

```
static void ObracunajPlate()
```
Obračunava plate uposlenih. Ovo je bitno pozvati jer interno obračunava bonus za sve doktore, na koji utiču faktori poput broja usluženih pacijenata ili broja pacijenata koji su se vratili u kliniku nakon pozitivnog iskustva.

### Metode za dodavanje uposlenih
U nastavku se nalaze prototipi metoda za dodavanje uposlenih. Username se generiše automatski unutar sistema.

```
static int DodajCistac(string ime, string prezime)
static int DodajDoktor(string ime, string prezime, int idOrdinacije)
static int DodajTech(string ime, string prezime)
static int DodajTehnicar(string ime, string prezime)
static int DodajUprava(string ime, string prezime)
```

Tipovi uposlenih su, redoslijedom:
* čistač
* doktor
* IT podrška
* medicinski tehničar
* uprava