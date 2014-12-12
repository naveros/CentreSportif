using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CentreSportifLib.dto;
using CentreSportifGUI.Views.formulaire;


namespace CentreSportifGUI.Views.menu
{
    public partial class MenuGroupe : Form
    {
        GroupeDTO g;
        CentreSportifGUI owner;
        public MenuGroupe(GroupeDTO g)
        {
            InitializeComponent();
            this.g = g;
            remplir();
        }

        private void remplir()
        {
            labelIDActivite.Text = g.IdActivite;
            labelIDGroupe.Text = g.IdGroupe;
            labelNumeroGroupe.Text = g.NumeroGroupe;
            labelPrix.Text = g.Prix.ToString();
        }

        private void button2_Click(object sender, EventArgs e) //Modifier
        {
            //TODO Formulaire inscription/modifier
            FormulaireGroupe form = new FormulaireGroupe(g);
            form.Owner = this.Owner;
            form.ShowDialog();

        }
        private void button1_Click(object sender, EventArgs e) //Supprimer
        {

            var confirmResult = MessageBox.Show("Êtes-vous certain de vouloir supprimer ce groupe ? ",
                                       "Confirmer la suppression d'un groupe",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                owner = (CentreSportifGUI)this.Owner;
                try
                {
                    owner.DbCreateur.ServiceGroupe.delete(g);
                    labelMessage.Text = "Le groupe à bien été supprimé";
                    owner.RefreshTableGroupe();
                    button1.Enabled = false;
                    button2.Enabled = false;
                    button4.Enabled = false;
                }
                catch (Exception ee)
                {
                    Console.WriteLine("Erreur dans la requete delete groupe");
                    Console.Write(ee.Message);
                }


            }


        }
        private void button3_Click(object sender, EventArgs e) //Quitter
        {
            this.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


    }
}
