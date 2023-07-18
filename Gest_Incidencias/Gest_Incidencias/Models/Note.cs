using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace Gest_Incidencias.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        //public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

    }
}

