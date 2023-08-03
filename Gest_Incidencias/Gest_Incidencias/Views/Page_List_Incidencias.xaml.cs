using Gest_Incidencias.Models;
using Gest_Incidencias.ViewModels;
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

namespace Gest_Incidencias.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_List_Incidencias : ContentPage
    {
        private /*readonly*/ INavigationService _navigationService;

        public string Tipo { get; set; }

        public Page_List_Incidencias()
        {
            this._navigationService = ContainerLocator.Container.Resolve<INavigationService>();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            Tipo = Tipo;
            BindingContext = new Page_List_IncidenciasViewModel(_navigationService, this.Tipo);
            base.OnAppearing();
            Console.WriteLine(" ON APPEARING Page_List_Incidencias, TIPO: " + Tipo);
        }

    }
}
