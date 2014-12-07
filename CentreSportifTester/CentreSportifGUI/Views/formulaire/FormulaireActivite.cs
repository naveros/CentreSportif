using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CentreSportifLib.dto;

namespace CentreSportifGUI.Views.formulaire
{
    public partial class FormulaireActivite : Form
    {
        ActiviteDTO a;
        string mode;
        public FormulaireActivite(ActiviteDTO a)
        {
            InitializeComponent();

            if (a != null)
            {

                this.mode = "Modifier";
                this.a = a;
                remplir();
            }
            else
            {
                this.mode = "Créer";
                this.a = new ActiviteDTO();
            }
            this.Text = mode;
        }

        private void button5_Click(object sender, EventArgs e) //Valider
        {

        }

        private void button1_Click(object sender, EventArgs e) //Annuler 
        {

        }

        public void remplir() { }
    }
}
