using System;
using SQLite;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Globalization;

namespace Gest_Incidencias.Models
{
    public class Note
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }
        public string DateModification { get; set; }
        public string DateStarting { get; set; }
        public string DateFinish { get; set; }
        public string DateDeleted { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsSelected { get; set; } = false; // checkbox del listado
        public bool IsChecked { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public bool IsFinished { get; set; } = false;
        public bool InProgress { get; set; } = false;
        public string StateElement {
            get {
                if (IsAvailable) return FontAwesome.FontAwesomeIcons.Check;
                else if (IsDeleted) return FontAwesome.FontAwesomeIcons.Trash;
                else if (InProgress) return FontAwesome.FontAwesomeIcons.Play;
                else if (IsFinished) return FontAwesome.FontAwesomeIcons.Flag;
                else return null;
            }
        }
        public Color StateColor {
            get {
                /*if (IsAvailable) return Color.Green;
                else */
                if (IsAvailable) return Color.DarkTurquoise;
                else if (IsDeleted) return Color.Red;
                else if (InProgress) return Color.Green;
                else if (IsFinished) return Color.DarkBlue;
                else return Color.Gray;
            }
        }

        //[Ignore]
        //public Color SelectedItemColor
        //{
        //    get
        //    {
        //        if (!IsSelected) return Color.DarkBlue;
        //        else return Color.Transparent;
        //    }
        //}
        //public float StateSelectedToOpacity{
        //    get{
        //        if (!IsSelected) return float.Parse("1", CultureInfo.InvariantCulture);
        //        else return float.Parse("0,5", CultureInfo.InvariantCulture);
        //    }
        //}
        public string Tipo { get; set; }
    }
}

