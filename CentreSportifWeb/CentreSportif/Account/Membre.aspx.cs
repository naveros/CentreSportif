using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

namespace CentreSportif.Account
{
    public partial class Membre : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((String)(Session["uname"]) == "")
            {
                Session.Remove("uname");
                Session.Remove("urole");
                Session.Remove("idpersonne");
                Response.Redirect("~/Default.aspx", true);
            }
            else
            {
                string connectionString = @"Data Source=db4free.net; Database=centresportif420; user=centresportif420; password=stephane420;";
                using (MySqlConnection cn = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter adp = new MySqlDataAdapter("select idgroupe,dateinscription,datefin,prix from abonnement WHERE idpersonne='" + (String)(Session["idpersonne"]) + "'", cn);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        vosActivite.DataSource = dt;
                        vosActivite.DataBind();
                    }
                    cn.Close();
                }
                using (MySqlConnection cn = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter adp = new MySqlDataAdapter("select prenom,nom,sexe,datenaissance,email,role from personne WHERE idpersonne='" + (String)(Session["idpersonne"]) + "'", cn);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        monProfile.DataSource = dt;
                        monProfile.DataBind();
                    }
                    cn.Close();
                }
                using (MySqlConnection cn = new MySqlConnection(connectionString))
                {
                    MySqlDataAdapter adp = new MySqlDataAdapter("select nom,duree,description,idactivite from activite", cn);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        activiteDisponible.DataSource = dt;
                        activiteDisponible.DataBind();
                    }
                    cn.Close();
                }
            }
        }

        protected void InscriptionActivite(object sender, GridViewCommandEventArgs e)
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 100);
            DateTime date = DateTime.Now;
            if (e.CommandName == "Inscription")
            {
                String queryStr;
                MySql.Data.MySqlClient.MySqlCommand cmd;
                String connectionString = @"Data Source=db4free.net; Database=centresportif420; user=centresportif420; password=stephane420;";
                using (MySqlConnection cn = new MySqlConnection(connectionString))
                {
                    cn.Open();
                    queryStr = "INSERT INTO centresportif420.abonnement (idpersonne, idgroupe, dateinscription, datefin, prix) VALUES ('" + (String)(Session["idpersonne"]) + "', '62', '2014-12-16', '2015-06-16', '" + 50 + "')";
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(queryStr, cn);
                    cmd.ExecuteReader();
                    cn.Close();
                }
            }
        }
        public static double DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
        }
    }
}