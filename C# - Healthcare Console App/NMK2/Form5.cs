using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NMK;
using NMK.Pacijenti;

namespace NMK2
{

    public partial class Form5 : Form
    {
        public static Form1 babo;
        public bool hitni = false;
        public Pacijent p;
        public Pacijent pacijentDuznik;
        public bool vecNasao = false;
        public bool modRegistracija = true;
        bool[] validirao = { false, false, false, false, false, false, false };   // 0 - Ime,  1 - Prezime
                                                                                  // 2 - Datum <> Jmbg, 3 Adresa
                                                                                  // 4 5 i 6 radioButtoni
        bool samoPregledi = false;      // ulazi u formu sa anamnezom -> ne smije mijenjati karton
        bool zavrsioPrviHitni = false;
        bool zavrsioDrugiHitni = false;

        void Ocisti()
        {
            radioButton1.Checked = radioButton2.Checked = radioButton9.Checked = radioButton10.Checked = false;
            radioButton3.Checked = radioButton4.Checked = false;
            textBox1.Text = textBox2.Text = textBox3.Text = textBox5.Text = "";
            userControl11.NulirajSliku();
            toolStripStatusLabel1.Text = "";
            hitni = false; vecNasao = false; modRegistracija = true;
            for (int i = 0; i < 7; i++) validirao[i] = false;
            UserControl1.validiranaSlika = true;
            zavrsioDrugiHitni = zavrsioPrviHitni = false;
        }

        void OcistiKartone()
        {
            p = null;
            textBox6.Text = textBox7.Text = textBox8.Text = textBox9.Text = textBox10.Text = "";
            for(int i=0; i<checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);       // sve postavimo na nečekirano
            }
        }

        void OcistiHitnogPrvi()
        {
            textBox11.Text = textBox12.Text = "";
            radioButton5.Checked = radioButton6.Checked = false;
        }
        
        void OcistiHitnogDrugi()
        {
            textBox13.Text = "";
            radioButton7.Checked = radioButton8.Checked = false;
            dateTimePicker3.Value = DateTime.Now;
        }

