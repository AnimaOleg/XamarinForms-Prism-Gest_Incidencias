using Gest_Incidencias.Models;
using Gest_Incidencias.Views;
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
    public class Page_List_IncidenciasViewModel : BaseViewModel /*BindableBase*/
    {
        #region Variables
        int contador_notas_seleccionadas = 0;

        private /*readonly*/ Services.IMessageService _messageService;
        private /*readonly*/ INavigationService _navigationService; /*=> NavigationService;*/ // AÑADIDO A MANO; //public INavigation Navigation { get; set; }
        #endregion


        #region Properties
        public string Tipo { get; set; }

        private ObservableCollection<Note> _notes;
        public ObservableCollection<Note> Notes {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }
        private bool _isAvailableProperty = false;
        public bool IsAvailableProperty
        {
            get => _isAvailableProperty;
            set => SetProperty(ref _isAvailableProperty, value);
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
        //public DelegateCommand CommandGoToDetailsCommand { private set; get; }

        private DelegateCommand _navigationCommand;
        public DelegateCommand NavigateCommand =>
            _navigationCommand ?? (_navigationCommand = new DelegateCommand(ExecuteNavigationCommand));


        //private DelegateCommand _createItemCommand;
        //public DelegateCommand CreateItemCommand =>
        //    _createItemCommand ?? (_createItemCommand = new DelegateCommand(CreateItem));

        #endregion


        #region Constructor
        public Page_List_IncidenciasViewModel(INavigationService navigationService) : base(navigationService)
        { }

        public Page_List_IncidenciasViewModel(INavigationService navigationService, string tipo) : base(navigationService)
        { // DEBUGEAR
            
            // Variables
            this.Tipo = tipo;
            Title = "Page_List_IncidenciasViewModel 2";
            _messageService = DependencyService.Get<Services.IMessageService>();
            _navigationService = navigationService; //Navigation = navigation;
            Console.WriteLine(" MODEL 1 Page_List_IncidenciasViewModel, TIPO: " + tipo);

            /// Commands
            //OnCheckedChangedCommand = new Command<CheckedChangedEventArgs>(OnCheckedChanged);
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
        async void ExecuteNavigationCommand()
        {
            await _navigationService.NavigateAsync("ViewA");
        }
        async void Execute_CreateItem()
        {
            try
            {
                await _navigationService.NavigateAsync("/Page_Entry_Incidence");
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
                    IsAvailableProperty = true;
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

                            Console.WriteLine("NOTA: " + Notes[i].Title);
                            //Console.WriteLine("COLOR: " + SelectedItemColor);
                            break;
                        }
                    }
                    break;
                default:
                    //Console.WriteLine(" PARAMETERS.NOTA:"+ Parameters.EditingNote.Title);
                    IsAvailableProperty = false;
                    //IsCheckedChanged = false;
                    Parameters.EditingNote = null;
                    break;
            }
            

            // Seleccion del Primero
            //if (Notes.Any(x => x.IsSelected)) {
            //    notaSeleccionada = Notes.First(x => x.IsSelected);
            //    int n = Notes.IndexOf(x => x.IsSelected);
            //    Console.WriteLine("nota: "+Notes[n].Title);
            //}
        }

        async void Execute_GetReports()
        {
            // https://www.google.com/search?q=xamarin+generar+reporte+con+pdf&rlz=1C1GCEU_esES1064ES1064&sxsrf=AB5stBjR1xYSV5FLdhD7ATVklfuycQf8Qg%3A1690541030546&ei=5pvDZPvxIOqrkdUP-IipiAU&ved=0ahUKEwj7_OmMnLGAAxXqVaQEHXhEClEQ4dUDCA8&uact=5&oq=xamarin+generar+reporte+con+pdf&gs_lp=Egxnd3Mtd2l6LXNlcnAiH3hhbWFyaW4gZ2VuZXJhciByZXBvcnRlIGNvbiBwZGYyBRAhGKABMgUQIRigATIFECEYoAFInRBQyQlYtQ9wAXgBkAEAmAHGAaABhweqAQMxLja4AQPIAQD4AQHCAgoQABhHGNYEGLADwgIEECEYFeIDBBgAIEGIBgGQBgg&sclient=gws-wiz-serp
            // https://medium.com/@nekszerlopezespinoza/como-crear-un-pdf-y-compartirlo-xamarin-forms-dd702f7fcf60
            // https://www.youtube.com/watch?v=FqetV1Lh-9c
            //await Navigation.PushAsync(new Page_List_Incidencias());
            //await Navigation.PushAsync(new MainPage());
        }

        async void Execute_DeleteItem()
        {
            if (Parameters.EditingNote == null) {
                await _messageService.ShowAsync(message: "Elige una Incidencia para Eliminar");
            }
            else
            {
                Parameters.EditingNote.IsAvailable = false;
                Parameters.EditingNote.IsSelected = false;
                Parameters.EditingNote.IsDeleted = true;
                Parameters.EditingNote.Tipo = "Borradas";
                Parameters.EditingNote.DateDeleted = DateTime.UtcNow.ToString("dd/MM/yyyy - HH:mm");

                await App.Database.SaveNoteAsync(Parameters.EditingNote);
                await NavigationService.NavigateAsync("MainPage");
            }

            //await Navigation.PushAsync(new Page_List_Incidencias());
        }

        async void Execute_ModifyItem()
        {
            if (Parameters.EditingNote != null) {
                Parameters.EditingNote.IsSelected = false;
                await NavigationService.NavigateAsync("/Page_Item_Detail");
            }
            else {
                await _messageService.ShowAsync(message: "Elige una Incidencia para Modificar");
            }
        }

        async void Execute_FinishItem()
        {
            if (Parameters.EditingNote != null)
            {
                Console.WriteLine("NOTA Finalizar - Finalizada:" + Parameters.EditingNote.Title);

                //Parameters.EditingNote.IsEnabled = false;
                Parameters.EditingNote.IsSelected = false;

                if (Parameters.EditingNote.DateFinish == "")
                {
                    Parameters.EditingNote.IsFinished = true;
                    Parameters.EditingNote.IsAvailable = false;
                    Parameters.EditingNote.Tipo = "Finalizadas";
                    Parameters.EditingNote.DateFinish = DateTime.UtcNow.ToString("dd/MM/yyyy - HH:mm");
                }
                else
                {
                    Parameters.EditingNote.IsFinished = false;
                    Parameters.EditingNote.IsAvailable = true;
                    Parameters.EditingNote.Tipo = "Disponibles";
                    Parameters.EditingNote.DateFinish = "";
                    //VisibleFecha = true
                }
                await App.Database.SaveNoteAsync(Parameters.EditingNote);
                await NavigationService.NavigateAsync("MainPage");
            }
            else
            {
                Console.WriteLine("NOTA Finalizar - Parameters.EditingNote == NULL ");
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
