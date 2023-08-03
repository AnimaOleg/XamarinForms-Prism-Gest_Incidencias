using Gest_Incidencias.Data;
using Gest_Incidencias.Services;
using Gest_Incidencias.ViewModels;
using Gest_Incidencias.ViewModels.Estados;
using Gest_Incidencias.Views;
using Prism;
using Prism.Ioc;
using Prism.Navigation;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Mvvm;
using System.Reflection;

namespace Gest_Incidencias
{
    public partial class App /*: Application*/
    {
        static NoteDatabase database;

        // Nueva Navegacion
        public INavigationService MyNavigationService => NavigationService;
        public App() : this(null) { }
        public App(IPlatformInitializer initializer) : base(initializer) { }

        public static NoteDatabase Database
        {
            get {
                if (database == null)
                    database = new NoteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "IncidencesAicom.db3"));
                return database;
            }
        }


        protected override async void OnInitialized()
        {
            DependencyService.Register<IMessageService, MessageService>();
            InitializeComponent();

            await NavigationService.NavigateAsync("/MainPage");

            //await NavigationService.NavigateAsync("/MainPage?message=Hello%20From%20PrismApplication"); // Para el ejemplo de Mensajes
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();

            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<Page_List_Incidencias, Page_List_IncidenciasViewModel>();
            //containerRegistry.RegisterForNavigation<Page_List_Incidencias, ViewModels.Estados.Todas_Page_List_IncidenciasViewModel>();
            containerRegistry.RegisterForNavigation<Page_Entry_Incidence, Page_Entry_IncidenceViewModel>();
            containerRegistry.RegisterForNavigation<Page_Item_Detail, Page_Item_DetailViewModel>();
            containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>();




            // Tell the Unity ViewModelLocator to use the Unity container to resolve ViewModels
            //ViewModelLocator.SetDefaultViewModelFactory((viewModelType) => _container.Resolve(viewModelType));
            //ViewModelLocator.SetAutowireViewModel((viewModelType) => _container.Resolve(viewModelType));
            //SetDefaultViewTypeToViewModelTypeResolver

            //ViewModelLocator.SetAutowireViewModel(ViewAViewModel, true);

            //containerRegistry.RegisterForNavigation<MainPage>("MiAlias");
        }


        //protected override void ConfigureViewModelLocator()
        //{
        //    base.ConfigureViewModelLocator();

        //    ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
        //    {
        //        var viewName = viewType.FullName.Replace(".ViewModels.", ".CustomNamespace.");
        //        var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
        //        var viewModelName = $"{viewName}ViewModel, {viewAssemblyName}";
        //        return Type.GetType(viewModelName);
        //    });
        //}


        /* Antigua Navegacion
        public App()
        {
            DependencyService.Register<IMessageService, MessageService>();

            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }*/

        //protected override void RegistryTypes(IContainerRegistry containerRegistry) => containerRegistry.RegisterForNavigation<MainPage>();
        /*protected override void OnStart() { }
        protected override void OnSleep() { }
        protected override void OnResume() { }*/
    }
}
