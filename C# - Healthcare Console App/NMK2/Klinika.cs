using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMK.Ordinacije;
using NMK.Uposlenici;
using NMK.Pacijenti;
using NMK2;
using System.Security.Cryptography;

namespace NMK
{


    public class Klinika
    {
        List<Pacijent> pacijenti;
        List<Pacijent> hitniSlucajevi;
        List<Karton> kartoni;
        List<Pregled> pregledi;
        List<Ordinacija> ordinacije;
        List<Doktor> doktori;
        List<Uposlenik> osoblje;

        public List<Karton> Kartoni { get => kartoni; set => kartoni = value; }
        public List<Pacijent> Pacijenti { get => pacijenti; set => pacijenti = value; }
        public List<Ordinacija> Ordinacije { get => ordinacije; set => ordinacije = value; }
        public List<Pregled> Pregledi { get => pregledi; set => pregledi = value; }
        public List<Doktor> Doktori { get => doktori; set => doktori = value; }
        public List<Uposlenik> Osoblje { get => osoblje; set => osoblje = value; }

        public Klinika()
        {
            pacijenti = new List<Pacijent>();
            hitniSlucajevi = new List<Pacijent>();
            kartoni = new List<Karton>();
            pregledi = new List<Pregled>();
            ordinacije = new List<Ordinacija>();
            doktori = new List<Doktor>();
            osoblje = new List<Uposlenik>();
            // potrebno je dodati nekoliko ordinacija i doktora zbog testiranja!

            Ordinacija o1_17983, o2_17983, o3_17983, o4_17983, o5_17983;
            o1_17983 = new OpstaMedicina(new List<Pacijent>(), new Aparat(true), "Opšta medicina", true, 1);
            o2_17983 = new Oftamologija(new List<Pacijent>(), new Aparat(true), "Oftamologija", true, 2);
            o3_17983 = new Ortopedija(new List<Pacijent>(), new Aparat(true), "Ortopedija", true, 3);
            o4_17983 = new Kardiologija(new List<Pacijent>(), new Aparat(true), "Kardiologija", true, 4);
            o5_17983 = new Dermatologija(new List<Pacijent>(), new Aparat(true), "Dermatologija", true, 5);

            Doktor d1_17983, d2_17983, d3_17983, d4_17983, d5_17983;
            d1_17983 = new Doktor(o1_17983);
            d2_17983 = new Doktor(o2_17983);
            d3_17983 = new Doktor(o3_17983);
            d4_17983 = new Doktor(o4_17983);
            d5_17983 = new Doktor(o5_17983);

            d1_17983.Ime = "Sinan"; d1_17983.Prezime = "Sakic"; d1_17983.KorisnickoIme = "ssakic1";
            d2_17983.Ime = "Edin"; d2_17983.Prezime = "Dzeko"; d2_17983.KorisnickoIme = "edzeko1";
            d3_17983.Ime = "Bruce"; d3_17983.Prezime = "Lee"; d3_17983.KorisnickoIme = "blee1";
            d4_17983.Ime = "Sebija"; d4_17983.Prezime = "Izetbegovic"; d4_17983.KorisnickoIme = "sizet1";
            d5_17983.Ime = "Zeljko"; d5_17983.Prezime = "Bebek"; d5_17983.KorisnickoIme = "zbebek1";
            MD5 hash = MD5.Create();
            d1_17983.Lozinka = GetMd5Hash(hash, "qwertz1");
            d2_17983.Lozinka = GetMd5Hash(hash, "uiop2");
            d3_17983.Lozinka = GetMd5Hash(hash, "asdf3");
            d4_17983.Lozinka = GetMd5Hash(hash, "ghjk4");
            d5_17983.Lozinka = GetMd5Hash(hash, "xcvb5");

            ordinacije.Add(o1_17983); ordinacije.Add(o2_17983); ordinacije.Add(o3_17983); ordinacije.Add(o4_17983); ordinacije.Add(o5_17983);
            doktori.Add(d1_17983); doktori.Add(d2_17983); doktori.Add(d3_17983); doktori.Add(d4_17983); doktori.Add(d5_17983);

            Uposlenik m1_17983 = new MedicinskiTehnicar();
            m1_17983.Ime = "Mustafa"; m1_17983.Prezime = "Sudzuka"; m1_17983.KorisnickoIme = "msudzuka1";
            Uposlenik m2_17983 = new MedicinskiTehnicar();
            m2_17983.Ime = "Razija"; m2_17983.Prezime = "Mujanovic"; m2_17983.KorisnickoIme = "rmujan1";
            m1_17983.Lozinka = GetMd5Hash(hash, "x3me1");
            m2_17983.Lozinka = GetMd5Hash(hash, "mnbv6");

            osoblje.Add(m1_17983); osoblje.Add(m2_17983);

        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }


