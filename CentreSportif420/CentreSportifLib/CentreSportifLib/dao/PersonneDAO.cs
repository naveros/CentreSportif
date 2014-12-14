﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using CentreSportifLib.dto;
using System.Globalization;

namespace CentreSportifLib.dao
{

    //TODO CRUD de abonnement . presences et adresse:
    public class PersonneDAO
    {

        MySqlConnection con;

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
            this.con = connexion;
        }

        #region CRUD Personne

        public String addPersonne(PersonneDTO p)
        {
            String result = "null";
            MySqlCommand cmd = new MySqlCommand(queryCreatePersonne, con);
            cmd.Parameters.AddWithValue("@prenom", p.Prenom);
            cmd.Parameters.AddWithValue("@nom", p.Nom);
            cmd.Parameters.AddWithValue("@sexe", p.Sexe);
            cmd.Parameters.AddWithValue("@datenaissance", p.DateNaissance);
            cmd.Parameters.AddWithValue("@email", p.Email);
            cmd.Parameters.AddWithValue("@motdepasse", p.MotDePasse);
            cmd.Parameters.AddWithValue("@codebarre", p.CodeBarre);
            cmd.Parameters.AddWithValue("@role", p.Role);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                result = cmd.LastInsertedId.ToString();
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public PersonneDTO getPersonneByCodeBarre(String codeBarre)
        {
            MySqlCommand cmd = new MySqlCommand(queryReadPersonneByCodeBarre, con);
            MySqlDataReader reader = null;
            PersonneDTO result = new PersonneDTO();
            try
            {
                con.Open();
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
                con.Close();
            }
            return result;
        }

        public PersonneDTO getPersonne(int idPersonne)
        {
            MySqlCommand cmd = new MySqlCommand(queryReadPersonne, con);
            MySqlDataReader reader = null;
            PersonneDTO result = new PersonneDTO();
            try
            {
                con.Open();
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
                //String r2 = reader[0].ToString();
            }
            /*    catch (Exception e)
                {
                    Console.Write(e.Message);
                }*/
            finally
            {
                con.Close();
            }
            return result;
        }

        public void updatePersonne(PersonneDTO p)
        {
            MySqlCommand cmd = new MySqlCommand(queryUpdatePersonne, con);
            cmd.Parameters.AddWithValue("@idpersonne", p.IdPersonne);
            cmd.Parameters.AddWithValue("@prenom", p.Prenom);
            cmd.Parameters.AddWithValue("@nom", p.Nom);
            cmd.Parameters.AddWithValue("@email", p.Email);
            cmd.Parameters.AddWithValue("@motdepasse", p.MotDePasse);
            cmd.Parameters.AddWithValue("@codebarre", p.CodeBarre);
            cmd.Parameters.AddWithValue("@role", p.Role);
            cmd.Parameters.AddWithValue("@sexe", p.Sexe);
            cmd.Parameters.AddWithValue("@datenaissance", p.DateNaissance);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }

        public void deletePersonne(PersonneDTO p)
        {
            MySqlCommand cmd = new MySqlCommand(queryDeletePersonne, con);
            cmd.Parameters.AddWithValue("@idpersonne", p.IdPersonne); ;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public List<PersonneDTO> getAllPersonnes()
        {
            MySqlCommand cmd = new MySqlCommand(queryReadAllPersonne, con);
            MySqlDataReader reader = null;
            List<PersonneDTO> result = new List<PersonneDTO>();
            try
            {
                con.Open();
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
                    //String r2 = reader[0].ToString();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans la requete getAllPersonnes");
                Console.Write(e.Message);
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public List<PersonneDTO> getAllByRole(String role)
        {
            MySqlCommand cmd = new MySqlCommand(queryReadByRole, con);
            MySqlDataReader reader = null;
            List<PersonneDTO> result = new List<PersonneDTO>();
            try
            {
                con.Open();
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
                    //String r2 = reader[0].ToString();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans la requete getAllPersonnes");
                Console.Write(e.Message);
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        #endregion

        //TODO CRUD message, seance

        #region CRUD Adresse

        public void addAdresse(AdresseDTO adresseDTO)
        {
            MySqlCommand cmd = new MySqlCommand(queryCreateAdresse, con);
            cmd.Parameters.AddWithValue("@numero", adresseDTO.Numero);
            cmd.Parameters.AddWithValue("@rue", adresseDTO.Rue);
            cmd.Parameters.AddWithValue("@codepostal", adresseDTO.CodePostal);
            cmd.Parameters.AddWithValue("@ville", adresseDTO.Ville);
            cmd.Parameters.AddWithValue("@pays", adresseDTO.Pays);
            cmd.Parameters.AddWithValue("@idpersonne", adresseDTO.IdPersonne);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }
        public AdresseDTO getAdresse(PersonneDTO p)
        {
            MySqlCommand cmd = new MySqlCommand(queryReadAdresse, con);
            MySqlDataReader reader = null;
            AdresseDTO result = new AdresseDTO();
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@idpersonne", p.IdPersonne);
                reader = cmd.ExecuteReader();
                reader.Read();
               // result.IdAdresse = reader.GetString("idaddresse");
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
                con.Close();
            }
            return result;
        }
        public void updateAdresse(AdresseDTO adresseDTO)
        {
            MySqlCommand cmd = new MySqlCommand(queryUpdateAdresse, con);
            cmd.Parameters.AddWithValue("@numero", adresseDTO.Numero);
            cmd.Parameters.AddWithValue("@rue", adresseDTO.Rue);
            cmd.Parameters.AddWithValue("@codepostal", adresseDTO.CodePostal);
            cmd.Parameters.AddWithValue("@ville", adresseDTO.Ville);
            cmd.Parameters.AddWithValue("@pays", adresseDTO.Pays);
            cmd.Parameters.AddWithValue("@idpersonne", adresseDTO.IdPersonne);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }
        public void deleteAdresse() { }

        #endregion

        #region CRUD Abonnement
        public void addAbonnement(AbonnementDTO a)
        {
            MySqlCommand cmd = new MySqlCommand(queryCreateAbonnement, con);
            cmd.Parameters.AddWithValue("@idpersonne", a.IdPersonne);
            cmd.Parameters.AddWithValue("@idgroupe", a.IdGroupe);
            cmd.Parameters.AddWithValue("@dateinscription", a.DateInscription);
            cmd.Parameters.AddWithValue("@datefin", a.DateFin);
            cmd.Parameters.AddWithValue("@prix", a.Prix);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            /*  catch (Exception e)
              {
                  Console.Write(e.Message);
              }*/
            finally
            {
                con.Close();
            }
        }
        public void getAbonnement() { }
        public void updateAbonnement()
        {

        }
        public void deleteAbonnement() { }
        public List<AbonnementDTO> getAllAbonnements(PersonneDTO p)
        {
            MySqlCommand cmd = new MySqlCommand(queryReadAllAbonnements, con);
            cmd.Parameters.AddWithValue("@idpersonne", p.IdPersonne);
            MySqlDataReader reader = null;
            List<AbonnementDTO> result = new List<AbonnementDTO>();
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    AbonnementDTO a = new AbonnementDTO();

                    a.IdAbonnement = reader.GetString("idabonnement");
                    a.IdPersonne = reader.GetString("idpersonne");
                    a.IdGroupe = reader.GetString("idgroupe");
                    a.DateInscription = (DateTime)reader["dateinscription"];
                    a.DateFin = (DateTime)reader["datefin"];
                    a.Prix = reader.GetDecimal("prix");
                    result.Add(a);
                    //String r2 = reader[0].ToString();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans la requete getAllAbonnements");
                Console.Write(e.Message);
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        #endregion

        #region CRUD Enseigne
        public void addEnseigne(EnseigneDTO enseigne)
        {
            MySqlCommand cmd = new MySqlCommand(queryCreateEnseigne, con);
            cmd.Parameters.AddWithValue("@idpersonne", enseigne.IdPersonne);
            cmd.Parameters.AddWithValue("@idgroupe", enseigne.IdGroupe);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public EnseigneDTO getEnseigneByGroupId(String idGroupe)
        {

            MySqlCommand cmd = new MySqlCommand(queryReadEnseigneByGroupId, con);
            cmd.Parameters.AddWithValue("@idgroupe", idGroupe);
            MySqlDataReader reader = null;
            EnseigneDTO result = new EnseigneDTO();
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();
                reader.Read(); //While? 
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
                con.Close();
            }
            return result;
        }
        #endregion

        #region CRUD Presence
        public void addPresence() { }
        public void getPresence() { }
        public void updatePresence() { }
        public void deletePresence() { }
        public List<PresenceDTO> getAllPresences(PersonneDTO p)
        {
            MySqlCommand cmd = new MySqlCommand(queryReadAllPresences, con);
            cmd.Parameters.AddWithValue("@idpersonne", p.IdPersonne);
            MySqlDataReader reader = null;
            List<PresenceDTO> result = new List<PresenceDTO>();
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PresenceDTO presenceDTO = new PresenceDTO();

                    presenceDTO.IdPresence = reader.GetString("idpresence");
                    presenceDTO.IdPersonne = reader.GetString("idpersonne");
                    presenceDTO.IdSeance = reader.GetString("idseance");
                    presenceDTO.Present = reader.GetBoolean("present");
                    result.Add(presenceDTO);
                    //String r2 = reader[0].ToString();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans la requete getAllPresences");
                Console.Write(e.Message);
            }
            finally
            {
                con.Close();
            }
            return result;
        }


        #endregion

        #region CRUD Paiement
        public void addPaiement(PaiementDTO paiementDTO)
        {

            MySqlCommand cmd = new MySqlCommand(queryCreatePaiement, con);
            cmd.Parameters.AddWithValue("@idpersonne", paiementDTO.IdPersonne);
            cmd.Parameters.AddWithValue("@montant", paiementDTO.Montant);
            cmd.Parameters.AddWithValue("@mode", paiementDTO.Mode);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                con.Close();
            }

        }
        public void getPaiement() { }
        public void updatePaiement() { }
        public void deletePaiement() { }
        public List<PaiementDTO> getAllPaiements(PersonneDTO p)
        {
            MySqlCommand cmd = new MySqlCommand(queryReadAllPaiements, con);
            cmd.Parameters.AddWithValue("@idpersonne", p.IdPersonne);
            MySqlDataReader reader = null;
            List<PaiementDTO> result = new List<PaiementDTO>();

            try
            {
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PaiementDTO paiementDTO = new PaiementDTO();


                    paiementDTO.IdPersonne = reader.GetString("idpersonne");
                    paiementDTO.Date = reader.GetDateTime("date");
                    paiementDTO.Montant = reader.GetDecimal("montant");
                    paiementDTO.Mode = reader.GetString("mode");
                    result.Add(paiementDTO);
                    //String r2 = reader[0].ToString();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans la requete getAllPaiements");
                Console.Write(e.Message);
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        #endregion

        #region CRUD Message

        public void addMessage(MessageDTO messageDTO)
        {
            MySqlCommand cmd = new MySqlCommand(queryCreateMessage, con);
            cmd.Parameters.AddWithValue("@idpersonne", messageDTO.IdPersonne);
            cmd.Parameters.AddWithValue("@contenu", messageDTO.Contenu);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void getMessage() { }
        public void updateMessage() { }
        public void deleteMessage(String idMessage)
        {
            
            MySqlCommand cmd = new MySqlCommand(queryDeleteMessage, con);
            cmd.Parameters.AddWithValue("@idmessage",idMessage); ;

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public List<MessageDTO> getAllMessages(PersonneDTO personneDTO)
        {


            MySqlCommand cmd = new MySqlCommand(queryReadAllMessages, con);
            cmd.Parameters.AddWithValue("@idpersonne", personneDTO.IdPersonne);
            MySqlDataReader reader = null;
            List<MessageDTO> result = new List<MessageDTO>();

            try
            {
                con.Open();
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
                con.Close();
            }
            return result;
        }

        #endregion


    }
}