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
    public partial class Page_Item_Detail : ContentPage
    {
        private /*readonly*/ INavigationService _navigationService;

        public Page_Item_Detail()
        {
            this._navigationService = ContainerLocator.Container.Resolve<INavigationService>();
            InitializeComponent();
            //BindingContext = new Page_Entry_IncidenceViewModel(Navigation); // funciona sin ponerlo
        }


        protected override void OnAppearing()
        {
            Console.WriteLine(" ON APPEARING Page_Entry_IncidenceViewModel");
            BindingContext = new Page_Item_DetailViewModel(_navigationService);
            base.OnAppearing();
        }



    }
}