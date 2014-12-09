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
using CentreSportifGUI.Views.formulaire.formulairesMembre;

namespace CentreSportifGUI.Views.menu
{
    public partial class MenuMembre : Form
    {
          PersonneDTO p;
          CentreSportifGUI owner;
        public MenuMembre(PersonneDTO p)
        {
            InitializeComponent();
            this.p = p;
            remplir();
        }

        private void remplir() {
            labelNom.Text = p.Nom;
            labelPrenom.Text = p.Prenom;
            labelID.Text = p.IdPersonne;
        }
        private void button1_Click(object sender, EventArgs e)//horaire
        {
            //TODO form view horaire des seances d'apres les groupes, d'apres les abonnements ! understood?
        }


        private void button7_Click(object sender, EventArgs e)//inscription
        {
            //TODO form inscription à un abonnement 
            FormulaireAbonnement form = new FormulaireAbonnement(p);
            //form.Owner = this.Owner;
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)//facturation
        {
            FormulaireFacturation form = new FormulaireFacturation(p);
            form.Owner = this.Owner;
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)//modifier
        {
            FormulaireMembre form = new FormulaireMembre(p);
            form.Owner = this.Owner;
            form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)//supprimer
        {
            

             var confirmResult = MessageBox.Show("Êtes-vous certain de vouloir supprimer ce membre ? ",
                                        "Confirmer la suppression d'un membre",
                                      MessageBoxButtons.YesNo);
             if (confirmResult == DialogResult.Yes)
             {
                 owner = (CentreSportifGUI)this.Owner;
                 try
                 {
                     owner.sp.ServicePersonne.delete(p);
                     labelMessage.Text = "Le membre à bien été supprimé";
                     owner.RefreshTableMembre();
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

        private void button6_Click(object sender, EventArgs e)//quitter
        {
            this.Dispose();
        }


    }
}

