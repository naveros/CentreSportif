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
        public CentreSportifGUI CentreView;
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
            label4.Text = "Message : ";
            try
            {
                g.IdGroupe = textBox1.Text;
                g.IdActivite = textBox2.Text;
                g.NumeroGroupe = textBox3.Text;


                if (this.mode.Equals("Créer"))
                {
                    CentreView.DbCreateur.ServiceGroupe.creer(g);
                    label4.Text += "Le groupe " + g.NumeroGroupe + " a bien été créer";
                    CentreView.RefreshTableGroupe();
                }
                else if (this.mode.Equals("Modifier"))
                {
                    CentreView.DbCreateur.ServiceGroupe.update(g);
                    label4.Text += "Le groupe " + g.NumeroGroupe + " a bien été modifier";
                    CentreView.RefreshTableGroupe();
                }
            }
            catch (Exception)
            {
                label4.Text = "Information incorrect";
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

        private void FormulaireGroupe_Load(object sender, EventArgs e)
        {
            CentreView = (CentreSportifGUI)this.Owner;
        }
    }
}
