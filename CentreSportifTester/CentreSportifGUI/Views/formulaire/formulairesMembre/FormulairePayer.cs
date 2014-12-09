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
    public partial class FormulairePayer : Form
    {
        CentreSportifGUI owner;
        PersonneDTO p;
        decimal montant;
        String mode;
        public FormulairePayer(decimal montant, String mode, PersonneDTO p)
        {
            InitializeComponent();
            this.p = p;
            this.montant = montant;
            this.mode = mode;
            owner = (CentreSportifGUI)this.Owner;
            this.textBox1.Text = ""+montant;
            this.textBox2.Text = mode;
        }
        

        private void button1_Click(object sender, EventArgs e)//Payer
        {
            try
            {
                decimal paiement = decimal.Parse(this.textBox3.Text);
                if (paiement < montant)
                {
                    PaiementDTO paiementDTO = new PaiementDTO();
                    paiementDTO.Date = new DateTime();
                    paiementDTO.IdPersonne = p.IdPersonne;
                    paiementDTO.Mode = mode;
                    paiementDTO.Montant = paiement;
                    owner.sp.ServicePersonne.addPaiement(paiementDTO);
                    this.label4.Text = "Paiement effectué ! Merci";
                }
                else {
                    this.label4.Text = "Veiller entrer une plus petit montant que celui dût. ";
                }

                
               
            }
            catch (Exception ee)
            {
                Console.WriteLine("Erreur dans la requete add paiement");
                Console.Write(ee.Message);
            }


        }

        private void button2_Click(object sender, EventArgs e)//annuler
        {
            this.Dispose();
        }
    }
}
