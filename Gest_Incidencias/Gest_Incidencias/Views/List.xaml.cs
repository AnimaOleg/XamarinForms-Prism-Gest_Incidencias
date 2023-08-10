using Gest_Incidencias.ViewModels;
using Prism.Ioc;
using Prism.Navigation;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gest_Incidencias.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class List : ContentPage
    {
        #region Variables
        private INavigationService _navigationService;
        public string Tipo { get; set; }
        #endregion


        #region Constructor
        public List()
        {
            this._navigationService = ContainerLocator.Container.Resolve<INavigationService>();
            InitializeComponent();
        }
        #endregion


        #region OnAppearing
        protected override void OnAppearing()
        {
            Tipo = Tipo;
            BindingContext = new List_ViewModel(_navigationService, this.Tipo /*, Contador_seleccion*/);
            base.OnAppearing();
        }
        #endregion

    }

}
