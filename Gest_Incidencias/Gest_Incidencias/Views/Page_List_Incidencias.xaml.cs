using Gest_Incidencias.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gest_Incidencias.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_List_Incidencias : ContentPage
    {
        Note note_aux; // auxiliar para deseleccionar el item de la lista

        public Page_List_Incidencias()
        {
            InitializeComponent();
            //BindingContext = new NoteVM { Notes = notes };
            //BindingContext = new NoteVM { Notes = listView.ItemsSource }; // FALTA CORREGIR

            // EJEMPLO LISTA ESTATICA
            /*var monkeys = new List<Monkey> {
                new Monkey {Name="Xander", Description="Writer"},
                new Monkey {Name="Rupert", Description="Engineer"},
                new Monkey {Name="Tammy", Description="Designer"},
                new Monkey {Name="Blue", Description="Speaker"},
			};
            BindingContext = new MonkeyVM { Monkeys = monkeys }; */

            // alternative way of setting the Header's binding
            //listView.SetBinding (ListView.HeaderProperty, new Binding("."));
        }


        // REEMPLAZADAAS PARA MYSQLLITE
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            listView.ItemsSource = await App.Database.GetNotesAsync(); // //listView.ItemsSource = new[] { "a", "b", "c" };
        }
        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
                //var mi_item = e.Item as Note;
                if (e.Item != null)
                {
                try
                {
                    /*if (note_aux == ((ListView)sender).SelectedItem) // DESSELECCIONAR
                    {
                        //await DisplayAlert("Seleccionado", "Desseleccionado " + e.Item.ToString(), "OK");
                        ((ListView)sender).SelectedItem = null;
                    }*/
                    if (note_aux == e.Item) {
                        ((ListView)sender).SelectedItem = null;
                        //await DisplayAlert("Seleccionado", "Desseleccionado " + e.Item.ToString(), "OK");
                    }
                    note_aux = (Note)e.Item; // auxiliar del seleccionado, para luego desseleccionarlo
                }
                catch (Exception ee)
                {
                    await DisplayAlert("ERROR, nota elegida no es la nota que estaba marcada anteriormente", ee.Message, "ok");
                }
            }

            if (e.Item == null) return;
        }
        // REEMPLAZADAAS PARA MYSQLLITE
        /*async void OnItemTapped(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the NoteEntryPage, passing the ID as a query parameter.
                Note note = (Note)e.CurrentSelection.FirstOrDefault();
                //await Shell.Current.GoToAsync($"{nameof(NoteEntryPage)}?{nameof(NoteEntryPage.ItemId)}={note.ID.ToString()}");
            }
        }*/


        async void OnReportsClicked(object sender, EventArgs e)
        {
            /*await DisplayAlert("ItemSelected", " Todavia NO IMPLEMENTADO", "Ok");
            var note = (Note)BindingContext;
            await App.Database.DeleteNoteAsync(note);
            await Navigation.PushAsync(new MainPage());*/
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {

            if (note_aux == null) {
                await DisplayAlert("OnDeleteClicked 2", "Debes Elegir", "ok");
            }
            else
            {
                //var note2 = ((ListView)sender).SelectedItem;
                //var note2 = (Note)BindingContext;
                await App.Database.DeleteNoteAsync(note_aux);
                //await App.Database.DeleteNoteAsync(note2);
                await Navigation.PushAsync(new MainPage());
            }

        }

        async void OnModifyClicked(object sender, EventArgs e)
        {
            if (note_aux == null)
            {
                await DisplayAlert("OnDeleteClicked 2", "Debes Elegir", "ok");
            }
            else
            {
                //var note = (Note)BindingContext;
                //await Navigation.PushAsync(new ItemDetailPage())
                //await Navigation.PushAsync(new ItemDetailPage(note_aux.Id));


                //var note = (Note)BindingContext;

                
                //BindingContext = note_aux;

                await Navigation.PushAsync(new Page_Item_Detail(note_aux.Id));



                /*var item = e.Item as Note;
                if (item != null) { await Navigation.PushAsync(new Page_Item_Detail(item)); }*/

                //Note note = note_aux;
                //await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailPage.ItemId)}={note_aux.Id.ToString()}");
            }


                //Page_Item_Detail.timeStamp.OnlineEndTime = DateTime.Now.ToString();
                /*Page_Item_Detail.BindingContextProperty = 
                Page_Item_Detail mainPage = new Page_Item_Detail();
                mainPage.BindingContext = Page_Item_Detail.timestamp;
                await Navigation.PushAsync(mainPage*/
            
        }

        async void OnFinishClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }

        async void OnCreateClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Page_Entry_Incidence());
        }





        /*
        class Monkey
        {
            public string Name { get; set; }
            public string Description { get; set; }
        }
        class MonkeyVM // View Model
        {
            public List<Monkey> Monkeys { get; set; }
            public string Intro { get { return "Monkey Header"; } }
            public string Summary { get { return " There were " + Monkeys.Count + " monkeys"; }
        }
        */

        /*
        public ObservableCollection<string> Items { get; set; }

        public Page_List_Incidencias()
        {
            InitializeComponent();
            listView.ItemsSource = new[] { "a", "b", "c" };
        }


        public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return; // has been set to null, do not 'process' tapped event
            DisplayAlert("Tapped", e.SelectedItem + " row was tapped", "OK");
            ((ListView)sender).SelectedItem = null; // de-select the row
        }

        async void OnCellClicked(object sender, EventArgs e)
        {
            var b = (Button)sender;
            var t = b.CommandParameter;

            await ((ContentPage)((ListView)((ViewCell)((StackLayout)b.Parent).Parent).Parent).Parent).DisplayAlert("Clicked", t + " button was clicked", "OK");
            Debug.WriteLine("clicked" + t);
        }
        */

    }


}
