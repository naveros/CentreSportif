using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Membership.OpenAuth;
using MySql.Data.MySqlClient;

namespace CentreSportifSylvestre.Account
{
    public partial class Register : Page
    {
        String queryStr;
        MySql.Data.MySqlClient.MySqlCommand cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }

        protected void registerUser(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=db4free.net; Database=centresportif420; user=centresportif420; password=stephane420;";
            using (MySqlConnection cn = new MySqlConnection(connectionString))
            {
                cn.Open();
                queryStr = "INSERT INTO centresportif420.personne (prenom, nom, sexe, datenaissance, email, motdepasse, codebarre, role)" + " VALUES ('" +
                   Server.HtmlEncode(((TextBox)(RegisterUserWizardStep.ContentTemplateContainer.FindControl("Nom"))).Text) + "', '" +
                   Server.HtmlEncode(((TextBox)(RegisterUserWizardStep.ContentTemplateContainer.FindControl("Prenom"))).Text) + "', '" +
                   Server.HtmlEncode(((TextBox)(RegisterUserWizardStep.ContentTemplateContainer.FindControl("Sexe"))).Text) + "', '" +
                   Server.HtmlEncode(((TextBox)(RegisterUserWizardStep.ContentTemplateContainer.FindControl("Naissance"))).Text) + "', '" +
                   Server.HtmlEncode(((TextBox)(RegisterUserWizardStep.ContentTemplateContainer.FindControl("Email"))).Text) + "', '" +
                   Server.HtmlEncode(((TextBox)(RegisterUserWizardStep.ContentTemplateContainer.FindControl("Password"))).Text) + "', '" +
                   Server.HtmlEncode(((TextBox)(RegisterUserWizardStep.ContentTemplateContainer.FindControl("UserName"))).Text) + "', 'membre')";
                cmd = new MySql.Data.MySqlClient.MySqlCommand(queryStr, cn);
                cmd.ExecuteReader();
                cn.Close();
                Console.Write(queryStr);
                Session["urole"] = "membre";
            }
            FormsAuthentication.SetAuthCookie(RegisterUser.UserName, createPersistentCookie: false);

            string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            if (!OpenAuth.IsLocalUrl(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
        }
    }
}