using Gest_Incidencias.Models;
using Gest_Incidencias.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using Prism.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Gest_Incidencias.ViewModels
{
    public class Page_Entry_IncidenceViewModel : /*BindableBase*/ BaseViewModel
    {
        #region Variables
        private readonly Services.IMessageService _messageService;
        private /*readonly*/ INavigationService _navigationService; //public INavigation Navigation { get; set; }
        #endregion


        #region Properties
        private int _id;
        public int Id {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        private string _title;
        public string Title {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        private string _description;
        public string Description {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        private DateTime _dateCreation;
        public DateTime DateCreation {
            get { return _dateCreation; }
            set { SetProperty(ref _dateCreation, value); }
        }
        private bool _isAvailable;
        public bool IsAvailable
        {
            get { return _isAvailable; }
            set { SetProperty(ref _isAvailable, value);
            }
        }
        #endregion


        #region Commands
        //public DelegateCommand UnfocusedTitleCommand { private set; get; }
        public DelegateCommand Command_TextChanged { private set; get; }
        public DelegateCommand Command_Cancel { private set; get; }
        public DelegateCommand Command_Create { private set; get; }

        /*private DelegateCommand _cancelCommand;
        public DelegateCommand CancelCommand =>
            _cancelCommand ?? (_cancelCommand = new DelegateCommand(Execute_Cancel_Command));*/

        private DelegateCommand _navigationCommand;
        public DelegateCommand NavigateCommand =>
            _navigationCommand ?? (_navigationCommand = new DelegateCommand(ExecuteNavigationCommand));

        #endregion


        #region Constructor
        public Page_Entry_IncidenceViewModel(INavigationService navigationService) : base(navigationService)
        {
            //Title = " VISTA Page_Entry_IncidenceViewModel";
            _navigationService = navigationService;
            _messageService = DependencyService.Get<Services.IMessageService>();

            Command_Cancel = new DelegateCommand(Execute_Cancel_Command);
            Command_Create = new DelegateCommand(Execute_Create_Command);
            Command_TextChanged = new DelegateCommand(Execute_TextChanged);
            //UnfocusedTitleCommand = new Command(UnfocusedTitle);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            Console.WriteLine(" INITIALIZE Page_Entry_IncidenceViewModel");
            base.Initialize(parameters);
        }
        #endregion


        #region CommandsFunctions
        async void ExecuteNavigationCommand()
        {
            Console.WriteLine("Click Boton NAVIGATE A");
            await _navigationService.NavigateAsync("ViewA");
        }

        void Execute_TextChanged()
        {
            if (Title == "")
            {
                IsAvailable = false;
            } else if(Description == ""){
                IsAvailable = false;
            }
            else {
                IsAvailable = true;
            }
        }
        
        async void Execute_Create_Command()
        {
            Console.WriteLine("Dentro");
            //var note = (Note)BindingContext;
            Note note = new Note {
                Title = Title,
                Description = Description,
                IsAvailable = IsAvailable,
                DateCreation = DateTime.UtcNow,
                Tipo = "Disponibles"
            };

            try {
                if (!string.IsNullOrWhiteSpace(note.Title) || !string.IsNullOrWhiteSpace(note.Description)) {
                    try {
                        await App.Database.SaveNoteAsync(note);
                        await NavigationService.NavigateAsync("MainPage");
                    }
                    catch (Exception ex) {
                        await _messageService.ShowAsync("Error: " + ex.Message);
                    }
                }
                else
                    await _messageService.ShowAsync(message: "Rellena el Título o la Descripción");
            }
            catch (Exception ex) {
                await _messageService.ShowAsync("Error de IsNullOrWhiteSpace: " + ex.Message);
            }

            //await Navigation.PopAsync(); // no deja la lista actualizada
        }

        async void Execute_Cancel_Command() {
            //await Navigation.PopAsync();
            //await NavigationService.GoBackAsync();
            await NavigationService.NavigateAsync("ViewA");

            //await NavigationService.NavigateAsync("MainPage");
        }
        #endregion
    }
}