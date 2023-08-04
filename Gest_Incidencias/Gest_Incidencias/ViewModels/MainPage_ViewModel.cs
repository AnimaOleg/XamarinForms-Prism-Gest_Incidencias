using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Gest_Incidencias.ViewModels
{
    public class MainPage_ViewModel : BaseViewModel
    {
        private /*readonly*/ INavigationService _navigationService;

        public MainPage_ViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Pagina Princial";
            _navigationService = navigationService;
        }
        public override void Initialize(INavigationParameters parameters)
        {
            //Console.WriteLine("PARAMETERS:" + parameters.ToString());
        }


        //public override void OnNavigatedFrom(INavigationParameters parameters)
        //{
        //}
        //public override void OnNavigatedTo(INavigationParameters parameters)
        //{
        //    //Message = parameters.GetValue<string>("message");
        //}
        //async void ExecuteNavigationCommand()
        //{
        //    Console.WriteLine("Click Boton NAVIGATE TO A");
        //    await _navigationService.NavigateAsync("Page_Entry_Incidence");
        //}






        /*
        public INavigation Navigation { get; set; }

        public string Tareas { get; set; } = "Tareas";
        public string EnCurso { get; set; } = "En Curso";
        public string Finalizadas { get; set; } = "Finalizadas";
        public MainPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            //_messageService = DependencyService.Get<Services.IMessageService>();
        }
        */
    }
}