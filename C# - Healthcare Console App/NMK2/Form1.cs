using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using NMK;

namespace NMK2
{
    public partial class Form1 : Form
    {
        public static Klinika NMK;
        public static string uneseniJMBG;
        public static int globalBrojac = 1;
        bool[] validirao = { false, false, false, false };   // 0 - Korisničko ime, 1 - lozinka, 2 - potvrda, 3 - JMBG
        public static string unesenoKorisnickoIme;

        public void Ocisti()
        {
            textBox1.Text = textBox2.Text = textBox3.Text = "";
            comboBox2.Text = maskedTextBox1.Text = "";
            validirao[0] = validirao[1] = validirao[2] = validirao[3] = false;
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

        public Form1()
        {
            InitializeComponent();
            NMK = new Klinika();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush plavi = new SolidBrush(Color.Gold);
            SolidBrush bijeli = new SolidBrush(Color.GhostWhite);
            Pen olovka = new Pen(Color.Black);
            //g.DrawEllipse(olovka, 180, 5, 152, 152);
            //g.DrawRectangle(olovka, 192, 70, 127, 27);
            g.FillEllipse(plavi, 180, 11, 152, 152);
            g.FillRectangle(bijeli, 192, 74, 127, 27);
            //g.DrawRectangle(olovka, 243, 15, 27, 130);
            g.FillRectangle(bijeli, 243, 21, 27, 130);

            g.DrawString("N      M      K", new Font("Arial", 16), new SolidBrush(Color.Red), 192, 74);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text.Length == 0)
            {
                toolStripStatusLabel1.Text = "Molimo odaberite ulogu!";
                errorProvider5.SetError(comboBox2, "Prazno polje!");
            }
            else
            {
                toolStripStatusLabel1.Text = "";
                errorProvider5.SetError(comboBox2, String.Empty);
            }

            if (comboBox2.Text == "Doktor")
            {
                if (validirao[0] && validirao[1] && validirao[2])
                {
                    unesenoKorisnickoIme = textBox1.Text;
                    var p = NMK.DajDoktora(textBox1.Text);
                    Ocisti();
                    MessageBox.Show("Dobro došli, " + p.Ime + "!");
                    Form f2 = new Form2();
                    Form2.babo = this;
                    f2.Show();
                    Visible = false;
                }
            }
            else if (comboBox2.Text == "Osoblje")
            {
                if (validirao[0] && validirao[1] && validirao[2])
                {
                    var p = NMK.DajOsoblje(textBox1.Text);
                    Ocisti();
                    Form f5 = new Form5();
                    MessageBox.Show("Dobro došli, " + p.Ime + "!");
                    f5.Show();
                    Form5.babo = this;
                    Visible = false;
                }
            }
            else if (comboBox2.Text == "Pacijent")
            {
                if (validirao[3])
                {
                    uneseniJMBG = maskedTextBox1.Text;
                    var p = NMK.DajPacijenta(uneseniJMBG);
                    Ocisti();
                    MessageBox.Show("Dobro došli, " + p.Ime + "!");
                    Form f4 = new Form4();
                    f4.Show();
                    Form4.babo = this;
                    Visible = false;
                }
            }
            else toolStripStatusLabel1.Text = "Molimo unesite ispravne podatke!";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0 || comboBox2.SelectedIndex == 1)
            {
                textBox1.Text = textBox2.Text = textBox3.Text = maskedTextBox1.Text = "";
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                maskedTextBox1.ReadOnly = true;
            }
            else
            {
                textBox1.Text = textBox2.Text = textBox3.Text = maskedTextBox1.Text = "";
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                maskedTextBox1.ReadOnly = false;
            }
            errorProvider1.SetError(textBox1, String.Empty);
            errorProvider2.SetError(textBox2, String.Empty);
            errorProvider3.SetError(textBox3, String.Empty);
            errorProvider4.SetError(maskedTextBox1, String.Empty);
            errorProvider5.SetError(comboBox2, String.Empty);
            toolStripStatusLabel1.Text = "";
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (comboBox2.Text.Length != 0)
            {
                if (comboBox2.Text.Length == 0)
                {
                    toolStripStatusLabel1.Text = "Molimo odaberite ulogu!";
                    errorProvider5.SetError(comboBox2, "Prazno polje!");
                }
                else
                {
                    toolStripStatusLabel1.Text = "";
                    errorProvider5.SetError(comboBox2, String.Empty);
                }
                if (textBox1.Text.Length == 0)
                {
                    errorProvider1.SetError(textBox1, "Prazno polje!");
                    toolStripStatusLabel1.Text = "Molimo unesite ponovo!";
                    validirao[0] = false;
                }
                else
                {
                    if (comboBox2.Text == "Doktor")
                    {
                        if (NMK.ProvjeriDoktora(textBox1.Text) == false)
                        {
                            validirao[0] = false;
                            errorProvider1.SetError(textBox1, "Nijedan doktor nema navedeno korisničko ime!");
                            toolStripStatusLabel1.Text = "Molimo unesite ponovo!";
                        }
                        else
                        {
                            validirao[0] = true;
                            errorProvider1.SetError(textBox1, String.Empty);
                            toolStripStatusLabel1.Text = "";
                        }
                    }
                    if (comboBox2.Text == "Osoblje")
                    {
                        if (NMK.ProvjeriOsoblje(textBox1.Text) == false)
                        {
                            validirao[0] = false;
                            errorProvider1.SetError(textBox1, "Nijedan uposlenik nema navedeno korisničko ime!");
                            toolStripStatusLabel1.Text = "Molimo unesite ponovo!";
                        }
                        else
                        {
                            validirao[0] = true;
                            errorProvider1.SetError(textBox1, String.Empty);
                            toolStripStatusLabel1.Text = "";
                        }
                    }
                }
            }
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            MD5 hash = MD5.Create();
            string pokusaj = GetMd5Hash(hash, textBox2.Text);

