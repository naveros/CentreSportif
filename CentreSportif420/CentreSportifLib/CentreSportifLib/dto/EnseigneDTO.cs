using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentreSportifLib.dto
{
    public class EnseigneDTO
    {
        #region Champs
        private String idenseigne;
        private String idgroupe;
        private String idpersonne;
        #endregion

        #region Propriete
        public EnseigneDTO() { }

        public String IdEnseigne
        {
            set
            {
                this.idenseigne = value;
            }
            get
            {
                return this.idenseigne;
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
        #endregion
    }
}
