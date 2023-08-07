using Gest_Incidencias.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gest_Incidencias.ViewModels
{
    public class Settings_ViewModel : /*BindableBase*/ BaseViewModel
    {
        #region Variables
        private /*readonly*/ IMessageService _messageService;
        private /*readonly*/ INavigationService _navigationService;
        #endregion

        #region Properties
        public string Tipo { get; set; }
        #endregion

        #region Constructor
        public Settings_ViewModel(INavigationService navigationService) : base(navigationService)
        { }

        public Settings_ViewModel(INavigationService navigationService, string tipo) : base(navigationService)
        {
        }
        #endregion
    }
}
