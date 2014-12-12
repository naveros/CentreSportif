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
    public partial class FormulaireAbonnement : Form
    {
        PersonneDTO p;
        ActiviteDTO activite;
        GroupeDTO groupe;
        List<SeanceDTO> seances;
        SeanceDTO lastSeance;
        public CentreSportifGUI CentreView;

        public FormulaireAbonnement(PersonneDTO p)
        {
            InitializeComponent();
            this.p = p;


        }
        private void init()
        {
            try
            {
                List<ActiviteDTO> activites = new List<ActiviteDTO>();
                activites = CentreView.DbCreateur.ServiceActivite.getAll();
                var bindingList = new BindingList<ActiviteDTO>(activites);
                var source = new BindingSource(bindingList, null);
                comboBox1.DataSource = source;
            }
            catch (Exception ee)
            {
                MessageBox.Show("Erreur dans la requete get all activitees");
                MessageBox.Show(ee.Message);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            activite = (ActiviteDTO)comboBox1.SelectedItem;

            String idActivite = activite.IdActivite;

            //    MessageBox.Show("Selected id : " +  activite.IdActivite + "\n" +
            //                  "nom " + activite.Nom);

            comboBox2.Enabled = true;
            try
            {
                List<GroupeDTO> groupes = CentreView.DbCreateur.ServiceGroupe.getAllByActivite(idActivite);
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
                this.dataGridView1.Rows.Clear();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                label6.Text = "Message : ";
                seances = null;
                lastSeance = null;
                groupe = null;
                groupe = (GroupeDTO)comboBox2.SelectedItem;

                EnseigneDTO enseigneDTO = CentreView.DbCreateur.ServicePersonne.getEnseigneByGroupId(groupe.IdGroupe);
                PersonneDTO professeur = CentreView.DbCreateur.ServicePersonne.findById(int.Parse(enseigneDTO.IdPersonne));


                this.dataGridView1.Rows.Clear();
                seances = CentreView.DbCreateur.ServiceGroupe.getAllSeancesByGroupId(groupe.IdGroupe);
                lastSeance = seances.Last();

                textBox1.Text = "" + seances.Count;
                textBox2.Text = lastSeance.DateFin.Day + " / " + lastSeance.DateFin.Month + " / " + lastSeance.DateFin.Year;
                textBox3.Text = "" + groupe.Prix;

                
                seances.ForEach(delegate(SeanceDTO seance)
                {
                    int i = this.dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = seance.DateDebut.DayOfWeek;
                    dataGridView1.Rows[i].Cells[1].Value = seance.DateDebut.Hour + "h";
                    dataGridView1.Rows[i].Cells[2].Value = seance.DateFin.Hour + "h";
                    dataGridView1.Rows[i].Cells[3].Value = professeur.Prenom + " " + professeur.Nom;

                });
            }
            catch (Exception ee)
            {
                label6.Text += "Erreur dans le chargement des séances du groupe " + groupe.NumeroGroupe;
                Console.WriteLine("Erreur dans la requete get all seances");
                Console.Write(ee.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)//inscrire
        {
            label6.Text = "Message : ";
            AbonnementDTO abonnement = new AbonnementDTO();
            try
            {
                abonnement.IdPersonne = p.IdPersonne;
                abonnement.IdGroupe = groupe.IdGroupe;
                abonnement.DateInscription = new DateTime();
                abonnement.Prix = groupe.Prix;
                abonnement.DateFin = lastSeance.DateFin; //TODO . a changer pour la date de fin

                CentreView.DbCreateur.ServicePersonne.addAbonnement(abonnement);
                label6.Text += "Ajout de l'abonnement réussit ! ";
            }
            catch (Exception ee)
            {
                label6.Text += "Erreur dans l'ajout de l'abonnement. Le membre est-il déjà inscrit? ";
                Console.WriteLine("Erreur dans la requete get create new abonnement");
                Console.Write(ee.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)//annuler
        {
            this.Dispose();
        }

        private void FormulaireAbonnement_Load(object sender, EventArgs e)
        {
            CentreView = (CentreSportifGUI)this.Owner;
            init();
        }

    }
}

