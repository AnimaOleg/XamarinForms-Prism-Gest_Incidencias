using Gest_Incidencias.Data;
using Gest_Incidencias.Services;
using Gest_Incidencias.ViewModels;
using Gest_Incidencias.Views;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using System;
using System.IO;
using Xamarin.Forms;

namespace Gest_Incidencias
{
    public partial class App /*: Application*/
    {
        #region Variables
        static NoteDatabase database;
        public INavigationService MyNavigationService => NavigationService; //  llamando a MyNavigationService lo que haces es llamar a NavigationService
        #endregion


        #region Constructor
        public App() : this(null) { }
        public App(IPlatformInitializer initializer) : base(initializer) { }
        #endregion


        #region OnInitialized
        protected override async void OnInitialized()
        {
            InitializeComponent();
            DependencyService.Register<IMessageService, MessageService>();

            await MyNavigationService.NavigateAsync("MainPage");
            //await MyNavigationService.NavigateAsync("Settings");
            //await NavigationService.NavigateAsync("MainPage?message=Hello%20From%20PrismApplication"); // Para el ejemplo de Mensajes
        }
        #endregion


        #region RegisterTypes
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();

            containerRegistry.RegisterForNavigation<MainPage, MainPage_ViewModel>();
            containerRegistry.RegisterForNavigation<List, List_ViewModel>();
            containerRegistry.RegisterForNavigation<Creation, Creation_ViewModel>();
            containerRegistry.RegisterForNavigation<Details, Details_ViewModel>();
            containerRegistry.RegisterForNavigation<Settings, Settings_ViewModel>();

            //containerRegistry.RegisterForNavigation<MainPage>("MiAlias");
        }
        #endregion


        #region Database
        public static NoteDatabase Database
        {
            get
            {
                if (database == null)
                    database = new NoteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "IncidencesAicom.db3"));
                return database;
            }
        }
        #endregion


    }
}
