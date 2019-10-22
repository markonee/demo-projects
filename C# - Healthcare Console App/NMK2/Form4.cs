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

namespace NMK2
{
    public partial class Form4 : Form
    {
        public static Form1 babo;
        string jmbg;

        public Form4()
        {
            InitializeComponent();
            jmbg = Form1.uneseniJMBG;
            Pacijent p = Form1.NMK.DajPacijenta(jmbg);
            pictureBox1.Image = p.SlikaPacijenta;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            textBox2.Text = p.Ime;
            textBox3.Text = p.Prezime;
            textBox5.Text = p.DatumRodjenja.ToString();
            textBox6.Text = p.Jmbg;
            if (p.SpolPacijenta == Pacijent.Spol.musko) textBox7.Text = "Muško";
            else textBox7.Text = "Žensko";
            textBox8.Text = p.Adresa;
            if (p.UBraku == true) textBox9.Text = "U braku";
            else textBox9.Text = "Slobodan";

            textBox10.Text = Form1.NMK.PretraziKarton(jmbg);
            if (Form1.NMK.PretraziKarton(jmbg) != "Navedeni pacijent nema svoj karton.\n")
            {
                textBox10.Text += "\r\n\r\nZakazani pregledi: \r\n" + Form1.NMK.IspisiMiPreglede(p.Jmbg);
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            
        }


        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Doviđenja!");
            babo.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = Form1.NMK.Zauzetost();
            textBox1.Text = s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
    }
}
