using CentreSportifLib.dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Calendar;
namespace CentreSportifGUI.Views.formulaire.formulairesMembre
{
    public partial class ViewHoraire : Form
    {
        List<CalendarItem> items = new List<CalendarItem>();
        PersonneDTO personneDTO;
        public CentreSportifGUI CentreView;
        int nbMoreWeek=0;
        public ViewHoraire(PersonneDTO personneDTO)
        {
            InitializeComponent();
            this.personneDTO = personneDTO;

        }

        private void ViewHoraire_Load(object sender, EventArgs e)
        {
            CentreView = (CentreSportifGUI)this.Owner;
        }

        private void calendar1_Layout(object sender, LayoutEventArgs e)
        {
            DateTime viewStart = DateTime.Today; ;
            DateTime viewEnd = DateTime.Today.AddDays(7);
            calendar1.ViewStart = viewStart;
            calendar1.ViewEnd = viewEnd;
            label1.Text = viewStart.ToShortDateString();

            List<AbonnementDTO> abonnements = CentreView.DbCreateur.ServicePersonne.getAllAbonnements(personneDTO);
            foreach (AbonnementDTO abonnement in abonnements)
            {
                GroupeDTO groupe = CentreView.DbCreateur.ServiceGroupe.findById(abonnement.IdGroupe);
                List<SeanceDTO> seances = CentreView.DbCreateur.ServiceGroupe.getAllSeancesByGroupId(abonnement.IdGroupe);
                foreach (SeanceDTO seance in seances)
                {
                    ActiviteDTO activite = CentreView.DbCreateur.ServiceActivite.findById(groupe.IdActivite);
                    CalendarItem item = new CalendarItem(calendar1, seance.DateDebut, seance.DateFin, activite.Nom);
                    items.Add(item);
                }

            }
            calendar1.Items.AddRange(items);
        }
        private void calendar1_LoadItems(object sender, CalendarLoadEventArgs e)
        {
        }
        private void button2_Click(object sender, EventArgs e)//Retour
        {
            this.Dispose();
        }



        private void button1_Click(object sender, EventArgs e) //Prochaine semaine
        {
            try
            {
                nbMoreWeek++;

                DateTime viewStart = DateTime.Today.AddDays(7 * nbMoreWeek - 7);
                DateTime viewEnd = DateTime.Today.AddDays(7 * nbMoreWeek);
                calendar1.ViewStart = viewStart;
                calendar1.ViewEnd = viewEnd;
                label1.Text = viewStart.ToShortDateString();
                calendar1.Items.AddRange(items);;
            }
            catch (Exception ee)
            {
                Console.WriteLine("Erreur dans changer semaine");
                Console.Write(ee.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)//Derniere semaine
        {
            try
            {
                nbMoreWeek--;

                DateTime viewStart = DateTime.Today.AddDays(7 * nbMoreWeek);
                DateTime viewEnd = DateTime.Today.AddDays(7 * nbMoreWeek +7);
                calendar1.ViewStart = viewStart;
                calendar1.ViewEnd = viewEnd;
                label1.Text = viewStart.ToShortDateString();
                calendar1.Items.AddRange(items);
            }
            catch (Exception ee)
            {
                Console.WriteLine("Erreur dans changer semaine");
                Console.Write(ee.Message);
            }
        }
    }
}
