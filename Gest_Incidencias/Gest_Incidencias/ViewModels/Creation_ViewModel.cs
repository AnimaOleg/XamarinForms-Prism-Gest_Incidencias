using Gest_Incidencias.Models;
using Gest_Incidencias.Services;
using Prism.Commands;
using Prism.Navigation;
using System;
using Xamarin.Forms;

namespace Gest_Incidencias.ViewModels
{
    public class Creation_ViewModel : /*BindableBase*/ BaseViewModel
    {
        #region Variables
        private readonly IMessageService _messageService;
        private readonly INavigationService _navigationService; //public INavigation Navigation { get; set; }
        #endregion


        #region Properties
        private int _id;
        public int Id {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        private string _name;
        public string Name {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        private string _description;
        public string Description {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        private DateTime _dateCreation;
        public DateTime DateCreation {
            get => _dateCreation;
            set => SetProperty(ref _dateCreation, value);
        }

        private string _estado_Actual = "Disponible";
        public string Estado_Actual
        {
            get => _estado_Actual;
            set => SetProperty(ref _estado_Actual, value);
        }
        #endregion


        #region Commands
        public DelegateCommand Command_Cancel { private set; get; }
        public DelegateCommand Command_Create { private set; get; }
        #endregion


        #region Constructor
        public Creation_ViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Nueva Entrada";
            _navigationService = navigationService;
            _messageService = DependencyService.Get<Services.IMessageService>();

            Command_Cancel = new DelegateCommand(Execute_Cancel);
            Command_Create = new DelegateCommand(Execute_Create);
        }
        #endregion


        #region Initialize
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
        }
        #endregion


        #region Execute_Create
        async void Execute_Create()
        {
            if (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Description) && Name.Length >= 3 && Description.Length >= 3)
            {
                Note note = new Note
                {
                    Name = Name,
                    Description = Description,
                    DateCreation = DateTime.UtcNow,
                    Estado_Actual = "Disponible"
                };
                await App.Database.SaveNoteAsync(note);
                await _navigationService.GoBackAsync();
            }
            else
                await _messageService.ShowAsync(message: "Rellena el Título o la Descripción. Mínimo 3 carácteres");
        }
        #endregion


        #region Execute_Cancel()
        async void Execute_Cancel()
        {
            await _navigationService.GoBackAsync();
        }
        #endregion
    }
}