        public List<string> IspisiMiPacijente()
        {
            List<string> povratni = new List<string>();
            foreach (Pacijent p in pacijenti)
            {
                povratni.Add(p.ToString() + "\n");
            }
            return povratni;
        }


        public string IspisiMiPreglede(string JMBG)
        {
            string povratni = "";
            foreach (Pregled p in pregledi)
            {
                if (p.Pacijent.Jmbg == JMBG) povratni += p.ToString() + "\r\n";
            }
            return povratni;
        }

        public void RegistrujPacijenta(Pacijent p)
        {
            p.DatumPrijema = DateTime.Now;
            pacijenti.Add(p);
        }

        public void RegistrujHitnogPacijenta(Pacijent p)
        {
            p.DatumPrijema = DateTime.Now;
            hitniSlucajevi.Add(p as HitniSlucaj);
        }

        public bool BrisiPacijenta(string JMBG)
        {
            int index = kartoni.FindIndex(k => k.Pacijent.Jmbg == JMBG);
            if (index != -1)
            {
                kartoni.RemoveAt(index);
                return true;
            }
            return false;
        }

        public string PrikaziRasporedPregledaPacijenta(string JMBG)
        {
            string povratna = "";

            int index = kartoni.FindIndex(x => x.JMBGPacijenta == JMBG);
            if (index != -1)
            {
                povratna = IspisiMiPreglede(JMBG);
            }
            else povratna = "r\nPacijent nema otvoren karton u klinici.";
            return povratna;
        }

        public void KreirajKarton(Karton k)
        {
            kartoni.Add(k);
        }

        public string PretraziKarton(string jmbg)
        {
            string povratni = "";
            int index = kartoni.FindIndex(x => x.JMBGPacijenta == jmbg);
            if (index == -1) povratni = "Navedeni pacijent nema svoj karton.\n";
            else
            {
                povratni += "Broj kartona: " + kartoni[index].BrojKartona;

                povratni += "\r\nSadašnja bolest: " + kartoni[index].SadasnjaBolest;
                povratni += "\r\nSadašnje alergije: " + kartoni[index].SadasnjeAlergije;
                povratni += "\r\nRanije bolesti: " + kartoni[index].RanijeBolesti;
                povratni += "\r\nRanije alergije: " + kartoni[index].RanijeAlergije;
                povratni += "\r\nPacijent je do sada " + kartoni[index].BrojacPosjeta.ToString() + " puta posjetio kliniku.";

            }
            return povratni;
        }

        public int ProvjeriKarton(string jmbg)
        {
            int indexKartona = kartoni.FindIndex(x => x.JMBGPacijenta == jmbg);
            if (indexKartona == -1) return -1;
            return indexKartona;
        }

        public void DodajPregled(string jmbg, string odjel)
        {

            Pregled p = new Pregled();
            int indexKartona = kartoni.FindIndex(x => x.JMBGPacijenta == jmbg);

            int indexPacijenta = pacijenti.FindIndex(x => x.Jmbg == jmbg);
            if (pacijenti[indexPacijenta] is NormalniSlucaj)
            {
                int indexOrdinacije = ordinacije.FindIndex(x => x.Odjel == odjel);
                p.Pacijent = new NormalniSlucaj(pacijenti[indexPacijenta] as NormalniSlucaj);
                p.Ordinacija = ordinacije[indexOrdinacije];
                int brojPacijenataIspred = ordinacije[indexOrdinacije].RedCekanja();
                ordinacije[indexOrdinacije].DodajURedCekanja(pacijenti[indexPacijenta]);
                pregledi.Add(p);
                pacijenti[indexPacijenta].ZakaziPregled(p, brojPacijenataIspred + 1);
                pacijenti[indexPacijenta].SortirajPreglede();
            }
            else
            {
                int indexOrdinacije = ordinacije.FindIndex(x => x.Odjel == odjel);
                p.Pacijent = new HitniSlucaj(pacijenti[indexPacijenta] as HitniSlucaj);
                p.Ordinacija = ordinacije[indexOrdinacije];
                int brojPacijenataIspred = ordinacije[indexOrdinacije].RedCekanja();
                ordinacije[indexOrdinacije].DodajURedCekanja(pacijenti[indexPacijenta]);
                pregledi.Add(p);
                pacijenti[indexPacijenta].ZakaziPregled(p, brojPacijenataIspred + 1);
                pacijenti[indexPacijenta].SortirajPreglede();
                kartoni[indexKartona].Pacijent.ZakaziPregled(p, brojPacijenataIspred + 1);
            }
        }

