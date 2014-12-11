﻿using System;
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
    public partial class FormulaireMembre : Form
    {
        PersonneDTO p;
        string mode;
        public FormulaireMembre(PersonneDTO p)
        {
            InitializeComponent();
            if (p != null)
            {

                this.mode= "Modifier";
                this.p = p;
                remplir();
            }
            else 
            {
                this.mode = "Créer";
                this.p = new PersonneDTO();
            }
            this.Text = mode;
        }

        private void button1_Click(object sender, EventArgs e) //enregistrer/modifier
        {
            //TODO: Gestion des erreurs
            p.IdPersonne = textBox1.Text;
            p.Prenom = textBox2.Text;
            p.Nom = textBox3.Text;
            p.Email = textBox4.Text;
            p.CodeBarre = textBox5.Text;


            AdresseDTO adresse = new AdresseDTO();
            adresse.Numero = textBox8.Text;
            adresse.Rue = textBox9.Text;
            adresse.CodePostal = textBox10.Text;
            adresse.Ville = textBox11.Text;
            adresse.IdPersonne = textBox1.Text;

            p.Adresse = adresse;

            if(radioButton2.Checked)
            {
                p.Sexe = 'F';
            }
            else
            {
                p.Sexe = 'M';
            }
            if(textBox6.Text.Equals(textBox7))
            {
                p.MotDePasse = textBox6.Text;
            }
            CentreSportifGUI owner = (CentreSportifGUI)this.Owner;
            if (this.mode.Equals("Créer"))
            {
                owner.DbCreateur.ServicePersonne.register(p);
                owner.DbCreateur.ServicePersonne.addAdresse(adresse);
            }
            else if (this.mode.Equals("Modifier"))
            {
                owner.DbCreateur.ServicePersonne.update(p);
                owner.DbCreateur.ServicePersonne.updateAdresse(adresse);
            }
        }

        private void button2_Click(object sender, EventArgs e) //annulé
        {
            this.Dispose();
        }

        private void remplir() 
        {
            textBox1.Text = p.IdPersonne;
            textBox2.Text = p.Prenom;
            textBox3.Text = p.Nom;
            textBox4.Text = p.Email;
            dateTimePicker1.Value = p.DateNaissance;

            if(p.Sexe.Equals('f'))
            {
                radioButton2.Checked = true;
            }
            
        }

        private void button3_Click(object sender, EventArgs e) //Changer 
        {
            //TODO
        }

        private void button4_Click(object sender, EventArgs e) //Captuer 
        {
            //TODO
        }
    }
}
