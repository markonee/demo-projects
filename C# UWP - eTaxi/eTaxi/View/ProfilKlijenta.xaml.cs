using eTaxi.Model;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace eTaxi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProfilKlijenta : Page
    {
        Klijent klijent;

        public ProfilKlijenta()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            klijent = e.Parameter as Klijent;
            ShowData();
        }

        private void ShowData()
        {
            labelaIme.Text = klijent.ime;
            labelaPrezime.Text = klijent.prezime;
            labelaSpol.Text = klijent.spol;
            labelaDatum.Text = klijent.datum_rodenja;
            labelaBrojVoznji.Text = "Broj vožnji: " + klijent.brojVoznji.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
