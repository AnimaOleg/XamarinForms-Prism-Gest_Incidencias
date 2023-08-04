using Gest_Incidencias.ViewModels;
using Prism.Ioc;
using Prism.Navigation;
using System;
using Xamarin.Forms;

namespace Gest_Incidencias.Views
{
    public partial class Creation_Page : ContentPage
    {
        #region Variables
        private INavigationService _navigationService;
        #endregion


        #region Constructor
        public Creation_Page()
        {
            this._navigationService = ContainerLocator.Container.Resolve<INavigationService>();
            InitializeComponent();
        }
        #endregion


        #region OnAppearing
        protected override void OnAppearing()
        {
            BindingContext = new Creation_Page_ViewModel(_navigationService);
            base.OnAppearing();
        }
        #endregion


    }
}