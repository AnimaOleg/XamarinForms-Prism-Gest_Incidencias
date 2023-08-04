using Gest_Incidencias.Models;
using Gest_Incidencias.Views;
using Gest_Incidencias.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Gest_Incidencias.ViewModels
{
    public class List_Page_ViewModel : BaseViewModel /*BindableBase*/
    {
        #region Variables
        int contador_notas_seleccionadas = 0;

        private /*readonly*/ IMessageService _messageService;
        private /*readonly*/ INavigationService _navigationService; /*=> NavigationService;*/ // AÑADIDO A MANO; //public INavigation Navigation { get; set; }
        #endregion


        #region Properties
        public string Tipo { get; set; }

        private ObservableCollection<Note> _notes;
        public ObservableCollection<Note> Notes {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }

        private bool _is_Unique_Selected = false;
        public bool Is_Unique_Selected
        {
            get => _is_Unique_Selected;
            set => SetProperty(ref _is_Unique_Selected, value);
        }


        /*private bool _selectedItemColor;
        public bool SelectedItemColor
        {
            get => _selectedItemColor;
            set => SetProperty(ref _selectedItemColor, value);
        }*/
        private int _cantidad_Items;
        public int Cantidad_Items {
            get => _cantidad_Items;
            set => SetProperty(ref _cantidad_Items, value);

        }
        #endregion


        #region Commands
        public Command Command_CheckedChanged { private set; get; }
        public DelegateCommand Command_GetReports { private set; get; }
        public DelegateCommand Command_DeleteItem { private set; get; }
        public DelegateCommand Command_ModifyItem { private set; get; }
        public DelegateCommand Command_FinishItem { private set; get; }
        public DelegateCommand Command_CreateItem { private set; get; }
        #endregion


        #region Constructor
        public List_Page_ViewModel(INavigationService navigationService) : base(navigationService)
        { }

        public List_Page_ViewModel(INavigationService navigationService, string tipo) : base(navigationService)
        {
            
            // Variables
            this.Tipo = tipo;
            //Title = "Listado";
            _messageService = DependencyService.Get<Services.IMessageService>();
            _navigationService = navigationService; //Navigation = navigation;
            Console.WriteLine(" MODEL 1 Page_List_Incidencias_ViewModel, TIPO: " + tipo);

            /// Commands
            Command_CheckedChanged = new Command<CheckedChangedEventArgs>(Execute_OnCheckedChanged);
            Command_GetReports = new DelegateCommand(Execute_GetReports);
            Command_DeleteItem = new DelegateCommand(Execute_DeleteItem);
            Command_ModifyItem = new DelegateCommand(Execute_ModifyItem);
            Command_FinishItem = new DelegateCommand(Execute_FinishItem);
            Command_CreateItem = new DelegateCommand(Execute_CreateItem);

            Load_Data();
        }

        async void Load_Data()
        {
            Notes = new ObservableCollection<Note>(await App.Database.GetNotesAsync(Tipo)); //Notes.ForEach(note => note.IsSelected = false);
            Cantidad_Items = Notes.Count();
        }
        public override void Initialize(INavigationParameters parameters)
        {
            //Console.WriteLine(" INITIALIZE Page_List_Incidencias params: " + parameters);
            base.Initialize(parameters);
        }

        #endregion


        #region CommandsFunctions
        async void Execute_CreateItem()
        {
            try
            {
                await _navigationService.NavigateAsync("Page_Entry_Incidence");
            }
            catch (Exception ex)
            {
                Console.WriteLine(" EXCEPTION CreateItem: " + ex);
            }
            //await _navigationService.NavigateAsync("Page_Entry_Incidence");
            //await Navigation.PushAsync(new Page_Entry_Incidence());
        }

        async void Execute_OnCheckedChanged(CheckedChangedEventArgs arg)
        {
            // Contador de Checks Marcados
            if (arg.Value == true)
                contador_notas_seleccionadas++;
            else if (arg.Value == false)
                contador_notas_seleccionadas--;
            Console.WriteLine("contador:" + contador_notas_seleccionadas);

            // Activacion Botones
            switch (contador_notas_seleccionadas)
            {
                case 1:
                    Is_Unique_Selected = true;
                    //IsCheckedChanged = true;
                    // Seleccion de 1 unica nota
                    for (int i = 0; i < Notes.Count(); i++)
                    {
                        // Hay Nota Seleccionada
                        if (Notes[i].IsSelected)
                        {
                            Parameters.EditingNote = Notes[i];
                            Parameters.EditingNote.IsSelected = false;
                            //Parameters.EditingNote.IsSelected2 = true;

                            Console.WriteLine("NOTA: " + Notes[i].Name);
                            //Console.WriteLine("COLOR: " + SelectedItemColor);
                            break;
                        }
                    }
                    break;
                default:
                    //Console.WriteLine(" PARAMETERS.NOTA:"+ Parameters.EditingNote.Name);
                    Is_Unique_Selected = false;
                    //IsCheckedChanged = false;
                    Parameters.EditingNote = null;
                    break;
            }
            

            // Seleccion del Primero
            //if (Notes.Any(x => x.IsSelected)) {
            //    notaSeleccionada = Notes.First(x => x.IsSelected);
            //    int n = Notes.IndexOf(x => x.IsSelected);
            //    Console.WriteLine("nota: "+Notes[n].Name);
            //}
        }

        async void Execute_GetReports()
        {
            //await _navigationService.NavigateAsync("Page_Reports");
        }

        async void Execute_DeleteItem()
        {
            if (Parameters.EditingNote == null) {
                await _messageService.ShowAsync(message: "Elige una Incidencia para Eliminar");
            }
            else
            {
                Parameters.EditingNote.IsSelected = false;
                //Parameters.EditingNote.IsAvailable = false;
                //Parameters.EditingNote.IsDeleted = true;
                Parameters.EditingNote.Estado_Actual = "Borrado";
                Parameters.EditingNote.DateDeleted = DateTime.UtcNow.ToString("dd/MM/yyyy - HH:mm");

                await App.Database.SaveNoteAsync(Parameters.EditingNote);
                await _navigationService.NavigateAsync("MainPage");
            }
        }

        async void Execute_ModifyItem()
        {
            if (Parameters.EditingNote != null) {
                Parameters.EditingNote.IsSelected = false;
                await _navigationService.NavigateAsync("Details_Page");
            }
            else {
                await _messageService.ShowAsync(message: "Elige una Incidencia para Modificar");
            }
        }

        async void Execute_FinishItem()
        {
            if (Parameters.EditingNote != null)
            {

                Parameters.EditingNote.IsSelected = false;
                //Parameters.EditingNote.IsAvailable = false;

                if (Parameters.EditingNote.Estado_Actual == "Disponible"
                    &&Parameters.EditingNote.Estado_Actual == "Iniciado" )
                {
                    Console.WriteLine("NOTA Finalizar - Finalizada:" + Parameters.EditingNote.Name);
                    //Parameters.EditingNote.IsFinished = true;
                    Parameters.EditingNote.Estado_Actual = "Finalizado";
                    Parameters.EditingNote.DateFinish = DateTime.UtcNow.ToString("dd/MM/yyyy - HH:mm");
                }
                else if(Parameters.EditingNote.Estado_Actual == "Disponible")
                {
                    Console.WriteLine("NOTA Finalizar - Renovada:" + Parameters.EditingNote.Name);
                    //Parameters.EditingNote.IsFinished = false;
                    //Parameters.EditingNote.InProgress = true;
                    Parameters.EditingNote.Estado_Actual = "Iniciado";
                    Parameters.EditingNote.DateStarting = DateTime.UtcNow.ToString("dd/MM/yyyy - HH:mm");
                }
                await App.Database.SaveNoteAsync(Parameters.EditingNote);
                await _navigationService.NavigateAsync("MainPage");
            }
            else
            {
                await _messageService.ShowAsync("NOTA Finalizar - no hay nada a cambiar de estado");
            }

        }
        #endregion

    }




    // EJEMEPLO color a hexadecimal
    //string color = HexConverter(Color.Red);
    //Console.WriteLine("Color:"+color);
    /*
    private static String HexConverter(System.Drawing.Color c)
    {
        return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
    }
    private static String RGBConverter(System.Drawing.Color c)
    {
        return "RGB(" + c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString() + ")";
    }
    */

}
