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
        
        PersonneDTO p;
        decimal montantDue;
        String mode;
        public CentreSportifGUI CentreView;
        public FormulairePayer(decimal montantDue, String mode, PersonneDTO p)
        {
            InitializeComponent();
            this.p = p;
            this.montantDue = montantDue;
            this.mode = mode;
            this.textBox1.Text = ""+montantDue;
            this.textBox2.Text = mode;
        }
        

        private void button1_Click(object sender, EventArgs e)//Payer
        {
            this.label4.Text = "Message : ";
            try
            {
                decimal paiement = decimal.Parse(this.textBox3.Text);
                if (paiement < montantDue)
                {
                    PaiementDTO paiementDTO = new PaiementDTO();
                    paiementDTO.IdPersonne = p.IdPersonne;
                    paiementDTO.Mode = mode;
                    paiementDTO.Montant = paiement;
                    CentreView.DbCreateur.ServicePersonne.addPaiement(paiementDTO);
                    this.textBox1.Text = ""+ (montantDue - paiementDTO.Montant);
                    this.label4.Text = "Paiement de "+paiementDTO.Montant+"$  effectué ! Merci";

                }
                else {
                    this.label4.Text = "Veiller entrer une plus petit montant que celui dût. ";
                }

                
               
            }
            catch (Exception ee)
            {
                this.label4.Text = "Erreur lors du paiement";
                Console.WriteLine("Erreur dans la requete add paiement");
                Console.Write(ee.Message);
            }


        }

        private void button2_Click(object sender, EventArgs e)//annuler
        {
            this.Dispose();
        }

        private void FormulairePayer_Load(object sender, EventArgs e)
        {
            //////////////////////////
            CentreView = (CentreSportifGUI)this.Owner;
        }
    }
}
