using ProjetoIntegrador.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace ProjetoIntegrador
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private string CodigoCapturado1;

        public string CodigoCapturado
        {
            get { return CodigoCapturado1; }
            set
            {
                CodigoCapturado1 = value;
                nameEntry.Text = CodigoCapturado1;
            }
        }


        bool iniCapt = false;
        bool exibindoMsg = false;
        ZXingScannerPage scannerPage = null;


        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetPeopleAsync();
        }

        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                await App.Database.SavePersonAsync(new Person
                {
                    Name = nameEntry.Text + " (" + DateTime.Now + ") ",
                });

                listView.ItemsSource = await App.Database.GetPeopleAsync();
                nameEntry.Text = "";
                
            }
        }


        public async Task Capturar()
        {
            scannerPage = Util.CapturarQRCode(scannerPage, "Escanear codigo", ZXing.BarcodeFormat.QR_CODE);


            if (!iniCapt)
            {
                scannerPage.OnScanResult += (result) =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        try
                        {
                            if (exibindoMsg)
                            {
                                return;
                            }

                            scannerPage.IsScanning = false;

                            if (!string.IsNullOrEmpty(CodigoCapturado))
                            {
                                return;
                            }

                            Util.Vibrar();

                            CodigoCapturado = result.Text;
                            await Navigation.PopAsync();
                        }
                        catch (Exception ex)
                        {
                            exibindoMsg = true;
                            await this.DisplayAlert("ATENÇÃO", "CODIGO INVALIDO! TENTE NOVAMENTE", "OK");
                            exibindoMsg = false;
                        }

                    });
                };

                iniCapt = true;

            }

            CodigoCapturado = "";

            await Navigation.PushAsync(scannerPage);

        }

        private async void ButtonScanner(object sender, EventArgs e)
        {
            await Capturar();
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new Second
                {
                    BindingContext = e.SelectedItem as Person
                });
            }
        }




    }
}
