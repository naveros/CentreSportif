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
        public CentreSportifGUI CentreView;

        public FormulaireAbonnement(PersonneDTO p)
        {
            InitializeComponent();
            this.p = p;

 
        }
        private void init() {
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

             activite =  (ActiviteDTO)comboBox1.SelectedItem;

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

            //nom prof, jour,heur, heure-fin , nombre de seances. prix
            try
            {
                 groupe = (GroupeDTO)comboBox2.SelectedItem;

                 seances = CentreView.DbCreateur.ServiceGroupe.getAllSeances(groupe); //bygroupID?
                
                 PersonneDTO professeur = CentreView.DbCreateur.ServicePersonne.getEnseigneByGroupId(groupe.IdGroupe);
                 //AbonnementDTO abonnement = CentreView.DbCreateur.ServicePersonne.getAbonnementByGroup
                AbonnementDTO abonnement = new AbonnementDTO();

                 this.dataGridView1.Rows.Clear();
                 CentreView.DbCreateur.ServiceGroupe.getAllSeances(groupe).ForEach(delegate(SeanceDTO seance)
                 {
                     int i = this.dataGridView1.Rows.Add();
                     // Console.WriteLine(g.ToString());
                     dataGridView1.Rows[i].Cells[0].Value = seance.DateDebut.Hour;
                     dataGridView1.Rows[i].Cells[1].Value = seance.DateFin.Hour;
                     dataGridView1.Rows[i].Cells[2].Value = professeur.Prenom + " " + professeur.Nom;
                     dataGridView1.Rows[i].Cells[3].Value = abonnement.DateFin;
                     dataGridView1.Rows[i].Cells[4].Value = abonnement.Prix;
                 });

                     //todo manuel
            /*    var bindingList = new BindingList<SeanceDTO>(seances);
                var source = new BindingSource(bindingList, null);
                dataGridView1.DataSource = source;*/
            }
            catch (Exception ee)
            {
                Console.WriteLine("Erreur dans la requete get all seances ");
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
                a.DateFin = new DateTime().AddDays(100); //TODO . a changer pour la date de fin

                CentreView.DbCreateur.ServicePersonne.addAbonnement(a);
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

        private void FormulaireAbonnement_Load(object sender, EventArgs e)
        {
            CentreView = (CentreSportifGUI)this.Owner;
            init();
        }

    }
}

