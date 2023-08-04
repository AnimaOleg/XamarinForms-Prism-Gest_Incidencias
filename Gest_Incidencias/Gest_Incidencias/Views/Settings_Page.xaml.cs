using Xamarin.Forms;

namespace Gest_Incidencias.Views
{
    public partial class Settings_Page : NavigationPage
    {
        #region Constructor
        public Settings_Page()
        {
            InitializeComponent();
        }
        #endregion

        #region OnAppearing
        protected override void OnAppearing()
        {
            //Tipo = Tipo;
            //BindingContext = new List_Page_ViewModel(_navigationService, this.Tipo);
            base.OnAppearing();
        }
        #endregion

    }
}
