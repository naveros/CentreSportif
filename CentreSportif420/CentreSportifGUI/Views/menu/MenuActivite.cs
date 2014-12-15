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

namespace CentreSportifGUI.Views.menu
{
    public partial class MenuActivite : Form
    {
        ActiviteDTO activiteDTO;
        public CentreSportifGUI CentreView;
        public MenuActivite(ActiviteDTO activiteDTO)
        {
            InitializeComponent();
            this.activiteDTO = activiteDTO;
        }

        private void remplir()
        {
            labelID.Text = activiteDTO.IdActivite;
            labelDescription.Text = activiteDTO.Description;
            labelNom.Text = activiteDTO.Nom;
            labelDuree.Text = activiteDTO.Duree;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormulaireActivite formActivite = new FormulaireActivite(activiteDTO);
            formActivite.Owner = this.Owner;
            formActivite.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Êtes-vous certain de vouloir supprimer cette activité ? ",
                                       "Confirmer la suppression d'un activité",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                CentreView = (CentreSportifGUI)this.Owner;
                try
                {
                    CentreView.DbCreateur.ServiceActivite.delete(activiteDTO);
                    labelMessage.Text = "L'activité à bien été supprimé";
                    CentreView.RefreshTableActivite();
                    button1.Enabled = false;
                    button2.Enabled = false;
                }
                catch (Exception ee)
                {
                    Console.WriteLine("Erreur dans la requete delete activite");
                    Console.Write(ee.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void MenuActivite_Load(object sender, EventArgs e)
        {
            CentreView = (CentreSportifGUI)this.Owner;
            remplir();
        }
    }
}
