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
using CentreSportifLib;

namespace CentreSportifGUI.Views.menu
{
    public partial class MenuMembre : Form
    {
          PersonneDTO p;

        public MenuMembre(PersonneDTO p)
        {
            InitializeComponent();
            this.p = p;
        }

        private void remplir() {
            labelNom.Text = p.Nom;
            labelPrenom.Text = p.Prenom;
            labelID.Text = p.IdPersonne;
        }
        private void button1_Click(object sender, EventArgs e)//horaire
        {

        }

        private void button2_Click(object sender, EventArgs e)//presence
        {

        }

        private void button7_Click(object sender, EventArgs e)//inscription
        {

        }

        private void button3_Click(object sender, EventArgs e)//facturation
        {

        }

        private void button4_Click(object sender, EventArgs e)//modifier
        {
            FormulaireMembre form = new FormulaireMembre(p);
            form.Owner = this.Owner;
            form.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)//supprimer
        {

        }

        private void button6_Click(object sender, EventArgs e)//quitter
        {
            this.Dispose();
        }


    }
}

