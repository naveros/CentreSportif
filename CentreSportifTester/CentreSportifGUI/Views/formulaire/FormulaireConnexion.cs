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
        public FormulaireConnexion()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e) //Connexion manuel par ID ou par Code barre
        {

        }

        private void button1_Click(object sender, EventArgs e) //Retour
        {
            this.Dispose();
        }
    }
}
