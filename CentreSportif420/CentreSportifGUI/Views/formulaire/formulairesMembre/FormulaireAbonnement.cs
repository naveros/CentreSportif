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
        PersonneDTO personneDTO;
        ActiviteDTO activiteDTO;
        GroupeDTO groupeDTO;
        List<SeanceDTO> listSeanceDTO;
        SeanceDTO lastSeanceDTO;
        public CentreSportifGUI CentreView;

        public FormulaireAbonnement(PersonneDTO personneDTO)
        {
            InitializeComponent();
            this.personneDTO = personneDTO;
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
                Console.WriteLine("Erreur dans la requete get all activites");
                Console.Write(ee.Message);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            activiteDTO = (ActiviteDTO)comboBox1.SelectedItem;
            String idActivite = activiteDTO.IdActivite;
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
                groupeDTO = null;
                listSeanceDTO = null;
                lastSeanceDTO = null;

                groupeDTO = (GroupeDTO)comboBox2.SelectedItem;
                EnseigneDTO enseigneDTO = CentreView.DbCreateur.ServicePersonne.getEnseigneByGroupId(groupeDTO.IdGroupe);
                PersonneDTO professeur = CentreView.DbCreateur.ServicePersonne.findById(int.Parse(enseigneDTO.IdPersonne));

                this.dataGridView1.Rows.Clear();
                listSeanceDTO = CentreView.DbCreateur.ServiceGroupe.getAllSeancesByGroupId(groupeDTO.IdGroupe);
                lastSeanceDTO = listSeanceDTO.Last();

                textBox1.Text = "" + listSeanceDTO.Count;
                textBox2.Text = lastSeanceDTO.DateFin.Day + " / " + lastSeanceDTO.DateFin.Month + " / " + lastSeanceDTO.DateFin.Year;
                textBox3.Text = "" + groupeDTO.Prix;

                listSeanceDTO.ForEach(delegate(SeanceDTO seance)
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
                label6.Text += "Erreur dans le chargement des séances du groupe " + groupeDTO.NumeroGroupe;
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
                abonnement.IdPersonne = personneDTO.IdPersonne;
                abonnement.IdGroupe = groupeDTO.IdGroupe;
                abonnement.DateInscription = new DateTime();
                abonnement.Prix = groupeDTO.Prix;
                abonnement.DateFin = lastSeanceDTO.DateFin;
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