        public bool ProvjeriAparat(string odjel)
        {
            int indexOrdinacije = ordinacije.FindIndex(x => x.Odjel == odjel);
            if (indexOrdinacije != -1)
            {
                return ordinacije[indexOrdinacije].AparatFunkcionalan;
            }
            return false;
        }

        public void RegistrujNoviPregled(string jmbg, int indexOrdinacije, int brojOrdinacije, int indexKartona)
        {
            int indexPacijenta = Pacijenti.FindIndex(x => x.Jmbg == jmbg);
            int indexPregleda = Pregledi.FindIndex(x => x.Pacijent.Jmbg == jmbg && x.Ordinacija.Broj == brojOrdinacije);

            Pregledi[indexPregleda].VrijemePregleda = DateTime.Now;


            Dictionary<Pregled, int> d1 = new Dictionary<Pregled, int>();
            foreach (KeyValuePair<Pregled, int> k in Pacijenti[indexPacijenta].ZakazaniPregledi)
            {
                if (k.Key.Ordinacija.Broj != brojOrdinacije)
                {
                    d1.Add(k.Key, k.Value);
                }
            }
            Pacijenti[indexPacijenta].ZakazaniPregledi = d1;   // brisi podatke unutar pacijenata

            Dictionary<Pregled, int> d2 = new Dictionary<Pregled, int>();
            foreach (KeyValuePair<Pregled, int> k in Kartoni[indexKartona].Pacijent.ZakazaniPregledi)
            {
                if (k.Key.Ordinacija.Broj != brojOrdinacije)
                {
                    d2.Add(k.Key, k.Value);
                }
            }

            Kartoni[indexKartona].Pacijent.ZakazaniPregledi = d2;  // ovim izbacujemo aktivni pregled iz liste cekanja
            Ordinacije[indexOrdinacije].SkiniIzRedaCekanja();      // sljedeci pacijent dolazi na red
        }

        public void UvecajBrojPosjeta(string jmbg)
        {
            int index = kartoni.FindIndex(x => x.JMBGPacijenta == jmbg);
            kartoni[index].BrojacPosjeta++;
        }

        public double NaplatiPacijentu(string jmbg, int placanje)         // 0 - gotovina, 1 - rate, inace svaki pregled 50KM
        {
            int indexPacijenti = pacijenti.FindIndex(x => x.Jmbg == jmbg);
            int indexKartoni = kartoni.FindIndex(x => x.JMBGPacijenta == jmbg);


            int brojacPregleda = 0;     // koliko je pregleda obavljeno za vrijeme ove posjete klinici
            for (int i = 0; i < pregledi.Count; i++)
            {
                if (pregledi[i].Pacijent.Jmbg == jmbg && pregledi[i].VrijemePregleda >= pacijenti[indexPacijenti].DatumPrijema
                    && pregledi[i].VrijemePregleda <= DateTime.Now && pregledi[i].Placeno == false)
                {
                    pregledi[i].Placeno = true;    // ovim je naznaceno da je placeno
                    brojacPregleda++;              // tako da kasnije nece biti uracunato ponovo
                    break;
                }
            }

            double ukupno = brojacPregleda * 50;
            if (kartoni[indexKartoni].BrojacPosjeta > 3) // redovni pacijent
            {
                if (placanje == 1)
                {
                    ukupno -= ukupno * 10 / 100;
                }
                else
                {
                    ukupno /= 2;
                    kartoni[indexKartoni].Dugovi += ukupno;
                }
            }
            else
            {
                if (placanje == 2)
                {
                    ukupno += ukupno * 15 / 100;
                    ukupno /= 2;
                    kartoni[indexKartoni].Dugovi += ukupno;
                }
            }
            return ukupno;
        }


