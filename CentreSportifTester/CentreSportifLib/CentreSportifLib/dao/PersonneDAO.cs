using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using CentreSportifLib.dto;
using System.Globalization;

namespace CentreSportifLib.dao
{
    public class PersonneDAO
    {
        MySqlConnection con;
        const String queryCreate = "INSERT INTO personne (prenom, nom, sexe, datenaissance, email, motdepasse, codebarre) VALUES (@prenom, @nom, @sexe, @datenaissance, @email, @motdepasse, @codebarre)";
        const String queryReadAll = "SELECT * FROM personne";
        const String queryRead = "SELECT * FROM personne WHERE idpersonne = @idpersonne";
        const String queryUpdate = "UPDATE personne SET prenom = @prenom, nom = @nom, email = @email, motdepasse = @motdepasse, codebarre = @codebarre WHERE conditions;";
        const String queryDelete = "DELETE FROM personne WHERE idpersonne = @idpersonne";
        //List<PersonneDTO> listPersonne{ get; set; }

        public PersonneDAO(MySqlConnection connexion)
        {
            this.con = connexion;
        }
        public void add(PersonneDTO p)
        {
            MySqlCommand cmd = new MySqlCommand(queryCreate, con);
            cmd.Parameters.AddWithValue("@prenom", p.Prenom);
            cmd.Parameters.AddWithValue("@nom", p.Nom);
            cmd.Parameters.AddWithValue("@sexe", p.Sexe);
            cmd.Parameters.AddWithValue("@datenaissance", p.DateNaissance);
            cmd.Parameters.AddWithValue("@email", p.Email);
            cmd.Parameters.AddWithValue("@motdepasse", p.MotDePasse);
            cmd.Parameters.AddWithValue("@codebarre", p.CodeBarre);
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

        public PersonneDTO get(PersonneDTO p)
        {
            MySqlCommand cmd = new MySqlCommand(queryRead, con);
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

        public List<PersonneDTO> getAll()
        {
            MySqlCommand cmd = new MySqlCommand(queryReadAll, con);
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
                    result.Add(p);
                    //String r2 = reader[0].ToString();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans la requete getAll");
                Console.Write(e.Message);
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public void update(PersonneDTO p)
        {
            MySqlCommand cmd = new MySqlCommand(queryUpdate, con);
            cmd.Parameters.AddWithValue("@prenom", p.Prenom);
            cmd.Parameters.AddWithValue("@nom", p.Nom);
            cmd.Parameters.AddWithValue("@email", p.Email);
            cmd.Parameters.AddWithValue("@motdepasse", p.MotDePasse);
            cmd.Parameters.AddWithValue("@codebarre", p.CodeBarre);
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

        public void delete(PersonneDTO p)
        {
            MySqlCommand cmd = new MySqlCommand(queryDelete, con);
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
    }
}
