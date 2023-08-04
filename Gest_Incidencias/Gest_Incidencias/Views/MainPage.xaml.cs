using System;
using Xamarin.Forms;

namespace Gest_Incidencias.Views
{
    public partial class MainPage : TabbedPage
    {
        #region Constructor
        public MainPage()
        {
            InitializeComponent();
            // Construccion de Paginas en lugar de XAML : https://learn.microsoft.com/es-es/xamarin/xamarin-forms/app-fundamentals/navigation/tabbed-page
        }
        #endregion


        #region TapGestureRecognizer_Tapped
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            // Click en el ICONO de Empresa en el NavBar
            //var param = ((TappedEventArgs)e).Parameter;
            //await App.Current.MainPage.DisplayAlert("Test Title", "TapGestureRecognizer_Tapped, 2. Sender: "+sender + ", 1. EventArgs: "+e, "OK");
        }
        #endregion

    }
}
