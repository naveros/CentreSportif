using System;
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
        const String queryUpdatePersonne = "UPDATE personne SET prenom = @prenom, nom = @nom, email = @email, motdepasse = @motdepasse, codebarre = @codebarre,role = @role WHERE idpersonne=@idpersonne;";
        const String queryDeletePersonne = "DELETE FROM personne WHERE idpersonne = @idpersonne";
        const String queryReadPersonneByCodeBarre = "SELECT * FROM personne WHERE codebarre = @codebarre";
        #endregion
        const String queryReadAdresse = "SELECT * FROM adresse WHERE idpersonne = @idpersonne";

        const String queryReadAllAbonnements = "SELECT * FROM abonnement WHERE idpersonne = @idpersonne";
        const String queryCreateAbonnement = "INSERT INTO abonnement(idpersonne,idgroupe, dateinscription, datefin , prix)VALUES(@idpersonne, @idgroupe, @dateinscription, @datefin , @prix)";

        const String queryCreateEnseigne = "INSERT INTO enseigne(idpersonne, idgroupe)VALUES(@idpersonne, @idgroupe)";

        const String queryReadAllGroupeSeance = "SELECT * FROM seance WHERE idgroup=@idgroup";

        const String queryReadAllPresences = "SELECT * FROM presence WHERE idpersonne = @idpersonne";

        const String queryReadAllPaiements = "SELECT * FROM paiement WHERE idpersonne = @idpersonne";
        const String queryCreatePaiement = "INSERT INTO paiement(idpersonne,date,montant,mode)VALUES(@idpersonne, @date, @montant, @mode)";

        #endregion

        public PersonneDAO(MySqlConnection connexion)
        {
            this.con = connexion;
        }

        #region CRUD Personne

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
            cmd.Parameters.AddWithValue("@idpersonne", p.Prenom);
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
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                con.Close();
            }


        }
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
        #region CRUD Enseigne
        public void addEnseigne(PersonneDTO personneDTO, GroupeDTO groupeDTO)
        {
            MySqlCommand cmd = new MySqlCommand(queryCreateEnseigne, con);
            cmd.Parameters.AddWithValue("@idpersonne", personneDTO.IdPersonne);
            cmd.Parameters.AddWithValue("@idgroupe", groupeDTO.IdGroupe);
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
            cmd.Parameters.AddWithValue("@date", paiementDTO.Date);
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

    }
}
