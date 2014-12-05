using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentreSportifLib.dto
{
    public class PaiementDTO
    {
        #region Champs

        private String idpersonne;
        private DateTime date;
        private decimal montant;
        private String mode;

        #endregion
        #region Propriete
        public String IdPersonne
        {
            set { this.idpersonne = value; }
            get { return this.idpersonne; }
        }
        public DateTime Date
        {
            set { this.date = value; }
            get { return this.date; }
        }
        public decimal Montant
        {
            set { this.montant = value; }
            get { return this.montant; }
        }
        public String Mode
        {
            set { this.mode = value; }
            get { return this.mode; }
        }

        #endregion
    }
}