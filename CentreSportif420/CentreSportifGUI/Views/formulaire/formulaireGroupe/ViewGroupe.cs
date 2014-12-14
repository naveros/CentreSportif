using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CentreSportifLib.dto;

namespace CentreSportifGUI.Views.formulaire.formulaireGroupe
{
    public partial class ViewGroupe : Form
    {
        GroupeDTO groupeDTO;
        public CentreSportifGUI CentreView;
        public ViewGroupe(GroupeDTO groupeDTO)
        {
            InitializeComponent();
            this.groupeDTO = groupeDTO;
        }

        public void init()
        {
            try
            {
                EnseigneDTO enseigneDTO = CentreView.DbCreateur.ServicePersonne.getEnseigneByGroupId(groupeDTO.IdGroupe);
                PersonneDTO professeurDTO = CentreView.DbCreateur.ServicePersonne.findById(int.Parse(enseigneDTO.IdPersonne));
                ActiviteDTO activiteDTO = CentreView.DbCreateur.ServiceActivite.findById(groupeDTO.IdActivite);
                List<SeanceDTO> seances = CentreView.DbCreateur.ServiceGroupe.getAllSeancesByGroupId(groupeDTO.IdGroupe);
                SeanceDTO lastSeance = seances.Last();

                textBox1.Text = "" + seances.Count;
                textBox2.Text = lastSeance.DateFin.Day + " / " + lastSeance.DateFin.Month + " / " + lastSeance.DateFin.Year;
                textBox3.Text = "" + groupeDTO.Prix;
                textBox4.Text = activiteDTO.Nom;

                seances.ForEach(delegate(SeanceDTO seance)
                {
                    int i = this.dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = seance.DateDebut.DayOfWeek;
                    dataGridView1.Rows[i].Cells[1].Value = seance.DateDebut.Hour + "h";
                    dataGridView1.Rows[i].Cells[2].Value = seance.DateFin.Hour + "h";
                    dataGridView1.Rows[i].Cells[3].Value = professeurDTO.Prenom + " " + professeurDTO.Nom;
                });
            }
            catch (Exception ee)
            {
                Console.WriteLine("Erreur dans la requete get all seances");
                Console.Write(ee.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)//annuler
        {
            this.Dispose();
        }

        private void viewGroupe_Load(object sender, EventArgs e)
        {
            CentreView = (CentreSportifGUI)this.Owner;
            init();
        }
    }
}
