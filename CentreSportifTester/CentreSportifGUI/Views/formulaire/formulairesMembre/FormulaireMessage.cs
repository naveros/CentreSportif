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
    public partial class FormulaireMessage : Form
    {
        public CentreSportifGUI CentreView;
        PersonneDTO personne;
        public FormulaireMessage(PersonneDTO personne)
        {
            InitializeComponent();
            this.personne = personne;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String contenu = this.textBox1.Text;
                MessageDTO messageDTO = new MessageDTO();
                messageDTO.Contenu = contenu;
                messageDTO.IdPersonne = personne.IdPersonne;
                CentreView.DbCreateur.ServicePersonne.addMessage(messageDTO);
                CentreView.AfficherMessage(messageDTO);
                this.Dispose();
            }
            catch (Exception ee)
            {
                Console.WriteLine("Erreur dans la requete ajout message ");
                Console.Write(ee.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FormulaireMessage_Load(object sender, EventArgs e)
        {

            CentreView = (CentreSportifGUI)this.Owner;
        }


    }
}
