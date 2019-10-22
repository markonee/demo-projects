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
using NMK.Ordinacije;
using NMK.Pacijenti;
using NMK.Uposlenici;

namespace NMK2
{
    public partial class Form2 : Form
    {
        public static Form1 babo;
        Doktor d = null;
        int brojOrdinacije = 0;
        List<Pacijent> cekaona;
        int indexOrdinacije = 0;
        bool prazna = false;
        Pacijent p = null;
        int indexKartona = 0;
        Terapija t = null;
        int indexDoktora = 0;
        bool pritisnuoLijevi = false;

        public bool ImalBroja(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] >= '0' && s[i] <= '9') return true;
            }
            return false;
        }

        public void OcistiLijevo()
        {
            textBox1.Text = textBox2.Text = "";
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }

        public void OcistiDesno()
        {
            label6.Visible = false;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker1.Visible = false;
            radioButton3.Checked = radioButton4.Checked = radioButton2.Checked = radioButton1.Checked = false;
            textBox3.Text = "";
        }

        public Form2()
        {
            InitializeComponent();
            cekaona = new List<Pacijent>();
            d = Form1.NMK.DajDoktora(Form1.unesenoKorisnickoIme);
            brojOrdinacije = d.Ordinacija.Broj;
            indexOrdinacije = Form1.NMK.Ordinacije.FindIndex(x => x.Broj == brojOrdinacije);
            cekaona = Form1.NMK.Ordinacije[indexOrdinacije].ListaCekanja;
            t = new Terapija();
            if (cekaona.Count == 0)
            {
                prazna = true;
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
            }
            else
            {
                p = cekaona[0];
                indexKartona = Form1.NMK.Kartoni.FindIndex(x => x.JMBGPacijenta == p.Jmbg);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (prazna) MessageBox.Show("Pošto je vaša lista čekanja prazna, moguća je samo\r\n" +
            "funkcionalnost pregleda svih lista čekanja!");
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Doviđenja!");
            babo.Show();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (prazna == false)
            {
                Form1.NMK.Kartoni[indexKartona].DodajMisljenje(textBox1.Text);
                Form1.NMK.Kartoni[indexKartona].DodajRezultat(textBox2.Text);
                Form1.NMK.RegistrujNoviPregled(p.Jmbg, indexOrdinacije, brojOrdinacije, indexKartona);
                foreach (object itemChecked in checkedListBox1.CheckedItems)
                {
                    Form1.NMK.DodajPregled(p.Jmbg, itemChecked.ToString());
                }
                MessageBox.Show("Uspješno obavljen pregled!");
                pritisnuoLijevi = true;
                toolStripStatusLabel1.Text = "";
                errorProvider5.SetError(button1, String.Empty);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OcistiLijevo();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (prazna == false)
            {
                if (radioButton1.Checked == false && radioButton2.Checked == false)
                {
                    errorProvider3.SetError(radioButton1, "Prazna polja!");
                    toolStripStatusLabel2.Text = "Molimo popravite unos!";
                }
                else if (radioButton3.Checked == false && radioButton4.Checked == false)
                {
                    errorProvider4.SetError(radioButton3, "Prazna polja!");
                    toolStripStatusLabel2.Text = "Molimo popravite unos!";
                }
                else if (pritisnuoLijevi == false)
                {
                    toolStripStatusLabel1.Text = "Molimo prvo registrujte pregled!";
                    errorProvider5.SetError(button1, "Pregled nije registrovan!");
                }
                else
                {

                    errorProvider3.SetError(radioButton2, String.Empty);
                    errorProvider4.SetError(radioButton3, String.Empty);
                    toolStripStatusLabel2.Text = "";
                    pritisnuoLijevi = false;
                    t.Propisano = textBox3.Text;
                    t.DatumPropisivanja = DateTime.Now;
                    indexDoktora = Form1.NMK.Doktori.FindIndex(x => x.Ordinacija.Broj == brojOrdinacije);
                    Form1.NMK.Doktori[indexDoktora].ObavioPregled();
                    t.GarancijaDoktora = Form1.NMK.Doktori[indexDoktora];
                    if (radioButton1.Checked == true) t.DugorocnaTerapija = true;
                    else t.DugorocnaTerapija = false;
                    MessageBox.Show("Terapija uspješno prijavljena!");
                    OcistiDesno();
                    OcistiLijevo();
                }
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton3.Checked == true && prazna == false)
            {
                dateTimePicker1.Visible = true;
                label6.Visible = true;
            }
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (prazna == false)
            {
                if (ImalBroja(textBox1.Text)) errorProvider1.SetError(textBox1, "Ne smije sadržavati broj!");
                else if (textBox1.Text.Length == 0) errorProvider1.SetError(textBox1, "Prazno polje!");
                else errorProvider1.SetError(textBox1, String.Empty);
            }
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            if (prazna == false)
            {
                if (ImalBroja(textBox2.Text)) errorProvider2.SetError(textBox1, "Ne smije sadržavati broj!");
                else if (textBox2.Text.Length == 0) errorProvider2.SetError(textBox2, "Prazno polje!");
                else errorProvider2.SetError(textBox2, String.Empty);
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked == true && prazna == false)
            {
                dateTimePicker1.Visible = false;
                label6.Visible = false;
            }
        }
    }
}
