using Gest_Incidencias.Models;
using Gest_Incidencias.Services;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using XFC = Xamarin.Forms.Color;
using Gest_Incidencias.Data;
using Syncfusion.Pdf.Grid;
using Syncfusion.Pdf;
using System.IO;
using Syncfusion.Drawing;
using ImTools;
using System.ComponentModel.Design;

//using Syncfusion.Pdf;
//using Syncfusion.Pdf.Parsing;
//using Syncfusion.Pdf.Graphics;
//using Syncfusion.Pdf.Grid;

namespace Gest_Incidencias.ViewModels
{
    public class List_ViewModel : BaseViewModel
    {
        #region Variables
        private readonly IMessageService _messageService;
        private readonly INavigationService _navigationService;
        private int Contador_seleccion { get; set; } = 0;
        public string Tipo { get; set; }
        #endregion


        #region Properties
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

        private XFC _is_Unique_Selected_BorderColor = XFC.LightGray;
        public XFC Is_Unique_Selected_BorderColor
        {
            get => _is_Unique_Selected_BorderColor;
            set => SetProperty(ref _is_Unique_Selected_BorderColor, value);
        }
        private int _cantidad_Items;
        public int Cantidad_Items {
            get => _cantidad_Items;
            set => SetProperty(ref _cantidad_Items, value);
        }
        /*private bool _selectedItemColor;
        public bool SelectedItemColor
        {
            get => _selectedItemColor;
            set => SetProperty(ref _selectedItemColor, value);
        }*/
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
        public List_ViewModel(INavigationService navigationService) : base(navigationService)
        { }

        public List_ViewModel(INavigationService navigationService, string tipo/*, int contador*/) : base(navigationService)
        {
            Title = "Listado de " + tipo;
            this.Tipo = tipo;
            _messageService = DependencyService.Get<Services.IMessageService>();
            _navigationService = navigationService; //Navigation = navigation;

            Command_CheckedChanged = new Command<CheckedChangedEventArgs>(Execute_OnCheckedChanged);
            Command_GetReports = new DelegateCommand(Execute_GetReports);
            Command_DeleteItem = new DelegateCommand(Execute_DeleteItem);
            Command_ModifyItem = new DelegateCommand(Execute_ModifyItem);
            Command_FinishItem = new DelegateCommand(Execute_FinishItem);
            Command_CreateItem = new DelegateCommand(Execute_CreateItem);
            Load_Data();
        }
        #endregion


        #region Load_Data() + Initialize()
        async void Load_Data()
        {
            Notes = new ObservableCollection<Note>(await App.Database.GetNotesAsync(Tipo));

            // En caso Disponible, se añaden tambien del tipo Renovado
            ObservableCollection<Note> list2;
            if (Tipo == "Disponible")
            {
                list2 = new ObservableCollection<Note>(await App.Database.GetNotesAsync("Renovado"));
                
                for(int i=0; i < list2.Count(); i++)
                {
                    Notes.Add(list2[i]);
                }
            }
            Cantidad_Items = Notes.Count();
            Parameters.EditingNote = null;
        }
        public override void Initialize(INavigationParameters parameters)
        {
            //Console.WriteLine(" INITIALIZE Page_List_Incidencias params: " + parameters);
            base.Initialize(parameters);
        }
        #endregion


        #region Execute_CreateItem
        async void Execute_CreateItem()
        {
            await _navigationService.NavigateAsync("Creation");
        }
        #endregion


        #region Execute_CreateItem
        async void Execute_OnCheckedChanged(CheckedChangedEventArgs arg)
        {
            Console.WriteLine(" ");

            // Contador de Checks Marcados
            if (arg.Value == true){
                Contador_seleccion++;
            }
            else if (arg.Value == false)
            {
                if(Contador_seleccion != 0) // No restar, para casos de acceder a los detalles, al volver atras se ponia en -1
                    Contador_seleccion--;
            }
            Console.WriteLine(" CONTADOR:"+Contador_seleccion);

            try {
                if (Notes == null)
                    Console.WriteLine("Vuelta a la vista List_ViewModel. Notes null, fallaria Notes.Count()");
                else
                    for (int i = 0; i < Notes.Count(); i++)
                    {
                        // Notas Seleccionadas:
                        if (Notes[i].IsSelected)
                        {
                            Parameters.EditingNote = Notes[i];
                            Console.WriteLine("( si ) " + Notes[i].Name);
                        }
                        else
                            Console.WriteLine("( no ) " + Notes[i].Name);
                    }
            }catch(Exception ex)
            {
                Console.WriteLine(" ");
                Console.WriteLine("Excepcion-Execute_OnCheckedChanged:" + ex);
            }

            // Activacion Botones
            switch (Contador_seleccion)
            {
                case 1:
                    Is_Unique_Selected = true;
                    Is_Unique_Selected_BorderColor = XFC.DarkBlue;

                    Console.WriteLine(" MI NOTA: " + Parameters.EditingNote.Name);
                    break;
                default:
                    Is_Unique_Selected = false;
                    Is_Unique_Selected_BorderColor = XFC.LightGray;
                    Parameters.EditingNote = null;
                    break;
            }
            Console.WriteLine(" ");

            // Seleccion del Primero
            //if (Notes.Any(x => x.IsSelected)) {
            //    notaSeleccionada = Notes.First(x => x.IsSelected);
            //    int n = Notes.IndexOf(x => x.IsSelected);
            //    Console.WriteLine("nota: "+Notes[n].Name);
            //}
        }
        #endregion