            if (textBox2.Text.Length == 0)
            {
                validirao[1] = false;
                errorProvider2.SetError(textBox2, "Prazno polje!");
                toolStripStatusLabel1.Text = "Molimo unesite ponovo lozinku!";
            }

            else
            {
                if (comboBox2.Text == "Doktor")
                {
                    string povratni = NMK.DajSifruDoktora(textBox1.Text);
                    if (povratni != pokusaj)
                    {
                        validirao[1] = false;
                        errorProvider2.SetError(textBox2, "Doktoru ne odgovara unesena lozinka!");
                        toolStripStatusLabel1.Text = "Molimo unesite ponovo lozinku!";
                    }
                    else
                    {
                        toolStripStatusLabel1.Text = "";
                        errorProvider2.SetError(textBox2, String.Empty);
                        validirao[1] = true;
                    }
                }
                if (comboBox2.Text == "Osoblje")
                {
                    string povratni = NMK.DajSifruUposlenika(textBox1.Text);
                    if (povratni != pokusaj)
                    {
                        toolStripStatusLabel1.Text = "Molimo unesite ponovo lozinku!";
                        errorProvider2.SetError(textBox2, "Uposleniku ne odgovara unesena lozinka!");
                        validirao[1] = false;
                    }
                    else
                    {
                        validirao[1] = true;
                        errorProvider2.SetError(textBox2, String.Empty);
                        toolStripStatusLabel1.Text = "";
                    }
                }
            }
        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            if (textBox3.Text.Length == 0)
            {
                toolStripStatusLabel1.Text = "Molimo unesite ponovo potvrdu lozinke!";
                errorProvider3.SetError(textBox3, "Prazno polje!");
                validirao[2] = false;
            }
            else
            {
                if (textBox3.Text != textBox2.Text)
                {
                    toolStripStatusLabel1.Text = "Molimo unesite ponovo potvrdu lozinke!";
                    errorProvider3.SetError(textBox3, "Lozinke nisu jednake!");
                    validirao[2] = false;
                }
                else
                {
                    validirao[2] = true;
                    errorProvider3.SetError(textBox3, String.Empty);
                    toolStripStatusLabel1.Text = "";
                }
            }

        }
        private void maskedTextBox1_Validating(object sender, CancelEventArgs e)
        {
            if (comboBox2.Text == "Pacijent")
            {
                if (maskedTextBox1.Text.Length == 0)
                {
                    errorProvider4.SetError(maskedTextBox1, "Prazno polje!");
                    toolStripStatusLabel1.Text = "Molimo unesite ponovo JMBG!";
                    validirao[3] = false;
                }
                else
                {

                    bool tmp = NMK.ProvjeriJMBGUBazi(maskedTextBox1.Text);
                    if (!tmp)
                    {
                        toolStripStatusLabel1.Text = "Molimo unesite ponovo JMBG!";
                        errorProvider4.SetError(maskedTextBox1, "Nijedan pacijent nema navedeni JMBG!");
                        validirao[3] = false;
                    }
                    else
                    {
                        toolStripStatusLabel1.Text = "";
                        errorProvider4.SetError(maskedTextBox1, String.Empty);
                        validirao[3] = true;
                    }
                }
            }
        }

        private void oAplikacijiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Zadaća studenata sa Elektrotehničkog fakulteta u Sarajevu iz\r\n" +
                "predmeta Razvoj programskih rješenja.");
        }

        private void modulZaDoktoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Modul doktora omogućava obavljanje pregleda, evidenciju\r\n" +
                "terapija, registraciju dodatnih pregleda kao i pregled lista čekanja u ordinacijama.");
        }

        private void modulZaOsobljeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Modul osoblja omogućava registrovanje/brisanje pacijenata, kreiranje\r\n" +
                "kartona kao i pregled lista čekanja u ordinacijama.");
        }

        private void modulZaPacijenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Modul pacijenta omogućava pacijentu kompletan uvid u svoje podatke i karton" +
                "uz dodatni pregled lista čekanja u ordinacijama, bez mogućnosti izmjene istih.");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Hvala Vam na povjerenju!");
        }

        private void comboBox2_Validating(object sender, CancelEventArgs e)
        {
            if (comboBox2.Text.Length == 0)
            {
                toolStripStatusLabel1.Text = "Molimo odaberite ulogu!";
                errorProvider5.SetError(comboBox2, "Prazno polje!");
            }
            else
            {
                toolStripStatusLabel1.Text = "";
                errorProvider5.SetError(comboBox2, String.Empty);
            }
        }
    }
}
