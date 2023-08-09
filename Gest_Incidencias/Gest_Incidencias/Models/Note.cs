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
        #region PrimaryKey
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        #endregion

        #region Atributos
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }
        public string DateModification { get; set; }
        public string DateStarting { get; set; }
        public string DateFinish { get; set; }
        public string DateDeleted { get; set; }

        //revisar si se puede usar desde aqui en lugar de List_Page.xaml/cs
        // Se utiliza en Detalis_Page_ViewModel.cs
        public bool IsSelected { get; set; } = false; // checkbox del listado
        public bool IsAvailable { get; set; } // estado inicial disponible al crear

        //public bool IsChecked { get; set; } = false;

        public string Estado_Actual { get; set; }
        #endregion


        #region Comportamientos_Bindeados
        public string StateElement {
            get {
                if (Estado_Actual == "Disponible") return FontAwesome.FontAwesomeIcons.Check;
                else if (Estado_Actual == "Borrado") return FontAwesome.FontAwesomeIcons.Trash;
                else if (Estado_Actual == "Iniciado") return FontAwesome.FontAwesomeIcons.Play;
                else if (Estado_Actual == "Finalizado") return FontAwesome.FontAwesomeIcons.Flag;
                else if (Estado_Actual == "Renovado") return FontAwesome.FontAwesomeIcons.TrashArrowUp;
                else return null;

                //if (IsAvailable) return FontAwesome.FontAwesomeIcons.Check;
                //else if (IsDeleted) return FontAwesome.FontAwesomeIcons.Trash;
                //else if (InProgress) return FontAwesome.FontAwesomeIcons.Play;
                //else if (IsFinished) return FontAwesome.FontAwesomeIcons.Flag; else retrn null;
            }
        }
        public Color StateColor {
            // Hacerlo en el List_ViewModel.cs=>Execute_OnCheckedChanged

            get
            {
                if (Estado_Actual == "Disponible") return Color.Turquoise;
                else if (Estado_Actual == "Borrado") return Color.PaleVioletRed;
                else if (Estado_Actual == "Iniciado") return Color.Green;
                else if (Estado_Actual == "Finalizado") return Color.DarkGray;
                else if (Estado_Actual == "Renovado") return Color.PaleTurquoise;
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
        #endregion

    }
}

