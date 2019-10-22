using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NMK2
{

    public partial class UserControl1 : UserControl
    {
        public static bool validiranaSlika = true;

        public void NulirajSliku()
        {
            pictureBox1.Image = null;
        }

        public Image DajSliku()
        {
            return pictureBox1.Image;
        }

        public bool ValidirajDatum(DateTime dat)
        {
            return (DateTime.Now.Year - dat.Year) * 12 + DateTime.Now.Month - dat.Month <= 6;
        }

        public UserControl1()
        {
            InitializeComponent();
            pictureBox1.Image = null;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Izaberite sliku:";
                dlg.Filter = "jpg files (*.jpg)|*.jpg";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image = new Bitmap(dlg.FileName);
                }
            }
            MessageBox.Show("Veličinu slike možete prilagoditi desnim klikom na samu sliku.");
        }


        private void dateTimePicker1_Validating(object sender, CancelEventArgs e)
        {
            if (dateTimePicker1.Value > DateTime.Now)
            {
                validiranaSlika = false;
                toolStripStatusLabel1.Text = "Odaberite validan datum!";
            }
            else if (!ValidirajDatum(dateTimePicker1.Value))
            {
                validiranaSlika = false;
                toolStripStatusLabel1.Text = "Slika je starija od pola godine. Molimo izaberite noviju!";
            }
            else
            {
                validiranaSlika = true;
                toolStripStatusLabel1.Text = "";
            }

        }

        private void ukloniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        private void prilagodiVeličinuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

        }
    }
}
