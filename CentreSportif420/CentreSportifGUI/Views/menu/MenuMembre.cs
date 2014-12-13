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
        PersonneDTO personneDTO;
        public CentreSportifGUI CentreView;
        public MenuMembre(PersonneDTO p)
        {
            InitializeComponent();
            this.personneDTO = p;
            remplir();
        }

        private void remplir()
        {
            labelNom.Text = personneDTO.Nom;
            labelPrenom.Text = personneDTO.Prenom;
            labelID.Text = personneDTO.IdPersonne;
        }
        private void button1_Click(object sender, EventArgs e)//horaire
        {
            //TODO form view horaire des seances d'apres les groupes, d'apres les abonnements ! understood?
        }


        private void button7_Click(object sender, EventArgs e)//inscription
        {
            //TODO form inscription à un abonnement 
            FormulaireAbonnement form = new FormulaireAbonnement(personneDTO);
            form.Owner = this.Owner;
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)//facturation
        {
            FormulaireFacturation formFacturation = new FormulaireFacturation(personneDTO);
            formFacturation.Owner = this.Owner;
            formFacturation.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)//modifier
        {
            FormulaireMembre form = new FormulaireMembre(personneDTO);
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
                try
                {
                    CentreView.DbCreateur.ServicePersonne.delete(personneDTO);
                    labelMessage.Text = "Le membre à bien été supprimé";
                    CentreView.RefreshTableMembre();
                    button1.Enabled = false;
                    button3.Enabled = false;
                    button4.Enabled = false;
                    button5.Enabled = false;
                    button7.Enabled = false;
                    button8.Enabled = false;
                }
                catch (Exception ee)
                {
                    Console.WriteLine("Erreur dans la requete delete personne");
                    Console.Write(ee.Message);
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

        private void button8_Click(object sender, EventArgs e)
        {
            FormulaireRole formRole = new FormulaireRole(personneDTO);
            formRole.Owner = this.Owner;
            formRole.ShowDialog();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            FormulaireMessage formRole = new FormulaireMessage(personneDTO);
            formRole.Owner = this.Owner;
            formRole.ShowDialog();
        }



        private void button2_Click_1(object sender, EventArgs e) //Ajouter message
        {
            FormulaireMessage formMessage = new FormulaireMessage(personneDTO);
            formMessage.Owner = this.Owner;
            formMessage.ShowDialog();
        }
        private void MenuMembre_Load(object sender, EventArgs e)
        {
            CentreView = (CentreSportifGUI)this.Owner;
        }
    }
}

