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
        }

        private void button2_Click(object sender, EventArgs e) //Modifier
        {
            //TODO Formulaire inscription/modifier
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
                    owner.sp.ServiceGroupe.delete(g);
                    labelMessage.Text = "Le groupe à bien été supprimé";
                    owner.RefreshTableGroupe();
                }
                catch (Exception)
                {

                    throw;
                }

            }
            else
            {
                // Does nothing
            }


        }
        private void button3_Click(object sender, EventArgs e) //Quitter
        {
            this.Dispose();
        }


    }
}
