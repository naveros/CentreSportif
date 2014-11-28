using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentreSportifLib.dto
{
    public class PresenceDTO
    {
        #region Champs
        private String idpresence;
        private String idpersonne;
        private String idseance;
        private Boolean present;
        #endregion
        #region Propriete
        public String IdPresence { set { this.idpresence = value; } get { return this.idpresence; } }
        public String IdPersonne{ set { this.idpersonne = value; } get { return this.idpersonne;; } }
        public String IdSeance { set { this.idseance = value; } get { return this.idseance; } }
        public Boolean Present { set { this.present = value; } get { return this.present; } }
        #endregion
    }
}
