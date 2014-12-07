﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using CentreSportifLib.dto;
using System.Globalization;

namespace CentreSportifLib.dao
{
    
    //TODO crud de abonnement . presences et adresse:
    public class PersonneDAO
    {

        MySqlConnection con;

        #region SQL Queries 

        const String queryCreatePersonne = "INSERT INTO personne (prenom, nom, sexe, datenaissance, email, motdepasse, codebarre, role) VALUES (@prenom, @nom, @sexe, @datenaissance, @email, @motdepasse, @codebarre, @role)";
        const String queryReadAllPersonne = "SELECT * FROM personne";
        const String queryReadPersonne = "SELECT * FROM personne WHERE idpersonne = @idpersonne";
        const String queryUpdatePersonne = "UPDATE personne SET prenom = @prenom, nom = @nom, email = @email, motdepasse = @motdepasse, codebarre = @codebarre,role = @role WHERE conditions;";
        const String queryDeletePersonne = "DELETE FROM personne WHERE idpersonne = @idpersonne";
       
        const String queryReadAdresse = "SELECT * FROM adresse WHERE idpersonne = @idpersonne";

        const String queryReadAllAbonnements = "SELECT * FROM abonnement WHERE idpersonne = @idpersonne";
        
        const String queryReadAllPresences = "SELECT * FROM presence WHERE idpersonne = @idpersonne";
        
        const String queryReadAllPaiements = "SELECT * FROM paiement WHERE idpersonne = @idpersonne";

        #endregion
        
        public PersonneDAO(MySqlConnection connexion)
        {
            this.con = connexion;
        }

        #region Crud Personne

        public void addPersonne(PersonneDTO p)
        {
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

        public PersonneDTO getPersonne(PersonneDTO p)
        {
            MySqlCommand cmd = new MySqlCommand(queryReadPersonne, con);
            MySqlDataReader reader = null;
            PersonneDTO result = new PersonneDTO();
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@idpersonne", p.IdPersonne);
                reader = cmd.ExecuteReader();
                reader.Read();
                result.IdPersonne = (String)reader["idpersonne"];
                result.Prenom = (String)reader["prenom"];
                result.Nom = (String)reader["nom"];
                result.Sexe = (char)reader["sexe"];
                result.DateNaissance = (DateTime)reader["datenaissance"];
                result.Email = (String)reader["email"];
                result.MotDePasse = (String)reader["motdepasse"];
                result.CodeBarre = (String)reader["codebarre"];
                result.Role = (String)reader["role"];
                //String r2 = reader[0].ToString();
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

       public void updatePersonne(PersonneDTO p)
        {
            MySqlCommand cmd = new MySqlCommand(queryUpdatePersonne, con);
            cmd.Parameters.AddWithValue("@prenom", p.Prenom);
            cmd.Parameters.AddWithValue("@nom", p.Nom);
            cmd.Parameters.AddWithValue("@email", p.Email);
            cmd.Parameters.AddWithValue("@motdepasse", p.MotDePasse);
            cmd.Parameters.AddWithValue("@codebarre", p.CodeBarre);
            cmd.Parameters.AddWithValue("@role", p.Role);
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

        #endregion 


        #region Crud Adresse

        public void addAdresse() { }
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
                result.IdAdresse = (String)reader["idadresse"];
                result.IdPersonne = (String)reader["idpersonne"];
                result.Numero = (String)reader["numero"];
                result.Rue = (String)reader["idpersonne"];
                result.CodePostal = (String)reader["codepostal"];
                result.Ville = (String)reader["ville"];
                result.Pays = (String)reader["pays"];
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
        public void updateAdresse() { } 
        public void deleteAdresse() { }

        #endregion

        #region Crud Abonnement
        public void addAbonnement() { }
        public void getAbonnement() { }
        public void updateAbonnement() { } 
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

        #region Crud Presence
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

        #region Crud Paiement
        public void addPaiement() { }
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

    }
}
