using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CentreSportifLib.dto;

namespace CentreSportifGUI.Views.formulaire.formulairesMembre
{
    public partial class FormulaireRole : Form
    {
        public CentreSportifGUI CentreView;
        PersonneDTO p = new PersonneDTO();
        public FormulaireRole(PersonneDTO p)
        {
            InitializeComponent();
            this.p = p;
        }

        private void FormulaireRole_Load(object sender, EventArgs e)
        {
            CentreView = (CentreSportifGUI)this.Owner;
            String role;
            if (p.Role == "prof")
            {
                role = "professeur";
            }
            else
            {
                role = p.Role;
            }
            label1.Text = p.Prenom + " " + p.Nom + " est présentement " + role;            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Text = "Message : ";
            String newRole="membre";
            if (comboBox1.SelectedIndex == 0)
            {
                newRole = "membre";
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                newRole = "prof";
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                newRole = "admin";
            }
            try
            {
                p.Role = newRole;
                CentreView.DbCreateur.ServicePersonne.update(p);
                label3.Text += "Le role de " + p.Prenom + " " + p.Nom + " a bien été modifié.";
            }
            catch (Exception ee)
            {
                label3.Text += "Erreur dans le changement du role";
                Console.WriteLine("Erreur dans update role personne");
                Console.Write(ee.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
