using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using MySql.Data.MySqlClient;
using CentreSportifLib.dto;

namespace CentreSportifLib.dao
{
    public class PersonneDAO
    {

        MySqlConnection connexion;

        #region SQL Queries

        #region Queries Personnes
        const String queryCreatePersonne = "INSERT INTO personne (prenom, nom, sexe, datenaissance, email, motdepasse, codebarre, role) VALUES (@prenom, @nom, @sexe, @datenaissance, @email, @motdepasse, @codebarre, @role)";
        const String queryReadAllPersonne = "SELECT * FROM personne";
        const String queryReadByRole = "SELECT * FROM personne WHERE role = @role";
        const String queryReadPersonne = "SELECT * FROM personne WHERE idpersonne = @idpersonne";
        const String queryUpdatePersonne = "UPDATE personne SET prenom = @prenom, nom = @nom, email = @email, motdepasse = @motdepasse, codebarre = @codebarre, role = @role, datenaissance = @datenaissance, sexe = @sexe WHERE idpersonne=@idpersonne;";
        const String queryDeletePersonne = "DELETE FROM personne WHERE idpersonne = @idpersonne";
        const String queryReadPersonneByCodeBarre = "SELECT * FROM personne WHERE codebarre = @codebarre";
        #endregion

        const String queryReadAllAbonnements = "SELECT * FROM abonnement WHERE idpersonne = @idpersonne";
        const String queryCreateAbonnement = "INSERT INTO abonnement(idpersonne,idgroupe, dateinscription, datefin , prix)VALUES(@idpersonne, @idgroupe, @dateinscription, @datefin , @prix)";

        const String queryCreateEnseigne = "INSERT INTO enseigne(idpersonne, idgroupe)VALUES(@idpersonne, @idgroupe)";
        const String queryReadEnseigneByGroupId = "SELECT * FROM enseigne WHERE idgroupe = @idgroupe";

        const String queryReadAllPresences = "SELECT * FROM presence WHERE idpersonne = @idpersonne";

        const String queryReadAllPaiements = "SELECT * FROM paiement WHERE idpersonne = @idpersonne";
        const String queryCreatePaiement = "INSERT INTO paiement(idpersonne,date,montant,mode)VALUES(@idpersonne, NOW(), @montant, @mode)";

        const String queryReadAdresse = "SELECT * FROM addresse WHERE idpersonne = @idpersonne";
        const String queryCreateAdresse = "INSERT INTO addresse(numero,rue,codepostal,ville,pays,idpersonne)VALUES(@numero,@rue,@codepostal,@ville,@pays,@idpersonne)";
        const String queryUpdateAdresse = "UPDATE addresse SET numero = @numero, rue = @rue, codepostal = @codepostal, ville = @ville, pays = @pays WHERE idpersonne=@idpersonne;";

        const String queryCreateMessage = "INSERT INTO message (idpersonne,contenu,datecreation)VALUES(@idpersonne,@contenu, NOW())";
        const String queryReadAllMessages = "SELECT * FROM message WHERE idpersonne = @idpersonne";
        const String queryDeleteMessage = "DELETE FROM message WHERE idmessage = @idmessage";
        #endregion

        public PersonneDAO(MySqlConnection connexion)
        {
            this.connexion = connexion;
        }

        #region CRUD Personne

        public String addPersonne(PersonneDTO personneDTO)
        {
            String result = "null";
            MySqlCommand cmd = new MySqlCommand(queryCreatePersonne, connexion);
            cmd.Parameters.AddWithValue("@prenom", personneDTO.Prenom);
            cmd.Parameters.AddWithValue("@nom", personneDTO.Nom);
            cmd.Parameters.AddWithValue("@sexe", personneDTO.Sexe);
            cmd.Parameters.AddWithValue("@datenaissance", personneDTO.DateNaissance);
            cmd.Parameters.AddWithValue("@email", personneDTO.Email);
            cmd.Parameters.AddWithValue("@motdepasse", personneDTO.MotDePasse);
            cmd.Parameters.AddWithValue("@codebarre", personneDTO.CodeBarre);
            cmd.Parameters.AddWithValue("@role", personneDTO.Role);
            try
            {
                connexion.Open();
                cmd.ExecuteNonQuery();
                result = cmd.LastInsertedId.ToString();
            }
            finally
            {
                connexion.Close();
            }
            return result;
        }