        // tri funkcije za poziv pri analizi sadrzaja
        public List<Doktor> NajvecePlate()
        {
            List<Doktor> doktoriSortirano = doktori.OrderByDescending(o => o.Plata).ToList();
            return doktoriSortirano;
        }

        public Pacijent NajvisePutaPosjetio()
        {
            if (kartoni.Count == 0) return null;
            List<Karton> kartoniSortirano = kartoni.OrderByDescending(o => o.BrojacPosjeta).ToList();
            Pacijent p = kartoniSortirano[0].Pacijent;
            return p;
        }

        public string Zauzetost()
        {
            string povratni = string.Empty;
            int brojac = 0;
            List<Ordinacija> ordinacijeSortirano = ordinacije.OrderByDescending(o => o.ListaCekanja.Count).ToList();
            bool neNula = false;

            foreach (Ordinacija O in ordinacijeSortirano)
            {
                if (O.ListaCekanja.Count != 0) neNula = true;
            }
            if (neNula == false) return "Sve ordinacije su prazne.";

            else
            {
                foreach (Ordinacija O in ordinacijeSortirano)
                {
                    brojac++;
                    povratni += brojac + ". odjel " + O.Odjel + ", redni broj " + O.Broj + "\r\nTrenutno u listi čekanja " + O.ListaCekanja.Count + " pacijenata.\r\n";
                }
            }

            return povratni;
        }

        public Pacijent DajPacijenta(string jmbg)      // poziva se kad pacijent otvori svoj modul - prikaz ličnih
                                                       // podataka na ekranu
        {
            int index = pacijenti.FindIndex(x => x.Jmbg == jmbg);
            if (index == -1) throw new Exception("Nije pronađen pacijent!");
            return pacijenti[index];
        }

        public Doktor DajDoktora(string korisnickoIme)
        {
            int index = doktori.FindIndex(x => x.KorisnickoIme == korisnickoIme);
            if (index == -1) return null;
            return doktori[index];
        }

        public Uposlenik DajOsoblje(string korisnickoIme)
        {
            int index = osoblje.FindIndex(x => x.KorisnickoIme == korisnickoIme);
            if (index == -1) throw new Exception("Nije pronađen uposlenik!");
            return osoblje[index];
        }

        public string PregledajKarton(string jmbg)      // omogućava pacijentu pregled vlastitog kartona
        {
            int index = kartoni.FindIndex(x => x.JMBGPacijenta == jmbg);
            if (index != -1) return PretraziKarton(jmbg);
            return "Pacijent nema svoj karton.";
        }

        public string DajSifruDoktora(string korisnickoIme)
        {
            int indexDoktora = doktori.FindIndex(x => x.KorisnickoIme == korisnickoIme);
            if (indexDoktora == -1) return null;
            return doktori[indexDoktora].Lozinka;
        }

        public string DajSifruUposlenika(string korisnickoIme)
        {
            int indexUposlenika = osoblje.FindIndex(x => x.KorisnickoIme == korisnickoIme);
            if (indexUposlenika == -1) return null;
            return osoblje[indexUposlenika].Lozinka;
        }

        public bool ProvjeriJMBGUBazi(string jmbg)
        {
            int indexPacijenta = pacijenti.FindIndex(x => x.Jmbg == jmbg);
            if (indexPacijenta == -1) return false;
            return true;
        }

        public bool ProvjeriDoktora(string korisnickoIme)
        {
            int indexDoktora = doktori.FindIndex(x => x.KorisnickoIme == korisnickoIme);
            if (indexDoktora == -1) return false;
            return true;
        }

        public bool ProvjeriOsoblje(string korisnickoIme)
        {
            int indexUposlenika = osoblje.FindIndex(x => x.KorisnickoIme == korisnickoIme);
            if (indexUposlenika == -1) return false;
            return true;
        }
    }
}