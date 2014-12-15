using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CentreSportifLib.dto;
using MySql.Data.MySqlClient;
namespace CentreSportifLib.dao
{
    public class GroupeDAO
    {
        MySqlConnection connexion;
        const String queryCreate = "INSERT INTO groupe (idactivite, numerogroupe, prix) VALUES (@idactivite, @numerogroupe, @prix)";
        const String queryReadAll = "SELECT * FROM groupe";
        const String queryRead = "SELECT * FROM groupe WHERE idgroupe = @idgroupe";
        const String queryReadByActivite = "SELECT * FROM groupe WHERE idactivite = @idactivite";
        const String queryUpdate = "UPDATE groupe SET numerogroupe = @numerogroupe WHERE idgroupe=@idgroupe;";
        const String queryDelete = "DELETE FROM groupe WHERE idgroupe=@idgroupe;";
        const String queryCreateSeance = "INSERT INTO seance (idgroupe, datedebut, datefin) VALUES (@idgroupe, @datedebut, @datefin)";
        const String queryReadAllseances = "SELECT * FROM seance WHERE idgroupe=@idgroupe;";

        public GroupeDAO(MySqlConnection connexion)
        {
            this.connexion = connexion;
        }

        #region CRUD Groupe
        public String add(GroupeDTO groupeDTO)
        {
            String id = "null";
            MySqlCommand cmd = new MySqlCommand(queryCreate, connexion);
            cmd.Parameters.AddWithValue("@idactivite", groupeDTO.IdActivite);
            cmd.Parameters.AddWithValue("@numerogroupe", groupeDTO.NumeroGroupe);
            cmd.Parameters.AddWithValue("@prix", groupeDTO.Prix);
            try
            {
                connexion.Open();
                cmd.ExecuteNonQuery();
                id = cmd.LastInsertedId.ToString();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return id;
        }

        public GroupeDTO get(String idGroupe)
        {
            MySqlCommand cmd = new MySqlCommand(queryRead, connexion);
            MySqlDataReader reader = null;
            GroupeDTO result = new GroupeDTO();
            try
            {
                connexion.Open();
                cmd.Parameters.AddWithValue("@idgroupe", idGroupe);
                reader = cmd.ExecuteReader();
                reader.Read();
                result.IdGroupe = reader.GetString("idgroupe");
                result.IdActivite = reader.GetString("idactivite");
                result.NumeroGroupe = reader.GetString("numerogroupe");
                result.Prix = reader.GetDecimal("prix");
            }
            /*  catch (Exception e)
              {
                  Console.Write(e.Message);
              }*/
            finally
            {
                connexion.Close();
            }
            return result;
        }

        public List<GroupeDTO> getAllByActivite(String idActivite)
        {

            MySqlCommand cmd = new MySqlCommand(queryReadByActivite, connexion);
            MySqlDataReader reader = null;
            List<GroupeDTO> result = new List<GroupeDTO>();
            try
            {
                connexion.Open();
                cmd.Parameters.AddWithValue("@idactivite", idActivite);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    GroupeDTO a = new GroupeDTO();
                    a.IdGroupe = reader.GetString("idgroupe");
                    a.IdActivite = idActivite;
                    a.NumeroGroupe = reader.GetString("numerogroupe");
                    a.Prix = reader.GetDecimal("prix");
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
                connexion.Close();
            }
            return result;
        }

        public List<GroupeDTO> getAll()
        {
            MySqlCommand cmd = new MySqlCommand(queryReadAll, connexion);
            MySqlDataReader reader = null;
            List<GroupeDTO> result = new List<GroupeDTO>();
            try
            {
                connexion.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    GroupeDTO groupeDTO = new GroupeDTO();
                    groupeDTO.IdGroupe = reader.GetString("idgroupe");
                    groupeDTO.IdActivite = reader.GetString("idactivite");
                    groupeDTO.NumeroGroupe = reader.GetString("numerogroupe");
                    groupeDTO.Prix = reader.GetDecimal("prix");
                    result.Add(groupeDTO);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans la requete getAll");
                Console.Write(e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return result;
        }

        public void update(GroupeDTO groupeDTO)
        {
            MySqlCommand cmd = new MySqlCommand(queryUpdate, connexion);
            cmd.Parameters.AddWithValue("@idgroupe", groupeDTO.IdGroupe);
            cmd.Parameters.AddWithValue("@idactivite", groupeDTO.IdActivite);
            cmd.Parameters.AddWithValue("@numerogroupe", groupeDTO.NumeroGroupe);
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

        public void delete(GroupeDTO groupeDTO)
        {
            MySqlCommand cmd = new MySqlCommand(queryDelete, connexion);
            cmd.Parameters.AddWithValue("@idgroupe", groupeDTO.IdGroupe); ;
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
        #endregion

        public void addSeance(SeanceDTO seanceDTO)
        {
            MySqlCommand cmd = new MySqlCommand(queryCreateSeance, connexion);
            cmd.Parameters.AddWithValue("@idgroupe", seanceDTO.IdGroupe);
            cmd.Parameters.AddWithValue("@datedebut", seanceDTO.DateDebut);
            cmd.Parameters.AddWithValue("@datefin", seanceDTO.DateFin);
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

        public List<SeanceDTO> getAllSeancesByGroupId(String idGroupe)
        {
            MySqlCommand cmd = new MySqlCommand(queryReadAllseances, connexion);
            MySqlDataReader reader = null;
            List<SeanceDTO> result = new List<SeanceDTO>();
            try
            {
                connexion.Open();
                cmd.Parameters.AddWithValue("@idgroupe", idGroupe);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SeanceDTO seanceDTO = new SeanceDTO();

                    seanceDTO.IdGroupe = reader.GetString("idgroupe");
                    seanceDTO.IdSeance = reader.GetString("idseance");
                    seanceDTO.DateDebut = reader.GetDateTime("datedebut");
                    seanceDTO.DateFin = reader.GetDateTime("datefin");

                    result.Add(seanceDTO);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans la requete getAllSeances");
                Console.Write(e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return result;
        }
    }
}
