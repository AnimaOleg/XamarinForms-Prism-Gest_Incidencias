using Gest_Incidencias.Models;
using System;
using System.ComponentModel;
//using Gest_Incidencias.ViewModels;
using Xamarin.Forms;

namespace Gest_Incidencias.Views
{
    // [XamlCompilation(XamlCompilationOptions.Compile)] // Al crear la vista, estaba, pero en el proyecto de donde se ha calcado, no estaba
    public partial class Page_Item_Detail : ContentPage
    {
        public Page_Item_Detail()
        {
            InitializeComponent();
            //BindingContext = new Note(); // Set the BindingContext of the page to a new Note.
        }
        // AÑADIDA A MANO
        public Page_Item_Detail(int itemId)
        {
            InitializeComponent();
            LoadNote(itemId);
        }

        public int ItemId
        {
            set { LoadNote(value); }
            //get { return ItemId; } // AÑADIDA A MANO
        }
        async void LoadNote(int itemId)
        {

            //await DisplayAlert("LoadNote:ID: ", itemId.ToString(), "ID");

            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the note and set it as the BindingContext of the page.
                Note note = await App.Database.GetNoteAsync(id);
                BindingContext = note;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load note.");
            }
        }
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            note.Date = DateTime.UtcNow;

            try
            {
                if (!string.IsNullOrWhiteSpace(note.Text))
                {
                    //await DisplayAlert("NOTE ID", note.Id.ToString(), "ID");

                    try { await App.Database.SaveNoteAsync(note); }
                    catch (Exception ex)
                    {
                        await DisplayAlert("App.DataBase Guardado", ex.Message, "fallo guardando");
                    }
                }
            }
            catch (Exception ee) {
                await DisplayAlert("ASD", ee.Message, "ok");
            }

            await Navigation.PushAsync(new Page_List_Incidencias());
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            await App.Database.DeleteNoteAsync(note);

            await Navigation.PushAsync(new Page_List_Incidencias());
        }
    }
}