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
    public enum Jour
    {
        Dimanche = 0,
        Lundi = 1,
        Mardi = 2,
        Mercredi = 3,
        Jeudi = 4,
        Vendredi = 5,
        Samedi = 6
    }
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
                //g.IdGroupe = textBox1.Text;
                groupeDTO.IdActivite = activiteDTO.IdActivite;
                groupeDTO.NumeroGroupe = textBox3.Text;
                groupeDTO.Prix = decimal.Parse(textBox4.Text);

                // add prof
                //Créee les seances
                DateTime tomorrow = DateTime.Today.AddDays(1);
                // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]

                if (this.mode.Equals("Créer"))
                {
                    String idgroupe = CentreView.DbCreateur.ServiceGroupe.creer(groupeDTO);
                    // add prof
                    EnseigneDTO enseigneDTO = new EnseigneDTO();
                    enseigneDTO.IdGroupe = idgroupe;
                    enseigneDTO.IdPersonne = professeur.IdPersonne;
                    CentreView.DbCreateur.ServicePersonne.addEnseigne(enseigneDTO);
                    //Crée les seance
                    int daysUntilNextSeance = ((int)dateTimePicker1.Value.DayOfWeek - (int)tomorrow.DayOfWeek + 7) % 7;
                    DateTime nextDay = tomorrow.AddDays(daysUntilNextSeance);
                    DateTime nextSeanceDebut = new DateTime(nextDay.Year, nextDay.Month, nextDay.Day, dateTimePicker1.Value.Hour, 0, 0);
                    DateTime nextSeanceFin = new DateTime(nextDay.Year, nextDay.Month, nextDay.Day, dateTimePicker1.Value.Hour, 0, 0);
                    nextSeanceFin = nextSeanceFin.AddHours(int.Parse(activiteDTO.Duree));
                    //nextSeanceDebut.AddHours(int.Parse(activite.Duree));
                    int nb = int.Parse(textBox2.Text);
                    for (int i = 0; i < nb; i++)
                    {
                        SeanceDTO seanceDTO = new SeanceDTO();
                        seanceDTO.IdGroupe = idgroupe;
                        seanceDTO.DateDebut = nextSeanceDebut;
                        seanceDTO.DateFin = nextSeanceFin;
                        CentreView.DbCreateur.ServiceGroupe.addSeance(seanceDTO);
                        nextSeanceDebut = nextSeanceDebut.AddDays(7);
                        nextSeanceFin = nextSeanceFin.AddDays(7);
                    }
                    label4.Text += "Le groupe " + groupeDTO.NumeroGroupe + " a bien été crée";
                    CentreView.RefreshTableGroupe();
                }
                else if (this.mode.Equals("Modifier"))
                {
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
                //Remplir le comboBox des jours
                CentreView = (CentreSportifGUI)this.Owner;
                comboBox3.DataSource = Enum.GetNames(typeof(DayOfWeek));
                dateTimePicker1.ShowUpDown = true;
                dateTimePicker1.CustomFormat = "hh";
                dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            }
        }
    }
}