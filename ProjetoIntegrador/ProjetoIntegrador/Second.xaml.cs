using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProjetoIntegrador.Helpers;

namespace ProjetoIntegrador
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Second : ContentPage
    {
        public Second()
        {
            InitializeComponent();
        }


        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Person)BindingContext;
            await App.Database.DeleteNoteAsync(note);
            await Navigation.PopAsync();
        }


        private void BtEnviar_Clicked(object sender, EventArgs e)
        {
            String des = etDest.Text;
            String ass = "Observação " + DateTime.Now;
            String text = lblCodi.Text;

            String url = "mailto:" + des + "?subject=" + ass + "&body=" + text;
            Device.OpenUri(new Uri(url));

        }



    }
}

