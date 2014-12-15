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
        ActiviteDTO activiteDTO;
        string mode;
        public CentreSportifGUI CentreView;
        public FormulaireActivite(ActiviteDTO activiteDTO)
        {
            InitializeComponent();
            if (activiteDTO != null)
            {
                this.mode = "Modifier";
                this.activiteDTO = activiteDTO;
                remplir();
            }
            else
            {
                this.mode = "Créer";
                this.activiteDTO = new ActiviteDTO();
            }
            this.Text = mode;
        }

        private void button5_Click(object sender, EventArgs e) //Valider
        {
            label4.Text = "Message : ";
            try
            {
                activiteDTO.IdActivite = textBox1.Text;
                activiteDTO.Nom = textBox2.Text;
                activiteDTO.Duree = textBox3.Text;
                activiteDTO.Description = richTextBox1.Text;

                if (this.mode.Equals("Créer"))
                {
                    CentreView.DbCreateur.ServiceActivite.creer(activiteDTO);
                    label4.Text += "L'activité " + activiteDTO.Nom + " a bien été créée";
                    CentreView.RefreshTableActivite();
                }
                else if (this.mode.Equals("Modifier"))
                {
                    CentreView.DbCreateur.ServiceActivite.modifier(activiteDTO);
                    label4.Text += "L'activité " + activiteDTO.Nom + " a bien été modifiée";
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

        public void remplir()
        {

            textBox1.Text = activiteDTO.IdActivite;
            textBox2.Text = activiteDTO.Nom;
            textBox3.Text = activiteDTO.Duree;
            richTextBox1.Text = activiteDTO.Description;
        }

        private void FormulaireActivite_Load(object sender, EventArgs e)
        {
            CentreView = (CentreSportifGUI)this.Owner;
        }
    }
}