        public PersonneDTO getPersonneByCodeBarre(String codeBarre)
        {
            MySqlCommand cmd = new MySqlCommand(queryReadPersonneByCodeBarre, connexion);
            MySqlDataReader reader = null;
            PersonneDTO result = new PersonneDTO();
            try
            {
                connexion.Open();
                cmd.Parameters.AddWithValue("@codebarre", codeBarre);

                reader = cmd.ExecuteReader();
                reader.Read();
                result.IdPersonne = reader.GetString("idpersonne");
                result.Prenom = reader.GetString("prenom");
                result.Nom = reader.GetString("nom");
                result.Sexe = reader.GetChar("sexe");
                result.DateNaissance = reader.GetDateTime("datenaissance");
                result.Email = reader.GetString("email");
                result.MotDePasse = reader.GetString("motdepasse");
                result.CodeBarre = reader.GetString("codebarre");
                result.Role = reader.GetString("role");
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return result;
        }

        public PersonneDTO getPersonne(int idPersonne)
        {
            MySqlCommand cmd = new MySqlCommand(queryReadPersonne, connexion);
            MySqlDataReader reader = null;
            PersonneDTO result = new PersonneDTO();
            try
            {
                connexion.Open();
                cmd.Parameters.AddWithValue("@idpersonne", idPersonne);

                reader = cmd.ExecuteReader();
                reader.Read();
                result.IdPersonne = reader.GetString("idpersonne");
                result.Prenom = reader.GetString("prenom");
                result.Nom = reader.GetString("nom");
                result.Sexe = reader.GetChar("sexe");
                result.DateNaissance = reader.GetDateTime("datenaissance");
                result.Email = reader.GetString("email");
                result.MotDePasse = reader.GetString("motdepasse");
                result.CodeBarre = reader.GetString("codebarre");
                result.Role = reader.GetString("role");
            }
            finally
            {
                connexion.Close();
            }
            return result;
        }

        public void updatePersonne(PersonneDTO personneDTO)
        {
            MySqlCommand cmd = new MySqlCommand(queryUpdatePersonne, connexion);
            cmd.Parameters.AddWithValue("@idpersonne", personneDTO.IdPersonne);
            cmd.Parameters.AddWithValue("@prenom", personneDTO.Prenom);
            cmd.Parameters.AddWithValue("@nom", personneDTO.Nom);
            cmd.Parameters.AddWithValue("@email", personneDTO.Email);
            cmd.Parameters.AddWithValue("@motdepasse", personneDTO.MotDePasse);
            cmd.Parameters.AddWithValue("@codebarre", personneDTO.CodeBarre);
            cmd.Parameters.AddWithValue("@role", personneDTO.Role);
            cmd.Parameters.AddWithValue("@sexe", personneDTO.Sexe);
            cmd.Parameters.AddWithValue("@datenaissance", personneDTO.DateNaissance);
            try
            {
                connexion.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                connexion.Close();
            }
        }

        public void deletePersonne(PersonneDTO personneDTO)
        {
            MySqlCommand cmd = new MySqlCommand(queryDeletePersonne, connexion);
            cmd.Parameters.AddWithValue("@idpersonne", personneDTO.IdPersonne); ;
            try
            {
                connexion.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                connexion.Close();
            }
        }

        public List<PersonneDTO> getAllPersonnes()
        {
            MySqlCommand cmd = new MySqlCommand(queryReadAllPersonne, connexion);
            MySqlDataReader reader = null;
            List<PersonneDTO> result = new List<PersonneDTO>();
            try
            {
                connexion.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PersonneDTO p = new PersonneDTO();
                    p.IdPersonne = reader.GetString("idpersonne");
                    p.Prenom = reader.GetString("prenom");
                    p.Nom = reader.GetString("nom");
                    p.Sexe = reader.GetChar("sexe");
                    p.DateNaissance = reader.GetDateTime("datenaissance");
                    p.Email = reader.GetString("email");
                    p.MotDePasse = reader.GetString("motdepasse");
                    p.CodeBarre = reader.GetString("codebarre");
                    p.Role = reader.GetString("role");
                    result.Add(p);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans la requete getAllPersonnes");
                Console.Write(e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return result;
        }

        public List<PersonneDTO> getAllByRole(String role)
        {
            MySqlCommand cmd = new MySqlCommand(queryReadByRole, connexion);
            MySqlDataReader reader = null;
            List<PersonneDTO> result = new List<PersonneDTO>();
            try
            {
                connexion.Open();
                cmd.Parameters.AddWithValue("@role", role);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PersonneDTO p = new PersonneDTO();
                    p.IdPersonne = reader.GetString("idpersonne");
                    p.Prenom = reader.GetString("prenom");
                    p.Nom = reader.GetString("nom");
                    p.Sexe = reader.GetChar("sexe");
                    p.DateNaissance = reader.GetDateTime("datenaissance");
                    p.Email = reader.GetString("email");
                    p.MotDePasse = reader.GetString("motdepasse");
                    p.CodeBarre = reader.GetString("codebarre");
                    p.Role = reader.GetString("role");
                    result.Add(p);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans la requete getAllPersonnes");
                Console.Write(e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return result;
        }

        #endregion

        #region CRUD Adresse

        public void addAdresse(AdresseDTO adresseDTO)
        {
            MySqlCommand cmd = new MySqlCommand(queryCreateAdresse, connexion);
            cmd.Parameters.AddWithValue("@numero", adresseDTO.Numero);
            cmd.Parameters.AddWithValue("@rue", adresseDTO.Rue);
            cmd.Parameters.AddWithValue("@codepostal", adresseDTO.CodePostal);
            cmd.Parameters.AddWithValue("@ville", adresseDTO.Ville);
            cmd.Parameters.AddWithValue("@pays", adresseDTO.Pays);
            cmd.Parameters.AddWithValue("@idpersonne", adresseDTO.IdPersonne);
            try
            {
                connexion.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                connexion.Close();
            }
        }
        public AdresseDTO getAdresse(String idPersonne)
        {
            MySqlCommand cmd = new MySqlCommand(queryReadAdresse, connexion);
            MySqlDataReader reader = null;
            AdresseDTO result = new AdresseDTO();
            try
            {
                connexion.Open();
                cmd.Parameters.AddWithValue("@idpersonne", idPersonne);
                reader = cmd.ExecuteReader();
                reader.Read();
                result.IdAdresse = reader.GetString("idaddresse");
                result.IdPersonne = reader.GetString("idpersonne");
                result.Numero = reader.GetString("numero");
                result.Rue = reader.GetString("idpersonne");
                result.CodePostal = reader.GetString("codepostal");
                result.Ville = reader.GetString("ville");
                result.Pays = reader.GetString("pays");
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                connexion.Close();
            }
            Console.WriteLine(result.ToString());
            return result;
        }
        public void updateAdresse(AdresseDTO adresseDTO)
        {
            MySqlCommand cmd = new MySqlCommand(queryUpdateAdresse, connexion);
            cmd.Parameters.AddWithValue("@numero", adresseDTO.Numero);
            cmd.Parameters.AddWithValue("@rue", adresseDTO.Rue);
            cmd.Parameters.AddWithValue("@codepostal", adresseDTO.CodePostal);
            cmd.Parameters.AddWithValue("@ville", adresseDTO.Ville);
            cmd.Parameters.AddWithValue("@pays", adresseDTO.Pays);
            cmd.Parameters.AddWithValue("@idpersonne", adresseDTO.IdPersonne);
            try
            {
                connexion.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                connexion.Close();
            }
        }
        public void deleteAdresse() { }

        #endregion

        #region CRUD Abonnement
        public void addAbonnement(AbonnementDTO abonnementDTO)
        {
            MySqlCommand cmd = new MySqlCommand(queryCreateAbonnement, connexion);
            cmd.Parameters.AddWithValue("@idpersonne", abonnementDTO.IdPersonne);
            cmd.Parameters.AddWithValue("@idgroupe", abonnementDTO.IdGroupe);
            cmd.Parameters.AddWithValue("@dateinscription", abonnementDTO.DateInscription);
            cmd.Parameters.AddWithValue("@datefin", abonnementDTO.DateFin);
            cmd.Parameters.AddWithValue("@prix", abonnementDTO.Prix);
            try
            {
                connexion.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                connexion.Close();
            }
        }
        public List<AbonnementDTO> getAllAbonnements(PersonneDTO personneDTO)
        {
            MySqlCommand cmd = new MySqlCommand(queryReadAllAbonnements, connexion);
            cmd.Parameters.AddWithValue("@idpersonne", personneDTO.IdPersonne);
            MySqlDataReader reader = null;
            List<AbonnementDTO> result = new List<AbonnementDTO>();
            try
            {
                connexion.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    AbonnementDTO abonnementDTO = new AbonnementDTO();
                    abonnementDTO.IdAbonnement = reader.GetString("idabonnement");
                    abonnementDTO.IdPersonne = reader.GetString("idpersonne");
                    abonnementDTO.IdGroupe = reader.GetString("idgroupe");
                    abonnementDTO.DateInscription = (DateTime)reader["dateinscription"];
                    abonnementDTO.DateFin = (DateTime)reader["datefin"];
                    abonnementDTO.Prix = reader.GetDecimal("prix");
                    result.Add(abonnementDTO);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans la requete getAllAbonnements");
                Console.Write(e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return result;
        }

        #endregion

        #region CRUD Enseigne
        public void addEnseigne(EnseigneDTO enseigneDTO)
        {
            MySqlCommand cmd = new MySqlCommand(queryCreateEnseigne, connexion);
            cmd.Parameters.AddWithValue("@idpersonne", enseigneDTO.IdPersonne);
            cmd.Parameters.AddWithValue("@idgroupe", enseigneDTO.IdGroupe);
            try
            {
                connexion.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                connexion.Close();
            }
        }
        public EnseigneDTO getEnseigneByGroupId(String idGroupe)
        {
            MySqlCommand cmd = new MySqlCommand(queryReadEnseigneByGroupId, connexion);
            cmd.Parameters.AddWithValue("@idgroupe", idGroupe);
            MySqlDataReader reader = null;
            EnseigneDTO result = new EnseigneDTO();
            try
            {
                connexion.Open();
                reader = cmd.ExecuteReader();
                reader.Read();
                result.IdEnseigne = reader.GetString("idenseigne");
                result.IdPersonne = reader.GetString("idpersonne");
                result.IdGroupe = idGroupe;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return result;
        }
        #endregion

        #region CRUD Presence
        public void addPresence() { }
        public void getPresence() { }
        public void updatePresence() { }
        public void deletePresence() { }

        public List<PresenceDTO> getAllPresences(PersonneDTO personneDTO)
        {
            MySqlCommand cmd = new MySqlCommand(queryReadAllPresences, connexion);
            cmd.Parameters.AddWithValue("@idpersonne", personneDTO.IdPersonne);
            MySqlDataReader reader = null;
            List<PresenceDTO> result = new List<PresenceDTO>();
            try
            {
                connexion.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PresenceDTO presenceDTO = new PresenceDTO();
                    presenceDTO.IdPresence = reader.GetString("idpresence");
                    presenceDTO.IdPersonne = reader.GetString("idpersonne");
                    presenceDTO.IdSeance = reader.GetString("idseance");
                    presenceDTO.Present = reader.GetBoolean("present");
                    result.Add(presenceDTO);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans la requete getAllPresences");
                Console.Write(e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return result;
        }


        #endregion

        #region CRUD Paiement
        public void addPaiement(PaiementDTO paiementDTO)
        {
            MySqlCommand cmd = new MySqlCommand(queryCreatePaiement, connexion);
            cmd.Parameters.AddWithValue("@idpersonne", paiementDTO.IdPersonne);
            cmd.Parameters.AddWithValue("@montant", paiementDTO.Montant);
            cmd.Parameters.AddWithValue("@mode", paiementDTO.Mode);

            try
            {
                connexion.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                connexion.Close();
            }
        }
        public List<PaiementDTO> getAllPaiements(String idPersonne)
        {
            MySqlCommand cmd = new MySqlCommand(queryReadAllPaiements, connexion);
            cmd.Parameters.AddWithValue("@idpersonne", idPersonne);
            MySqlDataReader reader = null;
            List<PaiementDTO> result = new List<PaiementDTO>();

            try
            {
                connexion.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PaiementDTO paiementDTO = new PaiementDTO();
                    paiementDTO.IdPersonne = reader.GetString("idpersonne");
                    paiementDTO.Date = reader.GetDateTime("date");
                    paiementDTO.Montant = reader.GetDecimal("montant");
                    paiementDTO.Mode = reader.GetString("mode");
                    result.Add(paiementDTO);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans la requete getAllPaiements");
                Console.Write(e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return result;
        }

        #endregion

        #region CRUD Message

        public void addMessage(MessageDTO messageDTO)
        {
            MySqlCommand cmd = new MySqlCommand(queryCreateMessage, connexion);
            cmd.Parameters.AddWithValue("@idpersonne", messageDTO.IdPersonne);
            cmd.Parameters.AddWithValue("@contenu", messageDTO.Contenu);
            try
            {
                connexion.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                connexion.Close();
            }
        }
        public void deleteMessage(String idMessage)
        {
            MySqlCommand cmd = new MySqlCommand(queryDeleteMessage, connexion);
            cmd.Parameters.AddWithValue("@idmessage", idMessage); ;

            try
            {
                connexion.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                connexion.Close();
            }
        }
        public List<MessageDTO> getAllMessages(String idPersonne)
        {


            MySqlCommand cmd = new MySqlCommand(queryReadAllMessages, connexion);
            cmd.Parameters.AddWithValue("@idpersonne", idPersonne);
            MySqlDataReader reader = null;
            List<MessageDTO> result = new List<MessageDTO>();

            try
            {
                connexion.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    MessageDTO MessageDTO = new MessageDTO();
                    MessageDTO.IdMessage = reader.GetString("idmessage");
                    MessageDTO.IdPersonne = reader.GetString("idpersonne");
                    MessageDTO.Contenu = reader.GetString("contenu");
                    MessageDTO.DateCreation = reader.GetDateTime("datecreation");
                    result.Add(MessageDTO);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans la requete getAllPaiements");
                Console.Write(e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return result;
        }
        #endregion
    }
}
