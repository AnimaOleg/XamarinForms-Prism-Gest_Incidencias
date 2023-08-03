//using Gest_Incidencias.ViewModels;
using Gest_Incidencias.ViewModels;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gest_Incidencias.ViewModels
{
    public class ViewAViewModel : BaseViewModel
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private DelegateCommand _navigationCommand;
        private readonly INavigationService _navigationService;

        public DelegateCommand NavigateCommand =>
            _navigationCommand ?? (_navigationCommand = new DelegateCommand(ExecuteNavigationCommand));

        public ViewAViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "ViewA";
            _navigationService = navigationService;
        }
        public override void Initialize(INavigationParameters parameters)
        {
            Console.WriteLine("ViewA");
            base.Initialize(parameters);
        }
        async void ExecuteNavigationCommand()
        {
            Console.WriteLine("Click Boton NAVIGATE");
            await _navigationService.NavigateAsync("MainPage");
        }
    }
}
