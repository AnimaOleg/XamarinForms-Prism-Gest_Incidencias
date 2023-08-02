using System;
using System.IO;
using Gest_Incidencias;
using Gest_Incidencias.Models;
using Gest_Incidencias.ViewModels;
using Prism.Navigation;
using Xamarin.Forms;

namespace Gest_Incidencias.Views
{
    public partial class Page_Entry_Incidence : ContentPage
    {
        private /*readonly*/ INavigationService _navigationService;

        public Page_Entry_Incidence()
        {
            InitializeComponent();
            //BindingContext = new Page_Entry_IncidenceViewModel(Navigation); // funciona sin ponerlo
        }


        protected override void OnAppearing()
        {
            Console.WriteLine(" ON APPEARING Page_Entry_IncidenceViewModel"); // DEBUGEAR
            //BindingContext = new Page_Entry_IncidenceViewModel(_navigationService);
            //base.OnAppearing();
        }



    }
}