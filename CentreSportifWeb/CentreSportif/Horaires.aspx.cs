using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

namespace CentreSportif
{
    public partial class Horaires : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData();
        }
        private void BindData()
        {
            string connectionString = @"Data Source=db4free.net; Database=centresportif420; user=centresportif420; password=stephane420;";
            using (MySqlConnection cn = new MySqlConnection(connectionString))
            {
                MySqlDataAdapter adp = new MySqlDataAdapter(
                "SELECT DISTINCT g.numerogroupe, a.nom, s.datedebut, s.datefin, p.nom as name from groupe g, activite a, seance s, personne p, enseigne e " +
                "WHERE (p.role = 'prof') AND (p.idpersonne = e.idpersonne) AND (e.idgroupe = s.idgroupe) AND (s.idgroupe = g.idgroupe) AND (g.idactivite = a.idactivite) " +
                "limit 0,200", cn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    GVhoraire.DataSource = dt;
                    GVhoraire.DataBind();
                }
                cn.Close();
            }
        }
    }
}