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
    public sealed partial class RegistracijaVozaca : Page
    {

        IMobileServiceTable<Vozac> vozaciTabela = App.MobileService.GetTable<Vozac>();

        IMobileServiceTable<Auto> autoTabela = App.MobileService.GetTable<Auto>();

        public RegistracijaVozaca()
        {
            this.InitializeComponent();
            List<String> regioni = new List<string>();

            regioni.Add("Sarajevo");
            regioni.Add("Banja Luka");
            regioni.Add("Mostar");
            regioni.Add("Zenica");
            regioni.Add("Tuzla");
            regioni.Add("Bihać");
            regioni.Add("Vitez");

            cBoxRegioni.ItemsSource = regioni;
            cBoxRegioni.SelectedIndex = 0;
            Pivot.SelectedIndex = 0;

        }

        private void dugmeNastavi1_Click(object sender, RoutedEventArgs e)
        {
            bool validacijaProsla = false;

            // validiraj
            string pgreske = "";
            if (!validirajIme(textBoxIme.Text, ref pgreske) || !validirajPrezime(textBoxPrezime.Text, ref pgreske) ||
               !validirajEMail(textBoxEMail.Text, ref pgreske) || !validirajPassword(passwordBoxLozinka.Password, ref pgreske) ||
               !validirajPotvrduPassworda(passwordBoxLozinka.Password, passwordBoxPotvrdaLozinke.Password, ref pgreske) ||
               !validirajDatum(datePicker.Date, ref pgreske) || !validirajRadioButton(ref pgreske))
            {
                Greska.Text = pgreske;
                validacijaProsla = false;
            }
            else
            {
                Pivot.SelectedIndex = 1;
                Greska.Text = "";
                validacijaProsla = true;
            }
        }

        private void buttonNazad_Click(object sender, RoutedEventArgs e)
        {
            Pivot.SelectedIndex = 0;
        }

        private void buttonNastavi_Click(object sender, RoutedEventArgs e)
        {
            string pgreske = "";
            if (!ValidirajJMBG(textBoxJMBG.Text, ref pgreske) || !ValidirajBroj(textBoxTelefon.Text, ref pgreske) ||
                !ValidirajAdresu(textBoxAdresa.Text, ref pgreske) || !ValidirajKarticu(textBoxBrojKartice.Text, ref pgreske))
            {
                Greska.Text = pgreske;
            }

            else
            {
                Greska.Text = "";
                Pivot.SelectedIndex = 2;
            }
        }

        private void buttonNazadZadnja_Click(object sender, RoutedEventArgs e)
        {
            Pivot.SelectedIndex = 1;
        }

        private void buttonNastaviZadnja_Click(object sender, RoutedEventArgs e)
        {
            bool validacijaProsla = false;

            string pgreske = "";
            if(!ValidirajModel(textBoxModel.Text, ref pgreske))
            {
                Greska.Text = pgreske;
                validacijaProsla = false;
            }

            else
            {
                Greska.Text = "";
                validacijaProsla = true;
            }

            if (validacijaProsla)
            {
                String spol;
                String datum = datePicker.Date.ToString();
                //Region region = (Region)(cBoxRegioni.SelectedIndex);

                if (radio1.IsChecked == true)
                {
                    spol = "Žensko";
                }
                else
                {
                    spol = "Muško";
                }
                try
                {
                    Auto auto = new Auto((int)(SliderBrojSjedista.Value), textBoxModel.Text, String.Empty);

                    buttonNastaviZadnja.IsEnabled = false;
                    autoTabela.InsertAsync(auto).ContinueWith(task =>
                    {
                        buttonNastaviZadnja.IsEnabled = true;
                        if (task.IsCanceled || task.IsFaulted)
                        {
                            new MessageDialog("Desila se neka greska").ShowAsync();
                        }
                        else
                        {
                            Vozac vozac = new Vozac(textBoxIme.Text, textBoxPrezime.Text, textBoxEMail.Text, passwordBoxLozinka.Password,
                                                    "/Assets/eTaxiLogo.png", datum, spol, cBoxRegioni.SelectedValue.ToString(), textBoxJMBG.Text, textBoxTelefon.Text, textBoxAdresa.Text,
                                                    textBoxBrojKartice.Text, textBoxRadnoIskustvo.Text, "Čeka potvrdu", "Neodobren", auto.Id, 0, 0);

                            vozaciTabela.InsertAsync(vozac);

                            MessageDialog msgDialog = new MessageDialog("Uspješno ste unijeli novog vozaca.");
                            msgDialog.ShowAsync();

                            Tuple<Vozac, Auto> par = new Tuple<Vozac, Auto>(vozac, auto);

                            this.Frame.Navigate(typeof(ProfilVozaca), par);
                        }

                    }, TaskScheduler.FromCurrentSynchronizationContext());
                }
                catch (Exception ex)
                {
                    MessageDialog msgDialogError = new MessageDialog("Error" + ex.ToString());
                }

            }

        }

        private void Povratak_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Registracija));
        }


        public bool validirajIme(string tekst, ref string imeGreska)
        {
            imeGreska = "";

            if (tekst.Length == 0)
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

        public bool validirajPotvrduPassworda(string lozinka, string potvrda, ref string potvrdaGreska)
        {
            potvrdaGreska = "";

            if (lozinka != potvrda)
            {
                potvrdaGreska = "Potvrda lozinke i lozinka nisu iste!";
                return false;
            }

            return true;
        }

        public bool validirajRadioButton(ref string radioButtonGreska)
        {
            radioButtonGreska = "";

            if (radio1.IsChecked == false && radio2.IsChecked == false)
            {
                radioButtonGreska = "Odaberite spol!";
                return false;
            }

            return true;
        }

        public bool validirajDatum(DateTimeOffset datum, ref string datumGreska)
        {
            datumGreska = "";

            if (datum.Year > DateTimeOffset.Now.Year - 18)
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

        public bool ValidirajJMBG(string JMBG, ref string poruka)
        {
            poruka = "";

            if (JMBG.Length != 13)
            {
                poruka = "JMBG mora imati 13 cifara!";
                return false;
            }

            if(!ProvjeriJmbg(JMBG, datePicker.Date))
            {
                poruka = "JMBG i datum rođenja se ne podudaraju!";
                return false;
            }

            return true;
        }

        public bool ValidirajBroj(string broj, ref string poruka)
        {
            poruka = "";

            if (!provjeriBroj(broj))
            {
                poruka = "Broj mora imati minimalno 6 cifara!";
                return false;
            }

            return true;
        }

        public bool ValidirajAdresu(string adresa, ref string poruka)
        {
            poruka = "";

            if(adresa.Length == 0)
            {
                poruka = "Unesite adresu!";
                return false;
            }

            return true;
        }

        public bool ValidirajKarticu(string kartica, ref string poruka)
        {
            poruka = "";

            if(kartica.Length != 12)
            {
                poruka = "Unesite broj kartice (12 cifara)! ";
                return false;
            }

            foreach(char c in kartica)
            {
                if(!char.IsDigit(c))
                {
                    poruka = "Unesite broj kartice!";
                    return false;
                }
            }

            return true;
        }

        private bool provjeriBroj(string broj)
        {
            int d = broj.Length;
            if (d < 6) return false;
            int brojCifara = 0;

            for(int i = 0; i < d; i++)
            {
                if (Char.IsDigit(broj[i])) brojCifara++;
                if (Char.IsLetter(broj[i])) return false;
            }

            if (brojCifara < 6) return false;

            return true;
        }

        private bool ProvjeriJmbg(string jmbg, DateTimeOffset datRodjenja)
        {
            if (jmbg.Length != 13) return false;

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

        public bool ValidirajModel(string model, ref string poruka)
        {
            poruka = "";
            
            if(model.Length == 0)
            {
                poruka = "Unesite model automobila!";
                return false;
            }

            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Registracija));
        }
    }
}
