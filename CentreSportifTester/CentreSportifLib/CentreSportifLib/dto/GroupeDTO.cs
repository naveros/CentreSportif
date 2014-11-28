using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentreSportifLib.dto
{
    public class GroupeDTO
    {
        #region Champs
        private String idgroupe;
        private String idactivite;
        private String numerogroupe;
        #endregion
        #region Propriete
        public String IdGroupe { 
            set 
            {
                this.idgroupe = value; 
            } 
            get 
            {
                return this.idgroupe; 
            } 
        }
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
        public String NumeroGroupe 
        { 
            set 
            {
                this.numerogroupe = value;
            }
            get 
            {
                return this.numerogroupe;
            }
        }
        #endregion
    }
}
