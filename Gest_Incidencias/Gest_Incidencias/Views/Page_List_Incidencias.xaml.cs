using Gest_Incidencias.Models;
using Gest_Incidencias.ViewModels;
using Prism.Ioc;
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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_List_Incidencias : ContentPage
    {
        public string Tipo { get; set; }
        public Page_List_Incidencias()
        {
            InitializeComponent();            
        }
        protected override void OnAppearing()
        {
            BindingContext = new Page_List_IncidenciasViewModel(Navigation, this.Tipo);
            base.OnAppearing();
        }

    }
}
