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
       public CentreSportifGUI CentreView ;//= new CentreSportifGUI();
        public FormulaireConnexion()
        {
            InitializeComponent();
           // CentreView = (CentreSportifGUI) this.Owner;



        }


        private void button5_Click(object sender, EventArgs e) //Connexion manuel par ID ou par Code barre
        {
            if (textBox1.TextLength == 0 && textBox2.TextLength == 0) //Connexion par listeDéroulante
            {
                CentreView.connexionAccueil((PersonneDTO)comboBox1.SelectedItem);
            }
            else if(textBox1.Text.Length > 0)//Connexion par idpersonne
            {
                
                CentreView = (CentreSportifGUI) this.Owner;
            
                //CentreView.connexionAccueil(CentreView.DbCreateur.ServicePersonne.findById(tmp));
               if (textBox1.TextLength == 0 && textBox2.TextLength == 0)
               {
                   CentreView.connexionAccueil((PersonneDTO)comboBox1.SelectedItem);
               }
               else
               {
                   try
                   {
                       PersonneDTO tmp = new PersonneDTO();
                       String idMembre = textBox1.Text;
                       tmp.IdPersonne = textBox1.Text;
                       tmp = CentreView.DbCreateur.ServicePersonne.findById(tmp);
                       CentreView.connexionAccueil(tmp);
                   }
                   catch (Exception ee)
                   {
                       Console.WriteLine("Erreur dans la requete get all personnes");
                       Console.Write(ee.Message);
                   }
               }
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

        private void FormulaireConnexion_Load(object sender, EventArgs e)
        {
            CentreView = (CentreSportifGUI)this.Owner;

            try
            {
                List<PersonneDTO> personnes = CentreView.DbCreateur.ServicePersonne.getAll();
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
    }
}
