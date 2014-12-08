using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Web.Security;
using Microsoft.AspNet.Membership.OpenAuth;

namespace CentreSportifSylvestre.Account
{
    public partial class Login : Page
    {
        String queryStr;
        String name;
        String role;
        String idpersonne;
        MySql.Data.MySqlClient.MySqlCommand cmd;
        MySql.Data.MySqlClient.MySqlDataReader reader;
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "~/Account/Register.aspx";
        }

        protected void LoginUser(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=db4free.net; Database=centresportif420; user=centresportif420; password=stephane420;";
            using (MySqlConnection cn = new MySqlConnection(connectionString))
            {
                cn.Open();
                queryStr = "SELECT * FROM centresportif420.personne WHERE codebarre='" + Server.HtmlEncode(((TextBox)(Login1.FindControl("UserName"))).Text) + "' AND motdepasse='" + Server.HtmlEncode(((TextBox)(Login1.FindControl("Password"))).Text) + "'";
                cmd = new MySql.Data.MySqlClient.MySqlCommand(queryStr, cn);
                reader = cmd.ExecuteReader();
                name = "";
                while (reader.HasRows && reader.Read())
                {
                    name = reader.GetString(reader.GetOrdinal("nom"));
                    role = reader.GetString(reader.GetOrdinal("role"));
                    idpersonne = reader.GetString(reader.GetOrdinal("idpersonne"));
                    Session["idpersonne"] = idpersonne;
                    Session["urole"] = role;
                }
                if(reader.HasRows)
                {
                    Session["uname"] = name;
                    Response.BufferOutput = true;
                    Response.Redirect("~/Account/Membre.aspx", false);
                    FormsAuthentication.SetAuthCookie(Server.HtmlEncode(((TextBox)(Login1.FindControl("UserName"))).Text), true);
                }
                else
                {
                    Response.Redirect("~/Account/Login.aspx", false);
                }
                reader.Close();
                cn.Close();
            }
        }
    }
}