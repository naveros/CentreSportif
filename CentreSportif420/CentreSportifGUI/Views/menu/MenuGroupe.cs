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
using CentreSportifGUI.Views.formulaire.formulaireGroupe;


namespace CentreSportifGUI.Views.menu
{
    public partial class MenuGroupe : Form
    {
        GroupeDTO groupeDTO;
        CentreSportifGUI owner;
        public MenuGroupe(GroupeDTO groupeDTO)
        {
            InitializeComponent();
            this.groupeDTO = groupeDTO;
            remplir();
        }

        private void remplir()
        {
            labelIDActivite.Text = groupeDTO.IdActivite;
            labelIDGroupe.Text = groupeDTO.IdGroupe;
            labelNumeroGroupe.Text = groupeDTO.NumeroGroupe;
            labelPrix.Text = groupeDTO.Prix.ToString();
        }

        private void button2_Click(object sender, EventArgs e) //Modifier
        {
            FormulaireGroupe form = new FormulaireGroupe(groupeDTO);
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
                    owner.DbCreateur.ServiceGroupe.delete(groupeDTO);
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

        private void button4_Click(object sender, EventArgs e)
        {
            ViewGroupe form = new ViewGroupe(groupeDTO);
            form.Owner = this.Owner;
            form.ShowDialog();
        }
        private void button3_Click(object sender, EventArgs e) //Quitter
        {
            this.Dispose();
        }
    }
}
