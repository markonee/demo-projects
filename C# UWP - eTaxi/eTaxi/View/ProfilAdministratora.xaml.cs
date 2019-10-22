using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using eTaxi.Model;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using Windows.Media.Capture;
using Windows.Storage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace eTaxi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProfilAdministratora : Page
    {

        Klijent klijent;
        IMobileServiceTable<Vozac> vozacTabela;
        IMobileServiceTable<Klijent> klijentTabela;

        public ProfilAdministratora()
        {
            this.InitializeComponent();
            vozacTabela = App.MobileService.GetTable<Vozac>();
            klijentTabela = App.MobileService.GetTable<Klijent>();
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
            DajBrojVozaca();
            DajBrojKlijenata();
        }

        private async Task DajBrojVozaca()
        {
            await vozacTabela.ToListAsync().ContinueWith(vozacTask =>
            {
                List<Vozac> vozaci = vozacTask.Result;
                labelaBrojVozaca.Text = vozaci.Count().ToString();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async Task DajBrojKlijenata()
        {
            await klijentTabela.ToListAsync().ContinueWith(klijentTask =>
            {
                List<Klijent> klijenti = klijentTask.Result;
                labelaBrojKlijenata.Text = klijenti.Count().ToString();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void buttonIzmijeniSliku_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker
            {
                SuggestedStartLocation = PickerLocationId.PicturesLibrary,
                FileTypeFilter = { ".jpg", ".jpeg", ".png", ".gif" }
            };
            var file = picker.PickSingleFileAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
