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
        PersonneDTO personneDTO = new PersonneDTO();
        public FormulaireRole(PersonneDTO personneDTO)
        {
            InitializeComponent();
            this.personneDTO = personneDTO;
        }

        private void FormulaireRole_Load(object sender, EventArgs e)
        {
            CentreView = (CentreSportifGUI)this.Owner;
            label1.Text = personneDTO.Prenom + " " + personneDTO.Nom + " est présentement " + CentreView.formatRole(personneDTO.Role);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Text = "Message : ";
            String newRole = "membre";
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
                personneDTO.Role = newRole;
                CentreView.DbCreateur.ServicePersonne.update(personneDTO);
                label3.Text += "Le role de " + personneDTO.Prenom + " " + personneDTO.Nom + " a bien été modifié.";
                label1.Text = personneDTO.Prenom + " " + personneDTO.Nom + " est présentement " + CentreView.formatRole(personneDTO.Role);
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
