using Gest_Incidencias.ViewModels;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gest_Incidencias.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        #region Variables
        private INavigationService _navigationService;
        public string Tipo { get; set; }
        #endregion


        #region Constructor
        public Settings(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitializeComponent();
        }
        #endregion

        #region OnAppearing
        protected override void OnAppearing()
        {
            Tipo = Tipo;
            BindingContext = new Settings_ViewModel(_navigationService, this.Tipo);
            base.OnAppearing();
        }
        #endregion

    }
}
