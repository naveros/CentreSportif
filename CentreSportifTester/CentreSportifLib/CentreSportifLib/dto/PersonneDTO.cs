using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentreSportifLib.dto
{
    public class PersonneDTO
    {
        #region Champs
        private String idpersonne;
        private String prenom;
        private String nom;
        private char sexe;
        private DateTime datenaissance;
        private String email;
        private String motdepasse;
        private String codebarre;
        private String role;
        AdresseDTO adresse;
        List<SeanceDTO> seances;
        List<PresenceDTO> presences;
        #endregion
        #region Proprietés
        public String IdPersonne
        {
            set { this.idpersonne = value; }
            get { return this.idpersonne; }
        }
        public String Prenom
        {
            set { this.prenom = value; }
            get { return this.prenom; }
        }
        public String Nom
        {
            set { this.nom = value; }
            get { return this.nom; }
        }
        public Char Sexe
        {
            set { this.sexe = value; }
            get { return this.sexe; }
        }
        public DateTime DateNaissance
        {
            set { this.datenaissance = value; }
            get { return this.datenaissance; }
        }
        public String Email
        {
            set { this.email = value; }
            get { return this.email; }
        }
        public String MotDePasse
        {
            set { this.motdepasse = value; }
            get { return this.motdepasse; }
        }
        public String CodeBarre
        {
            set { this.codebarre = value; }
            get { return this.codebarre; }
        }
        public String Role
        {
            set { this.role = value; }
            get { return this.role; }
        }
        public AdresseDTO Adresse
        {
            set { this.adresse = value; }
            get { return this.adresse; }
        }
        public List<SeanceDTO> Seances
        {
            set { this.seances = value; }
            get { return this.seances; }
        }
        public List<PresenceDTO> Presences
        {
            set { this.presences = value; }
            get { return this.presences; }
        }
        #endregion
        #region Methodes
        public PersonneDTO()
        { }
        public override string ToString()
        {
            String result = "{";
            result += "Id:" + IdPersonne;
            result += ", Prenom:" + Prenom;
            result += ", Nom:" + Nom;
            result += ", Email:" + Email;
            result += "}";
            return result;
        }
        #endregion
    }
}
