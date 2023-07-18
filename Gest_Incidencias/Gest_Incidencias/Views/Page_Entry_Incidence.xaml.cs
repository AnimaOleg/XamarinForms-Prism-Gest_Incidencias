
using System;
using System.IO;
using Gest_Incidencias;
using Gest_Incidencias.Models;
using Xamarin.Forms;

namespace Gest_Incidencias.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class Page_Entry_Incidence : ContentPage
    {
        public int ItemId
        {
            set
            {
                LoadNote(value);
            }
        }

        public Page_Entry_Incidence()
        {
            InitializeComponent();
            BindingContext = new Note(); // Set the BindingContext of the page to a new Note.
        }

        /*public Page_Entry_Incidence(Note item)
        {
            //BindingContext = note; // Set the BindingContext of the page to a new Note.

            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Page_Entry_Incidence (NavigationPage.SetBackButtonTitle) ");

            //Title = item.Text;
            Title = "Page_Entry_Incidence";
            this.BindingContext = item; // binding using this 'item' model object and do whatever
        }*/




        async void LoadNote(int itemId)
        {
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
                    try {
                        await App.Database.SaveNoteAsync(note);
                        //await DisplayAlert("DataBase Guardado: ", /*note.ToString() + " " +*/ note.Id + " " + note.Text + " " + note.Description , "OK");
                    }
                    catch (Exception ex) {
                        await DisplayAlert("error DataBase Guardado", ex.Message, "OK");
                    }
                }
            }
            catch (Exception ex) {
                await DisplayAlert("EXCEPCION ", ex.Message, "ok");
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