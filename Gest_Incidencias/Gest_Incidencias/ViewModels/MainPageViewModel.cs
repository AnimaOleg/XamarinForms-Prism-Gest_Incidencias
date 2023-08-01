using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Gest_Incidencias.ViewModels
{
    internal class MainPageViewModel
    {
        //private readonly Services.IMessageService _messageService;
        public INavigation Navigation { get; set; }

        public string Tareas { get; set; } = "Tareas";
        public string EnCurso { get; set; } = "En Curso";
        public string Finalizadas { get; set; } = "Finalizadas";


        public MainPageViewModel(INavigation navigation)
        {
            Navigation = navigation;
            //_messageService = DependencyService.Get<Services.IMessageService>();


        }
    }
}