using System;
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
  /*  public enum Jour
    {
        Dimanche = 0,
        Lundi = 1,
        Mardi = 2,
        Mercredi = 3,
        Jeudi = 4,
        Vendredi = 5,
        Samedi = 6
    }*/
    public partial class FormulaireGroupe : Form
    {
        GroupeDTO groupeDTO;
        string mode;
        public CentreSportifGUI CentreView;
        public FormulaireGroupe(GroupeDTO groupeDTO)
        {
            InitializeComponent();

            if (groupeDTO != null)
            {

                this.mode = "Modifier";
                this.groupeDTO = groupeDTO;
                remplir();
            }
            else
            {
                this.mode = "Créer";
                this.groupeDTO = new GroupeDTO();
            }
            this.Text = mode;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label4.Text = "Message : ";
            try
            {
                ActiviteDTO activiteDTO = (ActiviteDTO)comboBox1.SelectedItem;
                PersonneDTO professeur = (PersonneDTO)comboBox2.SelectedItem;
                groupeDTO.IdActivite = activiteDTO.IdActivite;
                groupeDTO.NumeroGroupe = textBox3.Text;
                groupeDTO.Prix = decimal.Parse(textBox4.Text);

                if (this.mode.Equals("Créer"))
                {
                    String idgroupe = CentreView.DbCreateur.ServiceGroupe.creer(groupeDTO);
                    // add prof
                    EnseigneDTO enseigneDTO = new EnseigneDTO();
                    enseigneDTO.IdGroupe = idgroupe;
                    enseigneDTO.IdPersonne = professeur.IdPersonne;
                    CentreView.DbCreateur.ServicePersonne.addEnseigne(enseigneDTO);
                    //Crée les seance
                    int nbSeance = int.Parse(textBox2.Text);
                    DateTime nextSeanceDebut = dateTimePicker2.Value;
                    int seanceHour = (int)numericUpDown1.Value;
                    nextSeanceDebut = ChangeTime(nextSeanceDebut, seanceHour, 0, 0, 0);
                    DateTime nextSeanceFin = nextSeanceDebut.AddHours(int.Parse(activiteDTO.Duree));

                    for (int i = 0; i < nbSeance; i++)
                    {
                        if (checkBox1.Checked)
                        {
                            for (int j = 0; j < 7; j++)
                            {
                                SeanceDTO seanceDTO = new SeanceDTO();
                                seanceDTO.IdGroupe = idgroupe;
                                seanceDTO.DateDebut = nextSeanceDebut.AddDays(j);
                                seanceDTO.DateFin = nextSeanceFin.AddDays(j);
                                CentreView.DbCreateur.ServiceGroupe.addSeance(seanceDTO);
                            }
                        }
                        else
                        {
                            SeanceDTO seanceDTO = new SeanceDTO();
                            seanceDTO.IdGroupe = idgroupe;
                            seanceDTO.DateDebut = nextSeanceDebut;
                            seanceDTO.DateFin = nextSeanceFin;
                            CentreView.DbCreateur.ServiceGroupe.addSeance(seanceDTO);
                        }
                        nextSeanceDebut = nextSeanceDebut.AddDays(7);
                        nextSeanceFin = nextSeanceFin.AddDays(7);
                    }
                    label4.Text += "Le groupe " + groupeDTO.NumeroGroupe + " a bien été crée";
                    CentreView.RefreshTableGroupe();
                }
                else if (this.mode.Equals("Modifier"))
                {
                    groupeDTO.IdGroupe = textBox1.Text;
                    CentreView.DbCreateur.ServiceGroupe.update(groupeDTO);
                    label4.Text += "Le groupe " + groupeDTO.NumeroGroupe + " a bien été modifié";
                    CentreView.RefreshTableGroupe();
                }
            }
            catch (Exception ee)
            {
                label4.Text = "Informations incorrectes";
                Console.WriteLine("Erreur dans la connexion par ID");
                Console.Write(ee.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void remplir()
        {
            textBox1.Text = groupeDTO.IdActivite;
            textBox3.Text = groupeDTO.NumeroGroupe;
        }

        private void FormulaireGroupe_Load(object sender, EventArgs e)
        {
            CentreView = (CentreSportifGUI)this.Owner;
            if (this.mode.Equals("Créer"))
            {
                //Remplir le comboBox des activité
                List<ActiviteDTO> listActiviteDTO = CentreView.DbCreateur.ServiceActivite.getAll();
                var bindingList1 = new BindingList<ActiviteDTO>(listActiviteDTO);
                var source1 = new BindingSource(bindingList1, null);
                comboBox1.DataSource = source1;
                //Remplir le comboBox des profs
                List<PersonneDTO> listProfesseurDTO = CentreView.DbCreateur.ServicePersonne.getAllTeachers();
                var bindingList2 = new BindingList<PersonneDTO>(listProfesseurDTO);
                var source2 = new BindingSource(bindingList2, null);
                comboBox2.DataSource = source2;
                //Set up le numericUpDown
                numericUpDown1.Value = 1;
                numericUpDown1.Maximum = 24;
                numericUpDown1.Minimum = 1;

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dateTimePicker2.Enabled = false;
                label8.Text = "Nombre de semaines";
            }
            else
            {
                dateTimePicker2.Enabled = true;
                label8.Text = "Nombre de séances";
            }
        }
        public  DateTime ChangeTime(DateTime dateTime, int hours, int minutes, int seconds, int milliseconds)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                milliseconds);
        }
    }
}