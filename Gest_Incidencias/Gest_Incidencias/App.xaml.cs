using Gest_Incidencias.Data;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gest_Incidencias
{
    public partial class App : Application
    {
        static NoteDatabase database;
        
        public static NoteDatabase Database
        {
            get {
                if (database == null)
                    database = new NoteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "IncidencesAicom.db3"));
                return database;
            }
        }
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }
        protected override void OnStart() { }
        protected override void OnSleep() { }
        protected override void OnResume() { }
    }
}
