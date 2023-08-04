using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Gest_Incidencias.Models;
using System;

namespace Gest_Incidencias.Data
{
    public class NoteDatabase
    {
        readonly SQLiteAsyncConnection database;

        public NoteDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Note>().Wait();
        }

        public Task<List<Note>> GetNotesAsync(string tipo)
        {
            if (tipo == "" || tipo == null)
            {

                return database.Table<Note>().ToListAsync();
            }
            else
            {
                //    switch(tipo)
                //    {
                //        case "Todas":
                //            tipo = "All";
                //            break;
                //        case "Disponibles":
                //            tipo = "Enabled";
                //            break;
                //        case "Iniciadas":
                //            tipo = "InProgress";
                //            break;
                //        case "Finalizadas":
                //            tipo = "Finished";
                //            break;
                //        case "Borradas":
                //            tipo = "Deleted";
                //            break;
                //        default: break;
                //        }
            }
            return database.Table<Note>().Where(i => i.Estado_Actual == tipo).ToListAsync();
            //.Where(i => i.Tipo.Equals(tipo)).ToListAsync();
            //.FirstOrDefaultAsync();
            //Where(x => x.drugname.Contains(pillname));
        }

        public Task<Note> GetNoteAsync(int id)
        {
            // Get a specific note.
            return database.Table<Note>()
                            .Where(i => i.Id == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Note note)
        {
            if (note.Id != 0)
            {
                return database.UpdateAsync(note);
            }
            else
            {
                return database.InsertAsync(note);
            }
        }

        /*public Task<int> DeleteNoteAsync(Note note)
        {
            // Delete a note.
            return database.DeleteAsync(note);
        }*/
    }
}