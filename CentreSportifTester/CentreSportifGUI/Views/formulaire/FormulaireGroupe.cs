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
    public partial class FormulaireGroupe : Form
    {
        GroupeDTO g;
        string mode;
        public FormulaireGroupe(GroupeDTO g)
        {
            InitializeComponent();

            if (g != null)
            {

                this.mode = "Modifier";
                this.g = g;
                remplir();
            }
            else
            {
                this.mode = "Créer";
                this.g = new GroupeDTO();
            }
            this.Text = mode;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g.IdGroupe = textBox1.Text;
            g.IdActivite = textBox2.Text;
            g.NumeroGroupe = textBox3.Text;

            CentreSportifGUI owner = (CentreSportifGUI)this.Owner;
            if (this.mode.Equals("Créer"))
            {
                owner.sp.ServiceGroupe.creer(g);
            }
            else if (this.mode.Equals("Modifier"))
            {
                owner.sp.ServiceGroupe.update(g);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void remplir()
        {
            textBox1.Text = g.IdActivite;
            textBox2.Text = g.IdGroupe;
            textBox3.Text = g.NumeroGroupe;
        }
    }
}
