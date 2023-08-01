using System;
using System.IO;
using Gest_Incidencias;
using Gest_Incidencias.Models;
using Gest_Incidencias.ViewModels;
using Xamarin.Forms;

namespace Gest_Incidencias.Views
{
    public partial class Page_Entry_Incidence : ContentPage
    {
        public Page_Entry_Incidence()
        {
            InitializeComponent();
            BindingContext = new Page_Entry_IncidenceViewModel(Navigation); // funciona sin ponerlo
        }

    }
}