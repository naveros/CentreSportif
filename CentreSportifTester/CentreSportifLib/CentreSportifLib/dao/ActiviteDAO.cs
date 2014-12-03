using CentreSportifLib.dto;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentreSportifLib.dao
{
    public class ActiviteDAO
    {
        MySqlConnection con;
        const String queryCreate = "INSERT INTO activite (nom, duree, description) VALUES (@nom, @duree, @description)";
        const String queryReadAll = "SELECT * FROM activite";
        const String queryRead = "SELECT * FROM activite WHERE idactivite = @idactivite";
        const String queryUpdate = "UPDATE activite SET nom = @nom, duree=@duree, description=@description WHERE idactivite=@idactivite;";
        const String queryDelete = "DELETE FROM activite WHERE idactivite=@idactivite;";
        public ActiviteDAO(MySqlConnection connexion)
        {
            this.con = connexion;
        }

        public void add(ActiviteDTO a)
        {
            MySqlCommand cmd = new MySqlCommand(queryCreate, con);
            cmd.Parameters.AddWithValue("@nom", a.Nom);
            cmd.Parameters.AddWithValue("@duree", a.Duree);
            cmd.Parameters.AddWithValue("@description", a.Description);
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

        public ActiviteDTO get(ActiviteDTO a)
        {
            MySqlCommand cmd = new MySqlCommand(queryRead, con);
            MySqlDataReader reader = null;
            ActiviteDTO result = new ActiviteDTO();
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@idactivite", a.IdActivite);
                reader = cmd.ExecuteReader();
                reader.Read();
                result.IdActivite = reader.GetString("idactivite");
                result.Nom = reader.GetString("nom");
                result.Duree = reader.GetString("duree");
                result.Description = reader.GetString("description");
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

        public List<ActiviteDTO> getAll()
        {
            MySqlCommand cmd = new MySqlCommand(queryReadAll, con);
            MySqlDataReader reader = null;
            List<ActiviteDTO> result = new List<ActiviteDTO>();
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ActiviteDTO a = new ActiviteDTO();
                    a.IdActivite = reader.GetString("idactivite");
                    a.Duree = reader.GetString("duree");
                    a.Description = reader.GetString("description");
                    result.Add(a);
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

        public void update(ActiviteDTO a)
        {
            MySqlCommand cmd = new MySqlCommand(queryUpdate, con);
            cmd.Parameters.AddWithValue("@idactivite", a.IdActivite);
            cmd.Parameters.AddWithValue("@nom", a.Nom);
            cmd.Parameters.AddWithValue("@duree", int.Parse(a.Duree));
            cmd.Parameters.AddWithValue("@description", a.Description);
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

        public void delete(ActiviteDTO a)
        {
            MySqlCommand cmd = new MySqlCommand(queryDelete, con);
            cmd.Parameters.AddWithValue("@idactivite", a.IdActivite); ;
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
