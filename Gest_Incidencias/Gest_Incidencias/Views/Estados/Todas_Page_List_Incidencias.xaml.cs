using Gest_Incidencias.Models;
//using Gest_Incidencias.ViewModels;
using Gest_Incidencias.ViewModels.Estados;
using Prism.Ioc;
using Prism.Navigation;
using SQLitePCL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Gest_Incidencias.Views.Estados
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Todas_Page_List_Incidencias : ContentPage
    {
        private /*readonly*/ INavigationService _navigationService;

        public string Tipo { get; set; }

        public Todas_Page_List_Incidencias()
        {
            InitializeComponent();            
        }

        protected override void OnAppearing()
        {
            Console.WriteLine(" ON APPEARING Page_List_Incidencias, TIPO: " + Tipo); // DEBUGEAR
            Console.WriteLine(" AQUI FALLO, _navigationService = null");

            BindingContext = new Todas_Page_List_IncidenciasViewModel(_navigationService, this.Tipo);
            base.OnAppearing();
        }

    }
}
