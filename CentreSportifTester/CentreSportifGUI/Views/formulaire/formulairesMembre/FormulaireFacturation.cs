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
        PersonneDTO p;
        decimal montant = 0;
        CentreSportifGUI owner;
        public FormulaireFacturation(PersonneDTO p)
        {
            InitializeComponent();
            this.p = p;
            owner = (CentreSportifGUI)this.Owner;
            montant = CalculerSolde();
            

        }

        private void button6_Click(object sender, EventArgs e)//Payer comptant
        {
            FormulairePaye form = new FormulairePaye(montant, "comptant");
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)//Payer debit
        {
            FormulairePaye form = new FormulairePaye(montant, "debit");
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)//Payer Credit
        {
            FormulairePaye form = new FormulairePaye(montant, "Credit");
            form.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e) //Confirmer
        {

        }

        private void button3_Click(object sender, EventArgs e) //Annuler
        {
            this.Dispose();
        }
        public decimal CalculerSolde()
        {
            decimal solde=0;

            List<AbonnementDTO> listAbo = owner.sp.ServicePersonne.getAllAbonnements(p);
            listAbo.ForEach(delegate(AbonnementDTO abo) 
            {
                solde += abo.Prix;
            
            });

            List<PaiementDTO> listPaiements = owner.sp.ServicePersonne.getAllPaiements(p);
            listPaiements.ForEach(delegate(PaiementDTO paiement)
            {
                solde -= paiement.Montant;

            });
            
            
            
            
            return solde;
        }
    }
}
