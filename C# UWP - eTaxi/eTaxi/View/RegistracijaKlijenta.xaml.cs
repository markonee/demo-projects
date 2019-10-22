using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Popups;
using eTaxi.Model;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace eTaxi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistracijaKlijenta : Page
    {

        IMobileServiceTable<Klijent> klijentiTabela;

        public RegistracijaKlijenta()
        {
            this.InitializeComponent();
            klijentiTabela = App.MobileService.GetTable<Klijent>();
            datePicker.Date = new DateTime(2018,1,1);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            bool validacijaUspjesna = false;

            string pgreske = "";
            if(!validirajIme(textBoxIme.Text, ref pgreske) || !validirajPrezime(textBoxPrezime.Text, ref pgreske) || 
               !validirajEMail(textBoxEMail.Text, ref pgreske) || !validirajPassword(passwordBoxLozinka.Password, ref pgreske) ||
               !validirajPotvrduPassworda(passwordBoxLozinka.Password, passwordBoxPotvrdaLozinke.Password, ref pgreske) || 
               !validirajDatum(datePicker.Date, ref pgreske) || !validirajRadioButton(ref pgreske))
            {
                Greska.Text = pgreske;
                validacijaUspjesna = false;
            }
            else
            {
                Greska.Text = "";
                validacijaUspjesna = true;
            }

            if(validacijaUspjesna)
            {
                String spol;
                String datum = datePicker.Date.ToString("yyyy-MM-dd"); ;

                if(radio1.IsChecked == true)
                {
                    spol = "Žensko";
                }
                else
                {
                    spol = "Muško";
                }
                try
                {
                    Klijent klijent = new Klijent(textBoxIme.Text, textBoxPrezime.Text, textBoxEMail.Text, passwordBoxLozinka.Password,
                        "", spol, datum, false, 0);

                    // postaviti staticki sistem
                    // id prebaciti u int i stringove u enume

                    button1.IsEnabled = false;
                    klijentiTabela.InsertAsync(klijent).ContinueWith(task =>
                    {
                        button1.IsEnabled = true;
                        if (task.IsCanceled || task.IsFaulted)
                        {
                            new MessageDialog("Desila se neka greska").ShowAsync();
                        }
                        else
                        {
                            MessageDialog msgDialog = new MessageDialog("Uspješno ste unijeli novog klijenta.");
                            msgDialog.ShowAsync();

                            this.Frame.Navigate(typeof(ProfilKlijenta), klijent);
                        }

                    }, TaskScheduler.FromCurrentSynchronizationContext());
            
                }
                catch (Exception ex)
                {
                    MessageDialog msgDialogError = new MessageDialog("Error" + ex.ToString());                   
                }
            }

        }

        public bool validirajIme(string tekst, ref string imeGreska)
        {
            imeGreska = "";

            if(tekst.Length == 0)
            {
                imeGreska = "Unesite ime!";
                return false;
            }

            return true;
        }

        public bool validirajPrezime(string tekst, ref string prezimeGreska)
        {
            prezimeGreska = "";

            if (tekst.Length == 0)
            {
                prezimeGreska = "Unesite prezime!";
                return false;
            }

            return true;
        }

        public bool validirajEMail(string tekst, ref string pgreske)
        {
            bool imaLudogA = false;
            pgreske = "";

            if (tekst.Length == 0)
            {
                pgreske = "Unesite e-mail!";
                return false;
            }

            foreach (char c in tekst)
            {
                if (c == '@')
                {
                    imaLudogA = true;
                }
            }

            if (tekst.Length < 8 || !imaLudogA)
            {
                pgreske = "Neispravan unos e-maila!";
                return false;
            }

            return true;
        }

        public bool validirajPassword(string tekst, ref string pgreske2)
        {
            pgreske2 = "";

            if (tekst.Length == 0)
            {
                pgreske2 = "Unesite password!";
                return false;
            }

            if (tekst.Length < 8)
            {
                pgreske2 = "Neispravan unos passworda!";
                return false;
            }

            return true;
        }

        public bool validirajPotvrduPassworda (string lozinka, string potvrda, ref string potvrdaGreska)
        {
            potvrdaGreska = "";

            if(lozinka != potvrda)
            {
                potvrdaGreska = "Potvrda lozinke i lozinka nisu iste!";
                return false;
            }

            return true;
        }

        public bool validirajRadioButton(ref string radioButtonGreska)
        {
            radioButtonGreska = "";

            if(radio1.IsChecked == false && radio2.IsChecked == false)
            {
                radioButtonGreska = "Odaberite spol!";
                return false;
            }

            return true;
        }

        public bool validirajDatum(DateTimeOffset datum, ref string datumGreska)
        {
            datumGreska = "";

            if(datum.Year > DateTimeOffset.Now.Year - 18)
            {
                datumGreska = "Klijent mora biti punoljetan!";
                return false;
            }
            else
            {
                datumGreska = DateTimeOffset.Now.Offset.ToString();
            }

            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Registracija));
        }
    }
}
