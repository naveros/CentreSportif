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
        //TODO : Tests unitaires,, String vs Int pour les champs?? 

        MySqlConnection con;
        const String queryCreate = "INSERT INTO groupe (idactivite, numerogroupe) VALUES (@idactivite, @numerogroupe)";
        const String queryReadAll = "SELECT * FROM groupe";
        const String queryRead = "SELECT * FROM groupe WHERE idgroupe = @idgroupe";
        const String queryReadByActivite = "SELECT * FROM groupe WHERE idactivite = @idactivite";
        const String queryUpdate = "UPDATE groupe SET numerogroupe = @numerogroupe WHERE idgroupe=@idgroupe;";
        const String queryDelete = "DELETE FROM groupe WHERE idgroupe=@idgroupe;";
        const String queryReadAllseances = "SELECT * FROM seance WHERE idgroupe=@idgroupe;";
        public GroupeDAO(MySqlConnection connexion)
        {
            this.con = connexion;
        }

        #region CRUD Groupe
        public String add(GroupeDTO g)
        {
            String id = "null";
            MySqlCommand cmd = new MySqlCommand(queryCreate, con);
        //    cmd.Parameters.AddWithValue("@idgroupe", g.IdGroupe);
            cmd.Parameters.AddWithValue("@idactivite", g.IdActivite);
            cmd.Parameters.AddWithValue("@numerogroupe", g.NumeroGroupe);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                id = cmd.LastInsertedId.ToString();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally
            {
                con.Close();
            }
            return id;
        }

        public GroupeDTO get(GroupeDTO a)
        {
            MySqlCommand cmd = new MySqlCommand(queryRead, con);
            MySqlDataReader reader = null;
            GroupeDTO result = new GroupeDTO();
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@idgroupe", a.IdGroupe);
                reader = cmd.ExecuteReader();
                reader.Read();
                result.IdGroupe = reader.GetString("idgroupe");
                result.IdActivite = reader.GetString("idactivite");
                result.NumeroGroupe = reader.GetString("numerogroupe");
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

        public List<GroupeDTO> getAllByActivite(String idActivite) {

            MySqlCommand cmd = new MySqlCommand(queryReadByActivite, con);
            MySqlDataReader reader = null;
            List<GroupeDTO> result = new List<GroupeDTO>();
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@idactivite", idActivite);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    GroupeDTO a = new GroupeDTO();
                    a.IdGroupe = reader.GetString("idgroupe");
                    a.IdActivite = idActivite;
                    a.NumeroGroupe = reader.GetString("numerogroupe");
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

        public List<GroupeDTO> getAll()
        {
            MySqlCommand cmd = new MySqlCommand(queryReadAll, con);
            MySqlDataReader reader = null;
            List<GroupeDTO> result = new List<GroupeDTO>();
            try
            {
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    GroupeDTO a = new GroupeDTO();
                    a.IdGroupe = reader.GetString("idgroupe");
                    a.IdActivite = reader.GetString("idactivite");
                    a.NumeroGroupe = reader.GetString("numerogroupe");
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

        public void update(GroupeDTO a)
        {
            MySqlCommand cmd = new MySqlCommand(queryUpdate, con);
            cmd.Parameters.AddWithValue("@idgroupe", a.IdGroupe);
            cmd.Parameters.AddWithValue("@idactivite", a.IdActivite);
            cmd.Parameters.AddWithValue("@numerogroupe", a.NumeroGroupe);
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

        public void delete(GroupeDTO a)
        {
            MySqlCommand cmd = new MySqlCommand(queryDelete, con);
            cmd.Parameters.AddWithValue("@idgroupe", a.IdGroupe); ;
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

        public List<SeanceDTO> getAllSeances(GroupeDTO g) 
        {
            MySqlCommand cmd = new MySqlCommand(queryReadAllseances, con);
            MySqlDataReader reader = null;
            List<SeanceDTO> result = new List<SeanceDTO>();
            try
            {
                con.Open();
                cmd.Parameters.AddWithValue("@idgroupe", g.IdGroupe);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SeanceDTO s = new SeanceDTO();

                    s.IdGroupe = reader.GetString("idgroupe");
                    s.IdSeance = reader.GetString("idseance");
                    s.DateDebut = reader.GetDateTime("datedebut");
                    s.DateFin = reader.GetDateTime("datefin");

                    result.Add(s);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans la requete getAllSeances");
                Console.Write(e.Message);
            }
            finally
            {
                con.Close();
            }
            return result;

        }

    }
}
