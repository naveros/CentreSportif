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
        List<PresenceDTO> presences;
        List<AbonnementDTO> abonnements;
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
        public List<PresenceDTO> Presences
        {
            set { this.presences = value; }
            get { return this.presences; }
        }
        public List<AbonnementDTO> Abonnements
        {
            set { this.abonnements = value; }
            get { return this.abonnements; }
        }
        #endregion
        #region Methodes
        public PersonneDTO()
        { }
        public override string ToString()
        {         
            return Prenom + " " + Nom; ;
        }
        #endregion
    }
}
