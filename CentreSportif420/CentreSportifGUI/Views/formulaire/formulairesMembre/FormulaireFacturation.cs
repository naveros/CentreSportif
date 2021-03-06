﻿using System;
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
        PersonneDTO personneDTO;
        decimal montantDue = 0;
        public CentreSportifGUI CentreView;
        public FormulaireFacturation(PersonneDTO personneDTO)
        {
            InitializeComponent();
            this.personneDTO = personneDTO;
        }

        private void button6_Click(object sender, EventArgs e)//Payer comptant
        {
            FormulairePayer formPayer = new FormulairePayer(montantDue, "comptant", personneDTO);
            formPayer.Owner = this.Owner;
            this.Dispose();
            formPayer.ShowDialog();

        }

        private void button4_Click(object sender, EventArgs e)//Payer debit
        {
            FormulairePayer formPayer = new FormulairePayer(montantDue, "debit", personneDTO);
            formPayer.Owner = this.Owner;
            this.Dispose();
            formPayer.ShowDialog();

        }

        private void button1_Click(object sender, EventArgs e)//Payer Credit
        {
            FormulairePayer formPayer = new FormulairePayer(montantDue, "Credit", personneDTO);
            formPayer.Owner = this.Owner;
            this.Dispose();
            formPayer.ShowDialog();

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
                List<AbonnementDTO> listAbonnementDTO = CentreView.DbCreateur.ServicePersonne.getAllAbonnements(personneDTO);
                if (listAbonnementDTO.Count > 0)
                {
                    listAbonnementDTO.ForEach(delegate(AbonnementDTO abonnementDTO)
                    {
                        solde += abonnementDTO.Prix;
                    });
                }
                List<PaiementDTO> listPaiements = CentreView.DbCreateur.ServicePersonne.getAllPaiements(personneDTO.IdPersonne);
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
            this.montantDue = CalculerSolde();
            this.textBox1.Text = "" + montantDue;
        }
    }
}
