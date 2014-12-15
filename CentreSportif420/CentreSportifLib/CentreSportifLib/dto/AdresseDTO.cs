using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentreSportifLib.dto
{
    public class AdresseDTO
    {
        #region Champs
        private String idadresse;
        private String idpersonne;
        private String numero;
        private String rue;
        private String codepostal;
        private String ville;
        private String pays;
        #endregion
        #region Propriete
        public String IdAdresse { set { this.idadresse = value; } get { return this.idadresse; } }
        public String IdPersonne { set { this.idpersonne = value; } get { return this.idpersonne; } }
        public String Numero { set { this.numero = value; } get { return this.numero; } }
        public String Rue { set { this.rue = value; } get { return this.rue; } }
        public String CodePostal { set { this.codepostal = value; } get { return this.codepostal; } }
        public String Ville { set { this.ville = value; } get { return this.ville; } }
        public String Pays { set { this.pays = value; } get { return this.pays; } }
        #endregion
        public override string ToString()
        {
            return idadresse + " : " + idpersonne;
        }
    }
}