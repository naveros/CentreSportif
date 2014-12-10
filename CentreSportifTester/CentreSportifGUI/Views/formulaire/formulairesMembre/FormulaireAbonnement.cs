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
    public partial class FormulaireAbonnement : Form
    {
        PersonneDTO p;
        ActiviteDTO activite;
        GroupeDTO groupe;
        CentreSportifGUI centre;
        List<SeanceDTO> seances;


        public FormulaireAbonnement(PersonneDTO p)
        {
            InitializeComponent();
            this.p = p;
            centre = (CentreSportifGUI)this.Owner;

            init();
        }
        private void init() {
            try
            {   //TODO FIX, + les noms des activites affiche pas dans l'onglet activite. 
                List<ActiviteDTO> activites = new List<ActiviteDTO>();
                activites = centre.DbCreateur.ServiceActivite.getAll();
                var bindingList = new BindingList<ActiviteDTO>(activites);
                var source = new BindingSource(bindingList, null);
                comboBox1.DataSource = source;
            }
           catch (Exception ee)
            {
                MessageBox.Show("Erreur dans la requete get all activitees");
                MessageBox.Show(ee.Message);

                //TO-DELETE , ITS FOR TESTS
                ActiviteDTO a = new ActiviteDTO();
              /*  a.Nom = "tst nom ";
                a.IdActivite = "1";
                comboBox1.Items.Add(a);
                comboBox1.Items.Add(a);
                comboBox1.Items.Add(a);*/

            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

             activite =  (ActiviteDTO)comboBox1.SelectedItem;

            int idActivite = int.Parse(activite.IdActivite);
            
            MessageBox.Show("Selected id : " +  activite.IdActivite + "\n" +
                            "nom " + activite.Nom);

            comboBox2.Enabled = true;
            try
            {
                List<GroupeDTO> groupes = centre.DbCreateur.ServiceGroupe.getAllByActivite(idActivite);
                var bindingList = new BindingList<GroupeDTO>(groupes);
                var source = new BindingSource(bindingList, null);
                comboBox2.DataSource = source;
            }
            catch (Exception ee)
            {
                Console.WriteLine("Erreur dans la requete get all groupe by activitee ID");
                Console.Write(ee.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                 groupe = (GroupeDTO)comboBox2.SelectedItem;


                seances= centre.DbCreateur.ServiceGroupe.getAllSeances(groupe);
                var bindingList = new BindingList<SeanceDTO>(seances);
                var source = new BindingSource(bindingList, null);
                dataGridView1.DataSource = source;
            }
            catch (Exception ee)
            {
                Console.WriteLine("Erreur dans la requete get all groupe by activitee ID");
                Console.Write(ee.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)//inscrire
        {
            AbonnementDTO a = new AbonnementDTO();
            try
            {
                a.IdPersonne = p.IdPersonne;
                a.IdGroupe = groupe.IdGroupe;
                a.DateInscription = new DateTime();
                a.Prix = (int.Parse(activite.Duree) * seances.Count);
                a.DateFin = new DateTime(); //TODO . a changer pour la date de fin

                centre.DbCreateur.ServicePersonne.addAbonnement(a);
            }
            catch (Exception ee)
            {
                Console.WriteLine("Erreur dans la requete get create new abonnement");
                Console.Write(ee.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)//annuler
        {
            this.Dispose();
        }

    }
}

