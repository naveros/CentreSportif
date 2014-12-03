using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentreSportifLib.dto
{
    public class ActiviteDTO
    {
        #region Champs
        private String idactivite;
        private String nom;
        private String duree;
        private String description;
        #endregion
        #region Proprietes
        public String IdActivite
        {
            set
            {
                this.idactivite = value;
            }
            get
            {
                return this.idactivite;
            }
        }
        public String Nom
        {
            set { this.nom = value; }
            get { return this.nom; }
        }
        public String Duree
        {
            set
            {
                this.duree = value;
            }
            get
            {
                return this.duree;
            }
        }
        public String Description
        {
            set
            {
                this.description = value;
            }
            get
            {
                return description;
            }
        }
        #endregion
    }
}
