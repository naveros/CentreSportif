using CentreSportifLib.dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CentreSportifGUI.Views.formulaire
{
    public partial class FormulaireConnexion : Form
    {
        CentreSportifGUI centre;
        public FormulaireConnexion()
        {
            InitializeComponent();
            centre = (CentreSportifGUI) this.Owner;


            try
            {
                List<PersonneDTO> personnes = centre.sp.ServicePersonne.getAll();
                var bindingList = new BindingList<PersonneDTO>(personnes);
                var source = new BindingSource(bindingList, null);
                comboBox1.DataSource = source;
            }
            catch (Exception ee)
            {
                Console.WriteLine("Erreur dans la requete get all personnes");
                Console.Write(ee.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e) //Connexion manuel par ID ou par Code barre
        {
            if(textBox1.Text.Length > 0)//Connexion par idpersonne
            {
                PersonneDTO tmp = new PersonneDTO();
                tmp.IdPersonne = textBox1.Text;
                centre.connexionAccueil(centre.sp.ServicePersonne.findById(tmp));
                centre.connexionAccueil((PersonneDTO)comboBox1.SelectedItem);
            }
            else if (textBox2.Text.Length > 0)//Connexion par code barre
            {
                //TODO
            }
        }

        private void button1_Click(object sender, EventArgs e) //Retour
        {
            this.Dispose();
        }
    }
}
