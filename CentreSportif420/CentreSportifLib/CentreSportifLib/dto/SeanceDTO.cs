using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentreSportifLib.dto
{
    public class SeanceDTO
    {
        #region Champs
        private String idseance;
        private String idgroupe;
        private DateTime datedebut;
        private DateTime datefin;
        #endregion
        #region Propriete
        public String IdSeance
        {
            set
            {
                this.idseance = value;
            }
            get
            {
                return this.idseance;
            }
        }
        public String IdGroupe
        {
            set
            {
                this.idgroupe = value;
            }
            get
            {
                return this.idgroupe;
            }
        }
        public DateTime DateDebut
        {
            set { this.datedebut = value; }
            get { return this.datedebut; }
        }
        public DateTime DateFin
        {
            set { this.datefin = value; }
            get { return this.datefin; }
        }
        #endregion
    }
}
