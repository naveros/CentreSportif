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
        public CentreSportifGUI CentreView;
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
            label4.Text = "Message : ";
            try
            {
                a.IdActivite = textBox1.Text;
                a.Nom = textBox2.Text;
                a.Duree= textBox3.Text;
                a.Description = richTextBox1.Text;

                if (this.mode.Equals("Créer"))
                {
                    CentreView.DbCreateur.ServiceActivite.creer(a);
                    label4.Text += "L'activité " + a.Nom+ " a bien été créée";
                    CentreView.RefreshTableActivite();
                }
                else if (this.mode.Equals("Modifier"))
                {
                    CentreView.DbCreateur.ServiceActivite.modifier(a);
                    label4.Text += "L'activité " + a.Nom + " a bien été modifiée";
                    CentreView.RefreshTableActivite();
                }
            }
            catch (Exception)
            {
                label4.Text = "Informations incorrectes";
            }
        }

        private void button1_Click(object sender, EventArgs e) //Annuler 
        {
            this.Dispose();
        }

        public void remplir() {

            textBox1.Text = a.IdActivite;
            textBox2.Text = a.Nom;
            textBox3.Text = a.Duree;
            richTextBox1.Text = a.Description;
        }

        private void FormulaireActivite_Load(object sender, EventArgs e)
        {
            CentreView = (CentreSportifGUI)this.Owner;
        }
    }
}
