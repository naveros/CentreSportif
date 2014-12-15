using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentreSportifLib.dto
{
    public class AbonnementDTO
    {
        #region Champs
        private String idabonnement;
        private String idpersonne;
        private String idgroupe;
        private DateTime dateinscription;
        private DateTime datefin;
        private decimal prix;
        #endregion
        #region Propriete
        public String IdAbonnement
        {
            set
            {
                this.idabonnement = value;
            }
            get
            {
                return this.idabonnement;
            }
        }
        public String IdPersonne
        {
            set
            {
                this.idpersonne = value;
            }
            get
            {
                return this.idpersonne;
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
        public DateTime DateInscription
        {
            set { this.dateinscription = value; }
            get { return this.dateinscription; }
        }
        public DateTime DateFin
        {
            set { this.datefin = value; }
            get { return this.datefin; }
        }

        public decimal Prix
        {
            set
            {
                this.prix = value;
            }
            get
            {
                return this.prix;
            }
        }

        #endregion
    }
}
