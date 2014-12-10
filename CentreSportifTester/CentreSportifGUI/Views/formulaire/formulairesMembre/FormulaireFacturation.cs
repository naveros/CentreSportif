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
            FormulairePayer form = new FormulairePayer(montant, "comptant", p);
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)//Payer debit
        {
            FormulairePayer form = new FormulairePayer(montant, "debit", p);
            form.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)//Payer Credit
        {
            FormulairePayer form = new FormulairePayer(montant, "Credit", p);
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
                List<AbonnementDTO> listAbo = owner.DbCreateur.ServicePersonne.getAllAbonnements(p);
                if (listAbo.Count > 0)
                {
                    listAbo.ForEach(delegate(AbonnementDTO abo)
                    {
                        solde += abo.Prix;

                    });
                }
                List<PaiementDTO> listPaiements = owner.DbCreateur.ServicePersonne.getAllPaiements(p);
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
    }
}
