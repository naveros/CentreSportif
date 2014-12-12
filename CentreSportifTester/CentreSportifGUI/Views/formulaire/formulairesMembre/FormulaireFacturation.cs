using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CentreSportifLib.dto;

namespace CentreSportifGUI.Views.formulaire.formulairesMembre
{
    public partial class FormulaireFacturation : Form
    {
        PersonneDTO personne;
        decimal montant = 0;
        public CentreSportifGUI CentreView;
        public FormulaireFacturation(PersonneDTO personne)
        {
            InitializeComponent();
            this.personne = personne;
        }

        private void button6_Click(object sender, EventArgs e)//Payer comptant
        {
            FormulairePayer form = new FormulairePayer(montant, "comptant", personne);
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)//Payer debit
        {
            FormulairePayer form = new FormulairePayer(montant, "debit", personne);
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)//Payer Credit
        {
            FormulairePayer form = new FormulairePayer(montant, "Credit", personne);
            form.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e) //Annuler
        {
            this.Dispose();
        }
        public decimal CalculerSolde()
        {
            decimal solde = 0;
            try
            {
                List<AbonnementDTO> listAbo = CentreView.DbCreateur.ServicePersonne.getAllAbonnements(personne);
                if (listAbo.Count > 0)
                {
                    listAbo.ForEach(delegate(AbonnementDTO abonnement)
                    {
                        solde += abonnement.Prix;

                    });
                }
                List<PaiementDTO> listPaiements = CentreView.DbCreateur.ServicePersonne.getAllPaiements(personne);
                if (listPaiements.Count > 0)
                {
                    listPaiements.ForEach(delegate(PaiementDTO paiement)
                    {
                        solde -= paiement.Montant;

                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans la requete calculer solde");
                Console.Write(e.Message);
            }


            return solde;
        }

        private void FormulaireFacturation_Enter(object sender, EventArgs e) //TO TEST
        {
            textBox1.Text = "" + CalculerSolde();
        }

        private void FormulaireFacturation_Load(object sender, EventArgs e)
        { 
            CentreView = (CentreSportifGUI)this.Owner;
            montant = CalculerSolde();
            this.textBox1.Text = ""+montant;
        }
    }
}
