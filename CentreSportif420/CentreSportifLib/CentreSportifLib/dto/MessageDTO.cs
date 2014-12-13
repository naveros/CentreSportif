using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentreSportifLib.dto
{
    public class MessageDTO
    {
        #region Champs
        private String idmessage;
        private String idpersonne;
        private String contenu;
        private DateTime datecreation;
        #endregion
        #region Propriete;

        public String IdMessage
        {
            set { this.idmessage = value; }
            get { return this.idmessage; }
        }

        public String IdPersonne
        {
            set { this.idpersonne = value; }
            get { return this.idpersonne; }
        }
        public String Contenu
        {
            set { this.contenu = value; }
            get { return this.contenu; }
        }
        public DateTime DateCreation
        {
            set { this.datecreation = value; }
            get { return this.datecreation; }
        }
        public override String ToString(){

            return this.datecreation.ToShortDateString() + "  " + this.contenu;
        }
        #endregion
    }
}
