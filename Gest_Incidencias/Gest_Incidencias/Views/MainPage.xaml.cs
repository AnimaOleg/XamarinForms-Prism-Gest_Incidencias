//using Naxam.Controls.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Gest_Incidencias.Views;

namespace Gest_Incidencias.Views
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        // Click en el ICONO de Empresa en el NavBar
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //var param = ((TappedEventArgs)e).Parameter;
            //await App.Current.MainPage.DisplayAlert("Test Title", "TapGestureRecognizer_Tapped, 2. Sender: "+sender + ", 1. EventArgs: "+e, "OK");
        }

    }
}
