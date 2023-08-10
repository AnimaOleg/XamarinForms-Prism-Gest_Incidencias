using System;
using SQLite;
using System.Collections.Generic;
using System.Text;
//using System.Drawing;
using System.Globalization;
using Xamarin.Forms;

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

        [Ignore]
        public bool IsSelected { get; set; } = false; // checkbox del listado

        public string Estado_Actual { get; set; }
        #endregion


        #region Comportamientos_Bindeados
        [Ignore]
        public string StateElement {
            get {
                if (Estado_Actual == "Disponible") return FontAwesome.FontAwesomeIcons.Check;
                else if (Estado_Actual == "Borrado") return FontAwesome.FontAwesomeIcons.Trash;
                else if (Estado_Actual == "Iniciado") return FontAwesome.FontAwesomeIcons.Play;
                else if (Estado_Actual == "Finalizado") return FontAwesome.FontAwesomeIcons.Flag;
                else if (Estado_Actual == "Renovado") return FontAwesome.FontAwesomeIcons.TrashArrowUp;
                else return null;
            }
        }
        [Ignore]
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


        private Color _color_Selected;
        [Ignore]
        public Color Color_Selected
        {
            get => _color_Selected;
            set => _color_Selected = value;
        }
        #endregion

    }
}

