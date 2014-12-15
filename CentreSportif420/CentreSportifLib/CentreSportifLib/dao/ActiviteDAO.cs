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
        MySqlConnection connexion;
        const String queryCreate = "INSERT INTO activite (nom, duree, description) VALUES (@nom, @duree, @description)";
        const String queryReadAll = "SELECT * FROM activite";
        const String queryRead = "SELECT * FROM activite WHERE idactivite = @idactivite";
        const String queryUpdate = "UPDATE activite SET nom = @nom, duree=@duree, description=@description WHERE idactivite=@idactivite;";
        const String queryDelete = "DELETE FROM activite WHERE idactivite=@idactivite;";
        
        public ActiviteDAO(MySqlConnection connexion)
        {
            this.connexion = connexion;
        }

        public void add(ActiviteDTO activiteDTO)
        {
            MySqlCommand cmd = new MySqlCommand(queryCreate, connexion);
            cmd.Parameters.AddWithValue("@nom", activiteDTO.Nom);
            cmd.Parameters.AddWithValue("@duree", activiteDTO.Duree);
            cmd.Parameters.AddWithValue("@description", activiteDTO.Description);
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

        public ActiviteDTO get(String idActivite)
        {
            MySqlCommand cmd = new MySqlCommand(queryRead, connexion);
            MySqlDataReader reader = null;
            ActiviteDTO result = new ActiviteDTO();
            try
            {
                connexion.Open();
                cmd.Parameters.AddWithValue("@idactivite", idActivite);
                reader = cmd.ExecuteReader();
                reader.Read();
                result.IdActivite = idActivite;
                result.Nom = reader.GetString("nom");
                result.Duree = reader.GetString("duree");
                result.Description = reader.GetString("description");
            }
            finally
            {
                connexion.Close();
            }
            return result;
        }

        public List<ActiviteDTO> getAll()
        {
            MySqlCommand cmd = new MySqlCommand(queryReadAll, connexion);
            MySqlDataReader reader = null;
            List<ActiviteDTO> result = new List<ActiviteDTO>();
            try
            {
                connexion.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ActiviteDTO activiteDTO = new ActiviteDTO();
                    activiteDTO.IdActivite = reader.GetString("idactivite");
                    activiteDTO.Nom = reader.GetString("nom");
                    activiteDTO.Duree = reader.GetString("duree");
                    activiteDTO.Description = reader.GetString("description");
                    result.Add(activiteDTO);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans la requete getAllActivite : \n"+e);
                Console.Write(e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return result;
        }

        public void update(ActiviteDTO activiteDTO)
        {
            MySqlCommand cmd = new MySqlCommand(queryUpdate, connexion);
            cmd.Parameters.AddWithValue("@idactivite", activiteDTO.IdActivite);
            cmd.Parameters.AddWithValue("@nom", activiteDTO.Nom);
            cmd.Parameters.AddWithValue("@duree", int.Parse(activiteDTO.Duree));
            cmd.Parameters.AddWithValue("@description", activiteDTO.Description);
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

        public void delete(ActiviteDTO activiteDTO)
        {
            MySqlCommand cmd = new MySqlCommand(queryDelete, connexion);
            cmd.Parameters.AddWithValue("@idactivite", activiteDTO.IdActivite); ;
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

    }
}