        public bool ImalBroja(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] >= '0' && s[i] <= '9') return true;
            }
            return false;
        }

        public bool ProvjeriJmbg(string jmbg, DateTime datRodjenja)     
        {
            if (jmbg.Length == 0) return false;
            int danJmbg = 0, mjesecJmbg = 0, godinaJmbg = 0, dan = datRodjenja.Day, mjesec = datRodjenja.Month, godina = datRodjenja.Year;
            string s1 = string.Empty, s2 = string.Empty, s3 = string.Empty;

            for (int i = 0; i < 7; i++)
            {
                if (Char.IsDigit(jmbg[i]))
                {
                    if (i < 2) s1 += jmbg[i];
                    else if (i < 4) s2 += jmbg[i];
                    else s3 += jmbg[i];
                }
            }

            danJmbg = Int32.Parse(s1); mjesecJmbg = Int32.Parse(s2); godinaJmbg = Int32.Parse(s3);
            if (godinaJmbg % 100 > 0) godinaJmbg += 1000;
            else godinaJmbg += 2000;

            if (dan != danJmbg || mjesec != mjesecJmbg || godina != godinaJmbg) return false;
            return true;
        }

        public Form5()
        {
            InitializeComponent();
            p = null;
            pacijentDuznik = null;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MaxDate = DateTime.Now;
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Doviđenja!");
            babo.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (modRegistracija == false) MessageBox.Show("Opcija nije moguća jer ste u modu brisanja pacijenta!");
            else if (vecNasao == true)
            {
                MessageBox.Show("Nije potrebna anamneza. Pacijent već ima otvoren karton!");
            }
            else if (p != null)
            {
                groupBox1.Visible = false;
            }
            else MessageBox.Show("Molimo popunite podatke o pacijentu!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OcistiKartone();
            groupBox1.Visible = true;
            errorProvider1.SetError(textBox1, String.Empty);
            errorProvider5.SetError(groupBox8, String.Empty);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                hitni = true;
                MaximumSize = new Size(462, 570);
                MinimumSize = new Size(462, 570);
                groupBox2.Visible = true;
                groupBox5.Visible = true;
            }
            else if(tabControl1.SelectedIndex == 0)
            {
                MaximumSize = new Size(850, 585);
                MinimumSize = new Size(850, 585);
            }
            else
            {
                MaximumSize = new Size(425, 400);
                MinimumSize = new Size(425, 400);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton7.Checked == true || radioButton8.Checked == true)
            {
                groupBox2.Visible = true;
                zavrsioDrugiHitni = true;
                tabControl1.SelectedIndex = 0;
                errorProvider12.SetError(radioButton7, string.Empty);
            }
            else errorProvider12.SetError(radioButton7, "Odaberite opciju!");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (hitni && (!zavrsioPrviHitni || !zavrsioDrugiHitni)) MessageBox.Show("Prvo korektno ispunite protokol hitnog pacijenta!");
            else
            {
                if (radioButton3.Checked == true || radioButton4.Checked == true && modRegistracija)
                {
                    errorProvider7.SetError(groupBox6, String.Empty);
                    validirao[6] = true;
                    if (UserControl1.validiranaSlika && validirao[0] && validirao[1] && validirao[2] && validirao[3] && validirao[4] && validirao[5])
                    {
                        if (modRegistracija == true)
                        {
                            int index = Form1.NMK.ProvjeriKarton(textBox5.Text);
                            if (index != -1)
                            {
                                Form1.NMK.UvecajBrojPosjeta(textBox5.Text);
                                p = new NormalniSlucaj(); // da bi se izvrsio button click za anamnezu
                                vecNasao = true;
                                samoPregledi = true;
                            }

                            if (hitni == false && vecNasao == false)
                            {
                                p = new NormalniSlucaj();
                                p.Ime = textBox1.Text;
                                p.Prezime = textBox2.Text;
                                p.DatumRodjenja = dateTimePicker1.Value;
                                p.Jmbg = textBox5.Text;
                                if (radioButton1.Checked == true) p.SpolPacijenta = Pacijent.Spol.musko;
                                if (radioButton2.Checked == true) p.SpolPacijenta = Pacijent.Spol.zensko;
                                p.Adresa = textBox3.Text;
                                if (radioButton3.Checked == true) p.UBraku = true;
                                if (radioButton4.Checked == true) p.UBraku = false;
                                if (userControl11.DajSliku() != null) p.SlikaPacijenta = userControl11.DajSliku();
                                else p.SlikaPacijenta = Properties.Resources.maxresdefault;
                                Form1.NMK.RegistrujPacijenta(p);
                                MessageBox.Show("Pacijent uspješno registrovan!");
                                Ocisti();
                            }
                            else if(hitni == true && vecNasao == false)
                            {
                                HitniSlucaj h = new HitniSlucaj();
                                
                                h.Ime = textBox1.Text;
                                h.Prezime = textBox2.Text;
                                h.DatumRodjenja = dateTimePicker1.Value;
                                h.Jmbg = textBox5.Text;
                                if (radioButton1.Checked == true) h.SpolPacijenta = Pacijent.Spol.musko;
                                if (radioButton2.Checked == true) h.SpolPacijenta = Pacijent.Spol.zensko;
                                h.Adresa = textBox3.Text;
                                if (radioButton3.Checked == true) h.UBraku = true;
                                if (radioButton4.Checked == true) h.UBraku = false;
                                if (userControl11.DajSliku() != null) h.SlikaPacijenta = userControl11.DajSliku();
                                else h.SlikaPacijenta = Properties.Resources.maxresdefault;
                                h.RazlogPomoci = textBox11.Text;
                                h.PrvaPomoc = textBox12.Text;
                                if (radioButton5.Checked == true) h.Ziv = true;
                                else h.Ziv = false;
                                h.VrijemeSmrti = dateTimePicker2.Value;
                                h.UzrokSmrti = textBox13.Text;
                                if (radioButton7.Checked == true)
                                {
                                    h.Obdukcija = true;
                                    h.VrijemeObdukcije = dateTimePicker3.Value;
                                }
                                else h.Obdukcija = false;
                                MessageBox.Show("Hitni pacijent uspješno registrovan!");
                                OcistiHitnogPrvi(); OcistiHitnogDrugi(); Ocisti();
                            }
                        }
                    }
                    else toolStripStatusLabel1.Text = "Ispravite neispravno unesena polja!";
                }
                else if (modRegistracija == false)
                {
                    if (Form1.NMK.BrisiPacijenta(textBox5.Text) == true)
                    {
                        MessageBox.Show("Karton pacijenta uspješno obrisan!");
                        Ocisti();
                    }
                    else
                    {
                        MessageBox.Show("Pacijent se ne nalazi u bazi podataka!");
                        Ocisti();
                    }
                }
                else
                {
                    errorProvider7.SetError(groupBox6, "Odaberite opciju!");
                }
            }
      
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Karton k = new Karton();
            k.Pacijent = p;
            k.SadasnjaBolest = textBox6.Text;
            k.SadasnjeAlergije = textBox7.Text;
            k.RanijeBolesti = textBox8.Text;
            k.RanijeAlergije = textBox9.Text;
            k.ZdravstvenoStanjePorodice = textBox10.Text;
            k.BrojKartona = Form1.globalBrojac;
            Form1.NMK.KreirajKarton(k);
            MessageBox.Show("Uspješno kreiran karton!");
            Form1.NMK.UvecajBrojPosjeta(p.Jmbg);
            Form1.globalBrojac += 1;
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                Form1.NMK.DodajPregled(p.Jmbg, itemChecked.ToString());
            }
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton9.Checked == true)
            {
                errorProvider5.SetError(groupBox8, string.Empty);
                validirao[4] = true;
                modRegistracija = true;
                MessageBox.Show("Popunite odgovarajuća polja za registraciju!");
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox5.ReadOnly = false;
                textBox3.ReadOnly = false;
            }
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton10.Checked == true)
            {
                errorProvider5.SetError(groupBox8, string.Empty);
                validirao[4] = true;
                modRegistracija = false;
                MessageBox.Show("Popunite JMBG polje pacijenta kojeg želite obrisati!");
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox5.ReadOnly = false;
                textBox3.ReadOnly = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string s = Form1.NMK.Zauzetost();
            textBox4.Text = s;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (modRegistracija)
            {
                if (radioButton9.Checked == false && radioButton10.Checked == false) errorProvider5.SetError(groupBox8, "Odaberite opciju!");
                if (textBox1.Text.Length == 0) errorProvider1.SetError(textBox1, "Prazno polje!");
                else if (ImalBroja(textBox1.Text)) errorProvider1.SetError(textBox1, "Ime sadrži cifru!");
                else
                {
                    validirao[0] = true;
                    errorProvider1.SetError(textBox1, String.Empty);
                }
            }
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            if (modRegistracija)
            {
                if (textBox2.Text.Length == 0) errorProvider2.SetError(textBox2, "Prazno polje!");
                else if (ImalBroja(textBox2.Text)) errorProvider2.SetError(textBox2, "Ime sadrži cifru!");
                else
                {
                    validirao[1] = true;
                    errorProvider2.SetError(textBox2, String.Empty);
                }
            }
        }

        private void textBox5_Validating(object sender, CancelEventArgs e)
        {
            if (textBox5.Text.Length == 0 && modRegistracija) errorProvider3.SetError(textBox5, "Prazno polje!");
            else if (textBox5.Text.Length != 13 && modRegistracija) errorProvider3.SetError(textBox5, "JMBG mora imati 13 cifara!");
            else if (ProvjeriJmbg(textBox5.Text, dateTimePicker1.Value) == false && modRegistracija) errorProvider3.SetError(textBox5, "JMBG i datum rođenja se ne slažu!");
            else
            {
                validirao[2] = true;
                errorProvider3.SetError(textBox5, String.Empty);
            }
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            if (radioButton1.Checked == false && radioButton2.Checked == false) errorProvider6.SetError(groupBox7, "Odaberite opciju!");
            if (textBox3.Text.Length == 0) errorProvider4.SetError(textBox3, "Prazno polje!");
            else
            {
                validirao[3] = true;
                errorProvider4.SetError(textBox3, String.Empty);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            validirao[5] = true;
            errorProvider6.SetError(groupBox7, String.Empty);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            validirao[5] = true;
            errorProvider6.SetError(groupBox7, String.Empty);
        }

        private void dateTimePicker1_Validating(object sender, CancelEventArgs e)
        {
            if (ProvjeriJmbg(textBox5.Text, dateTimePicker1.Value) == true)
            {
                validirao[2] = true;
                errorProvider3.SetError(textBox5, String.Empty);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox14.Text.Length == 0)
            {
                errorProvider9.SetError(textBox14, "Prazno polje!");
                toolStripStatusLabel2.Text = "Molimo unesite ispravno!";
            }
            else if (textBox14.Text.Length != 13)
            {
                errorProvider9.SetError(textBox14, "JMBG mora imati tačno 13 cifara!");
                toolStripStatusLabel2.Text = "Molimo unesite ispravno!";
            }
            else if (Form1.NMK.ProvjeriJMBGUBazi(textBox14.Text) == false)
            {
                errorProvider9.SetError(textBox14, "Ne postoji nijedan registrovan pacijent sa tim JMBG!");
                toolStripStatusLabel2.Text = "Molimo unesite ispravno!";
            }
            else if (radioButton11.Checked == false && radioButton12.Checked == false)
            {
                toolStripStatusLabel2.Text = "Molimo unesite ispravno!";
                errorProvider10.SetError(radioButton11, "Odaberite opciju!");
            }
            else
            {
                errorProvider9.SetError(textBox14, String.Empty);
                errorProvider10.SetError(radioButton11, String.Empty);
                int provjereno = Form1.NMK.ProvjeriKarton(textBox14.Text);
                if (provjereno == -1) toolStripStatusLabel2.Text = "Pacijent nema otvoren karton!";
                else
                {
                    int nacinPlacanja = -38;
                    if (radioButton11.Checked == true) nacinPlacanja = 1;
                    else nacinPlacanja = 2;
                    textBox15.Text = Form1.NMK.NaplatiPacijentu(textBox14.Text, nacinPlacanja).ToString() + " KM.";
                    textBox16.Text = Form1.NMK.Kartoni[provjereno].Dugovi.ToString() + " KM.";
                }
            }
        }

        private void textBox14_Validating(object sender, CancelEventArgs e)
        {
            if (textBox14.Text.Length == 0)
            {
                errorProvider9.SetError(textBox14, "Prazno polje!");
                toolStripStatusLabel2.Text = "Molimo unesite ispravno!";
            }
            else if (textBox14.Text.Length != 13)
            {
                errorProvider9.SetError(textBox14, "JMBG mora imati tačno 13 cifara!");
                toolStripStatusLabel2.Text = "Molimo unesite ispravno!";
            }
            else if (Form1.NMK.ProvjeriJMBGUBazi(textBox14.Text) == false)
            {
                errorProvider9.SetError(textBox14, "Ne postoji nijedan registrovan pacijent sa tim JMBG!");
                toolStripStatusLabel2.Text = "Molimo unesite ispravno!";
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (radioButton5.Checked == false && radioButton6.Checked == false) errorProvider11.SetError(radioButton5, "Odaberite opciju!");
            else
            {
                errorProvider11.SetError(radioButton5, String.Empty);
                groupBox2.Visible = false;
                zavrsioPrviHitni = true;
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            errorProvider12.SetError(radioButton7, string.Empty);
            label19.Visible = true;
            dateTimePicker3.Visible = true;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            errorProvider12.SetError(radioButton7, string.Empty);
        }
    }
}
