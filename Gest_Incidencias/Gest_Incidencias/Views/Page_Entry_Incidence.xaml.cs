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
    public partial class Page_Entry_Incidence : ContentPage
    {
        private /*readonly*/ INavigationService _navigationService;

        public Page_Entry_Incidence()
        {
            this._navigationService = ContainerLocator.Container.Resolve<INavigationService>();
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            Console.WriteLine(" ON APPEARING Page_Entry_IncidenceViewModel"); // DEBUGEAR
            BindingContext = new Page_Entry_IncidenceViewModel(_navigationService);
            base.OnAppearing();
        }



    }
}