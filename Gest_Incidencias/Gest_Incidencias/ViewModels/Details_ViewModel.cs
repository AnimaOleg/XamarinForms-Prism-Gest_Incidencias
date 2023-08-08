using Gest_Incidencias.Models;
using Gest_Incidencias.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Gest_Incidencias.ViewModels
{
    public class Details_ViewModel : BaseViewModel
    {
        #region Variables
        private readonly Services.IMessageService _messageService;
        private readonly INavigationService _navigationService; //public INavigation Navigation { get; set; }
        #endregion


        #region Properties
        public string Tipo { get; set; }

        private int _id;
        public int Id {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _nombre;
        public string Name {
            get { return _nombre; }
            set { SetProperty(ref _nombre, value); }
        }

        private string _description;
        public string Description {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private DateTime _dateCreation;
        public DateTime DateCreation
        {
            get { return _dateCreation; }
            set { SetProperty(ref _dateCreation, value); }
        }

        private string _dateModification;
        public string DateModification
        {
            get { return _dateModification; }
            set { SetProperty(ref _dateModification, value); }
        }

        private string _dateStarting;
        public string DateStarting
        {
            get { return _dateStarting; }
            set { SetProperty(ref _dateStarting, value); }
        }

        private string _dateFinish;
        public string DateFinish
        {
            get { return _dateFinish; }
            set { SetProperty(ref _dateFinish, value); }
        }

        private string _dateDeleted;
        public string DateDeleted
        {
            get { return _dateDeleted; }
            set { SetProperty(ref _dateDeleted, value); }
        }
        /*private bool _isAvailableProperty = false;
        public bool IsAvailableProperty
        {
            get => _isAvailableProperty;
            set => SetProperty(ref _isAvailableProperty, value);
        }*/

        private string _estado_Actual;
        public string Estado_Actual{
            get { return _estado_Actual; }
            set { SetProperty(ref _estado_Actual, value); }
        }

        
        public bool Is_Started_State
        {
            //get{
            //    if (Estado_Actual == "Disponible") return true;
            //    else if (Estado_Actual == "Renovado") return true;
            //    else return false;
            //}
            get => Estado_Actual == "Disponible" ? true : Estado_Actual == "Renovado" ? true : false;
        }
        public bool Is_Finish_State{
            //get{if (Estado_Actual == "Iniciado") return true;else return false;}
            get => Estado_Actual == "Iniciado" ? true : false;
        }
        public Color Is_Available_State_BorderColor{
            //get{
            //    if (Estado_Actual == "Disponible") return Color.Green;
            //    else if (Estado_Actual == "Renovado") return Color.Green;
            //    else return Color.LightGray;
            //}
            get => Estado_Actual == "Disponible" ? Color.Green : Estado_Actual == "Renovado" ? Color.Green : Color.LightGray;
        }

        public Color Is_Started_State_BorderColor
        {
            //get{if (Estado_Actual == "Iniciado") return Color.Black;else return Color.LightGray;}
            get => Estado_Actual == "Iniciado" ? Color.Black : Color.LightGray;
        }
        public bool Is_Available_State
        {
            //get{if (Estado_Actual == "Disponible") return true;else return false;}
            get => Estado_Actual == "Disponible" ? true : false;
        }
        public bool Is_Deleted_State
        {
            //get{if (Estado_Actual == "Borrado") return true;else return false;}
            get => Estado_Actual == "Borrado" ? true : false;
        }
        public Color Is_Deleted_State_BorderColor
        {
            //get{if (Estado_Actual == "Borrado") return Color.Black;else return Color.LightGray;}
            get => Estado_Actual == "Borrado" ? Color.Black : Color.LightGray;
        }
        public Color Is_Renovated_State_BorderColor
        {
            //get{
            //    if (Estado_Actual == "Renovado") return Color.Black;
            //    else if (Estado_Actual == "Disponible") return Color.Black;
            //    else return Color.LightGray;
            //}
            get => Estado_Actual == "Renovado" ? Color.Black : Estado_Actual == "Disponible" ? Color.Black : Color.LightGray;
        }
        #endregion


        #region Commands
        public DelegateCommand IniciarCommand { private set; get; }
        public DelegateCommand FinalizarCommand { private set; get; }
        public DelegateCommand RenovarCommand { private set; get; }
        public DelegateCommand DeleteCommand { private set; get; }
        public DelegateCommand CancelCommand { private set; get; }
        public DelegateCommand SaveCommand { private set; get; }
        #endregion


        #region Constructor
        public Details_ViewModel(INavigationService navigationService) : base(navigationService)
        {
            //Title = "Detalles de \"" + Parameters.EditingNote.Name + "\"";
            Title = "Detalles";
            _navigationService = navigationService;
            _messageService = DependencyService.Get<Services.IMessageService>();

            IniciarCommand = new DelegateCommand(Execute_Iniciar);
            FinalizarCommand = new DelegateCommand(Execute_Finalizar);
            RenovarCommand = new DelegateCommand(Execute_Renovar);
            DeleteCommand = new DelegateCommand(Execute_Delete);
            CancelCommand = new DelegateCommand(Execute_Return);
            SaveCommand = new DelegateCommand(Execute_Save);

            Name = Parameters.EditingNote.Name;
            Description = Parameters.EditingNote.Description;
            //IsAvailable = Parameters.EditingNote.IsAvailable;
            DateCreation = Parameters.EditingNote.DateCreation;
            DateModification = Parameters.EditingNote.DateModification;
            DateStarting = Parameters.EditingNote.DateStarting;
            DateFinish = Parameters.EditingNote.DateFinish;
            DateDeleted = Parameters.EditingNote.DateDeleted;
            Estado_Actual = Parameters.EditingNote.Estado_Actual;

            Tipo = Tipo;
            Parameters.EditingNote.IsSelected = false;
        }
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
        }
        #endregion


        #region CommandsFunctions
        async void Execute_Iniciar()
        {
            // hay nota, y no esta ya iniciada
            if (Parameters.EditingNote != null && Parameters.EditingNote.Estado_Actual == "Disponible")
            {
                Parameters.EditingNote.Estado_Actual = "Iniciado";
                Parameters.EditingNote.DateStarting = DateTime.UtcNow.ToString("dd/MM/yyyy - HH:mm");
                Console.WriteLine(Parameters.EditingNote.DateStarting);

                await App.Database.SaveNoteAsync(Parameters.EditingNote);
                await _navigationService.NavigateAsync("MainPage");
            }
            else
                await _messageService.ShowAsync("Para Iniciar, debe estar en el estado Disponible");
        }

        async void Execute_Finalizar()
        {
            // hay nota, y no esta ya finalizada
            if (Parameters.EditingNote != null && Parameters.EditingNote.Estado_Actual == "Iniciado")
            {
                Console.WriteLine(" FINALIZADO");
                //IsAvailableProperty = true;
                Parameters.EditingNote.Estado_Actual = "Finalizado";
                Parameters.EditingNote.DateFinish = DateTime.UtcNow.ToString("dd/MM/yyyy - HH:mm");

                await App.Database.SaveNoteAsync(Parameters.EditingNote);
                await _navigationService.NavigateAsync("MainPage");
            }
            else
                await _messageService.ShowAsync("Para Finalizar, debe estar en el estado Iniciado");
        }

        async void Execute_Renovar()
        {
            if (Parameters.EditingNote.Estado_Actual == "Borrado")
            {
                if (!string.IsNullOrWhiteSpace(Parameters.EditingNote.Name) && !string.IsNullOrWhiteSpace(Parameters.EditingNote.Description))
                {
                    try
                    {
                        Parameters.EditingNote.DateDeleted = "Renovado " + DateTime.UtcNow;
                        Parameters.EditingNote.Estado_Actual = "Renovado";
                        await App.Database.SaveNoteAsync(Parameters.EditingNote);
                        await _navigationService.NavigateAsync("MainPage");
                    }
                    catch (Exception ex)
                    {
                        await _messageService.ShowAsync("Error: " + ex.Message);
                    }
                }
                else
                    await _messageService.ShowAsync("Renovar: Rellene Titulo y Descripcion");
            }
            else
                await _messageService.ShowAsync("Para Renovar, debe estar en el estado Borrado");

        }

        async void Execute_Delete()
        {
            if (Parameters.EditingNote.Estado_Actual == "Finalizado" || Parameters.EditingNote.Estado_Actual == "Disponible" )
            {
                Parameters.EditingNote.Estado_Actual = "Borrado";
                Parameters.EditingNote.DateDeleted = DateTime.UtcNow.ToString("dd/MM/yyyy - HH:mm");

                if (!string.IsNullOrWhiteSpace(Parameters.EditingNote.Name) && !string.IsNullOrWhiteSpace(Parameters.EditingNote.Description))
                {
                    try {
                        await App.Database.SaveNoteAsync(Parameters.EditingNote);
                        await _navigationService.NavigateAsync("MainPage");
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                    await _messageService.ShowAsync("Borrar: Titulo o Descripcion vacios");
            }
            else
                await _messageService.ShowAsync("Para borrar, debe estar en estado Disponible o Finalizado");
        }

        async void Execute_Return()
        {
            await _navigationService.NavigateAsync("MainPage");
        }

        async void Execute_Save()
        {
            if (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Description))
            {
                try
                {
                    Parameters.EditingNote.Name = Name;
                    Parameters.EditingNote.Description = Description;
                    Parameters.EditingNote.DateModification = DateTime.UtcNow.ToString("dd/MM/yyyy - HH:mm");
                    Parameters.EditingNote.IsSelected = false; //ConstructorLista=> Notes.ForEach(note => note.IsSelected = false);

                    await App.Database.SaveNoteAsync(Parameters.EditingNote);
                    await _navigationService.NavigateAsync("MainPage");
                }
                catch (Exception ex)
                {
                    await _messageService.ShowAsync("Error: "+ ex.Message);
                }
            }
            else
                await _messageService.ShowAsync("Guardar: Rellene Titulo y Descripcion");
        }
        #endregion

    }
}
