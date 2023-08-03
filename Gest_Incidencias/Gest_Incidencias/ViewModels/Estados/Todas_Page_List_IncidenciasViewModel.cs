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

namespace Gest_Incidencias.ViewModels.Estados
{
    public class Todas_Page_List_IncidenciasViewModel : BaseViewModel /*BindableBase*/
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
        private string _summary;
        public string Summary {
            get => _summary;
            set => SetProperty(ref _summary, value);

        }
        #endregion


        #region Commands
        public ICommand OnCheckedChangedCommand { private set; get; }
        public DelegateCommand GetReportsItemCommand { private set; get; }
        public DelegateCommand DeleteItemCommand { private set; get; }
        public DelegateCommand ModifyItemCommand { private set; get; }
        public DelegateCommand SetFinishedItemCommand { private set; get; }
        public DelegateCommand CreateItemCommand { private set; get; }
        public DelegateCommand GoToDetailsCommand { private set; get; }

        private DelegateCommand _navigationCommand;
        public DelegateCommand NavigateCommand =>
            _navigationCommand ?? (_navigationCommand = new DelegateCommand(ExecuteNavigationCommand));


        //private DelegateCommand _createItemCommand;
        //public DelegateCommand CreateItemCommand =>
        //    _createItemCommand ?? (_createItemCommand = new DelegateCommand(CreateItem));

        #endregion


        #region Constructor
        public Todas_Page_List_IncidenciasViewModel(INavigationService navigationService) : base(navigationService)
        { }

        public Todas_Page_List_IncidenciasViewModel(INavigationService navigationService, string tipo) : base(navigationService)
        { // DEBUGEAR
            
            // Variables
            this.Tipo = tipo;
            Title = "Page_List_IncidenciasViewModel 2";
            _messageService = DependencyService.Get<Services.IMessageService>();
            _navigationService = navigationService; //Navigation = navigation;
            Console.WriteLine(" MODEL 1 Page_List_IncidenciasViewModel, TIPO: " + tipo);

            /// Commands
            //OnCheckedChangedCommand = new Command<CheckedChangedEventArgs>(OnCheckedChanged);
            OnCheckedChangedCommand = new DelegateCommand<CheckedChangedEventArgs>(OnCheckedChanged);
            DeleteItemCommand = new DelegateCommand(DeleteItem);
            ModifyItemCommand = new DelegateCommand(ModifyItem);
            GetReportsItemCommand = new DelegateCommand(GetReportsItem);
            SetFinishedItemCommand = new DelegateCommand(Finalizar);
            CreateItemCommand = new DelegateCommand(CreateItem);

            Load_Data();
        }

        async void Load_Data()
        {
            Notes = new ObservableCollection<Note>(await App.Database.GetNotesAsync(Tipo)); //Notes.ForEach(note => note.IsSelected = false);
            Summary = Notes.Count() + ""; /* + " " + Tipo*/
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
            Console.WriteLine("Click Boton NAVIGATE TO B");
            try
            {
                await _navigationService.NavigateAsync("ViewA");
            }catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION ExecuteNavigationCommand: " + ex.Message);
            }
        }
        async void CreateItem()
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




        async void OnCheckedChanged(CheckedChangedEventArgs arg)
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

        async void GetReportsItem()
        {
            // https://www.google.com/search?q=xamarin+generar+reporte+con+pdf&rlz=1C1GCEU_esES1064ES1064&sxsrf=AB5stBjR1xYSV5FLdhD7ATVklfuycQf8Qg%3A1690541030546&ei=5pvDZPvxIOqrkdUP-IipiAU&ved=0ahUKEwj7_OmMnLGAAxXqVaQEHXhEClEQ4dUDCA8&uact=5&oq=xamarin+generar+reporte+con+pdf&gs_lp=Egxnd3Mtd2l6LXNlcnAiH3hhbWFyaW4gZ2VuZXJhciByZXBvcnRlIGNvbiBwZGYyBRAhGKABMgUQIRigATIFECEYoAFInRBQyQlYtQ9wAXgBkAEAmAHGAaABhweqAQMxLja4AQPIAQD4AQHCAgoQABhHGNYEGLADwgIEECEYFeIDBBgAIEGIBgGQBgg&sclient=gws-wiz-serp
            // https://medium.com/@nekszerlopezespinoza/como-crear-un-pdf-y-compartirlo-xamarin-forms-dd702f7fcf60
            // https://www.youtube.com/watch?v=FqetV1Lh-9c
            //await Navigation.PushAsync(new Page_List_Incidencias());
            //await Navigation.PushAsync(new MainPage());
        }

        async void DeleteItem()
        {
            if (Parameters.EditingNote == null) {
                await _messageService.ShowAsync(message: "Elige una Incidencia para Eliminar");
            }
            else
            {
                Parameters.EditingNote.IsAvailable = false;
                Parameters.EditingNote.IsSelected = false;
                Parameters.EditingNote.IsDeleted = true;
                Parameters.EditingNote.Tipo = "Deleted";
                Parameters.EditingNote.DateDeleted = DateTime.UtcNow.ToString("dd/MM/yyyy - HH:mm");

                await App.Database.SaveNoteAsync(Parameters.EditingNote);
                await NavigationService.NavigateAsync("MainPage");
            }

            //await Navigation.PushAsync(new Page_List_Incidencias());
        }

        async void ModifyItem()
        {
            if (Parameters.EditingNote != null) {
                Parameters.EditingNote.IsSelected = false;
                await NavigationService.NavigateAsync("Page_Item_Detail");
            }
            else {
                await _messageService.ShowAsync(message: "Elige una Incidencia para Modificar");
            }
        }

        async void Finalizar()
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
                    Parameters.EditingNote.Tipo = "Finished";
                    Parameters.EditingNote.DateFinish = DateTime.UtcNow.ToString("dd/MM/yyyy - HH:mm");
                }
                else
                {
                    Parameters.EditingNote.IsFinished = false;
                    Parameters.EditingNote.IsAvailable = true;
                    Parameters.EditingNote.Tipo = "Enabled";
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
