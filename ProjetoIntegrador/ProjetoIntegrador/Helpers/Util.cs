using Plugin.Vibrate;
using System;
using System.Collections.Generic;
using System.Text;
using ZXing.Net.Mobile.Forms;

namespace ProjetoIntegrador.Helpers
{
    public class Util
    {
        public static void Vibrar()
        {
            try
            {
                var v = CrossVibrate.Current;
                v.Vibration();
            }
            catch
            {

            }

        }


        public static ZXingScannerPage CapturarQRCode(ZXingScannerPage scannerPage, string titulo, ZXing.BarcodeFormat formato)
        {

            if (scannerPage == null)
            {
                var options = new ZXing.Mobile.MobileBarcodeScanningOptions();
                options.PossibleFormats = new List<ZXing.BarcodeFormat>()
                {
                    formato
                };


                // diretiva de compilação especifica para o Android
#if __Android__
        // Initialize the scanner frist so it can track the current context
    MobileBarcodeScanner.Initialize (Application);
#endif
                scannerPage = new ZXingScannerPage(options);
                scannerPage.IsScanning = true;

            }

            scannerPage.Title = titulo;

            return scannerPage;


        }

    }
}
