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
        ActiviteDTO a;
        public CentreSportifGUI CentreView;
        public MenuActivite(ActiviteDTO a)
        {
            InitializeComponent();
            this.a = a;
            
        }

        private void remplir()
        {
            labelID.Text = a.IdActivite;
            labelDescription.Text = a.Description;
            labelNom.Text = a.Nom;
            labelDuree.Text = a.Duree;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            FormulaireActivite formActivite = new FormulaireActivite(a);
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
                    CentreView.DbCreateur.ServiceActivite.delete(a);
                    labelMessage.Text = "L'activité à bien été supprimé";
                    CentreView.RefreshTableActivite();
                }
                catch (Exception ee)
                {
                    Console.WriteLine("Erreur dans la requete delete activite");
                    Console.Write(ee.Message);
                }


            }
            else
            {
                // Does nothing
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