        #region Execute_DeleteItem
        async void Execute_DeleteItem()
        {
            if (Parameters.EditingNote == null) {
                await _messageService.ShowAsync("ELIMINAR: Elige un elemento");
            }
            else
            {
                Contador_seleccion = 0;
                //if (Parameters.EditingNote.Estado_Actual != "Iniciado" || Parameters.EditingNote.Estado_Actual != "Borrado")
                if (Parameters.EditingNote.Estado_Actual == "Finalizado"
                    || Parameters.EditingNote.Estado_Actual == "Renovado"
                    || Parameters.EditingNote.Estado_Actual == "Disponible" )
                {

                    Parameters.EditingNote.IsSelected = false;
                    Parameters.EditingNote.Estado_Actual = "Borrado";
                    Parameters.EditingNote.DateDeleted = DateTime.UtcNow.ToString("dd/MM/yyyy - HH:mm");

                    await App.Database.SaveNoteAsync(Parameters.EditingNote);
                    await _navigationService.NavigateAsync("MainPage");
                    await _messageService.ShowAsync("BORRADO " + Parameters.EditingNote.Name);
                }
                else
                {
                    await _messageService.ShowAsync("Estado Actual: Iniciado/Borrado. No se puede borrar.");
                }
            }
        }
        #endregion


        #region Execute_ModifyItem
        async void Execute_ModifyItem()
        {
            if (Parameters.EditingNote == null)
            {
                await _messageService.ShowAsync("MODIFICAR: Elige un elemento");
            }
            else
            {
                Contador_seleccion = 0;
                Parameters.EditingNote.IsSelected = false;
                await _navigationService.NavigateAsync("Details");
            }
        }
        #endregion


        #region Execute_FinishItem
        async void Execute_FinishItem()
        {
            if (Parameters.EditingNote == null)
            {
                await _messageService.ShowAsync("No hay nada seleccionado");
            }
            else
            {
                Contador_seleccion = 0;
                Parameters.EditingNote.IsSelected = false;

                if (Parameters.EditingNote.Estado_Actual == "Iniciado")
                {
                    //Parameters.EditingNote.IsFinished = true;
                    Parameters.EditingNote.Estado_Actual = "Finalizado";
                    Parameters.EditingNote.DateFinish = DateTime.UtcNow.ToString("dd/MM/yyyy - HH:mm");

                    await App.Database.SaveNoteAsync(Parameters.EditingNote);
                    await _messageService.ShowAsync("FINALIZADO");
                    await _navigationService.NavigateAsync("MainPage");
                }
                else if (Parameters.EditingNote.Estado_Actual == "Disponible"
                    || Parameters.EditingNote.Estado_Actual == "Renovado")
                {
                    //Parameters.EditingNote.IsFinished = false;
                    //Parameters.EditingNote.InProgress = true;
                    Parameters.EditingNote.Estado_Actual = "Iniciado";
                    Parameters.EditingNote.DateStarting = DateTime.UtcNow.ToString("dd/MM/yyyy - HH:mm");

                    await App.Database.SaveNoteAsync(Parameters.EditingNote);
                    await _messageService.ShowAsync("INICIADO");
                    await _navigationService.NavigateAsync("MainPage");
                }
                else
                {
                    await _messageService.ShowAsync("Solo se pueden Iniciar/Finalizar estados Disponibles, Iniciados y Renovados. No se puede hacer ésto con Finalizados/Borrados");
                }

            }

        }
        #endregion


        #region Execute_GetReports()
        /*
         * busqueda basada: https://help.syncfusion.com/file-formats/pdf/create-pdf-file-in-xamarin
         * ejemplo funcional: https://github.com/SyncfusionExamples/PDF-Examples/tree/master/Getting%20Started/Xamarin/CreatePDFwithTable
         * NuGet: Syncfusion.Xamarin.Pdf
         * 
         */
        async void Execute_GetReports()
        {
            //void OnButtonClicked(object sender, EventArgs args)
            //Create a new PDF document.
            PdfDocument document = new PdfDocument();

            //Add a page.
            PdfPage page = document.Pages.Add();

            //Create a PdfGrid. - https://help.syncfusion.com/cr/xamarin/Syncfusion.Pdf.Grid.PdfGrid.html
            PdfGrid pdfGrid = new PdfGrid();

            //Add values to list
            List<object> data = new List<object>();
            //Object row1 = new { ID = "E01", Name = "Clay" };
            //Object row2 = new { ID = "E02", Name = "Thomas" };
            ////Add rows to datatable. 
            //data.Add(row1);
            //data.Add(row2);

            for (int i = 0; i < Notes.Count(); i++)
            {
                //if (Notes[i].IsSelected){
                    data.Add(Notes[i]);
                //}
            }

            //Add list to IEnumerable.
            IEnumerable<object> dataTable = data;

            //Assign data source.
            pdfGrid.DataSource = dataTable;

            //Draw grid to the page of PDF document.
            pdfGrid.Draw(page, new PointF(10, 10));

            //Save the PDF document to stream.
            MemoryStream stream = new MemoryStream();
            document.Save(stream);

            //Close the document.
            document.Close(true);

            //Save the stream as a file in the device and invoke it for viewing
            await Xamarin.Forms.DependencyService.Get<ISave>().SaveAndView("Gest_Incidencias_Listado.pdf", "application/pdf", stream);
        }
        #endregion

    }

}
