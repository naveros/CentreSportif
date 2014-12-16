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
    public partial class _Default : System.Web.UI.Page
    {
       protected void Page_Load(object sender, EventArgs e)
        {
           if (!IsPostBack)
            {
                if ((String)Session["uname"] != "" && (String)Session["urole"] == "membre" || (String)Session["urole"] == "administrateur")
                {
                    Response.Redirect("~/Account/Membre.aspx", false);
                }
                BindData();
            }
        }
        private void BindData()
        {
            string connectionString = @"Data Source=db4free.net; Database=centresportif420; user=centresportif420; password=stephane420;";
            using (MySqlConnection cn = new MySqlConnection(connectionString))
            {
                MySqlDataAdapter adp = new MySqlDataAdapter("select nom,duree,description from activite", cn);
                DataTable dt = new DataTable();
                adp.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    activiteD.DataSource = dt;
                    activiteD.DataBind();
                }
                cn.Close();
            }
        }
    }
}
