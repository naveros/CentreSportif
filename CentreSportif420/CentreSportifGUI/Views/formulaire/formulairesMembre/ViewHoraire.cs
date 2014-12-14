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
        public ViewHoraire(PersonneDTO personneDTO)
        {
            InitializeComponent();
            this.personneDTO = personneDTO;

        }

        private void ViewHoraire_Load(object sender, EventArgs e)
        {


            CentreView = (CentreSportifGUI)this.Owner;



        }

        private void calendar1_LoadItems(object sender, CalendarLoadEventArgs e)
        {
            try
            {

               

                
                
                /*  EnseigneDTO enseigneDTO = CentreView.DbCreateur.ServicePersonne.getEnseigneByGroupId(groupeDTO.IdGroupe);
                  PersonneDTO professeurDTO = CentreView.DbCreateur.ServicePersonne.findById(int.Parse(enseigneDTO.IdPersonne));
                  ActiviteDTO activiteDTO = CentreView.DbCreateur.ServiceActivite.findById(groupeDTO.IdActivite);
                  List<SeanceDTO> seances = CentreView.DbCreateur.ServiceGroupe.getAllSeancesByGroupId(groupeDTO.IdGroupe);*/

            }
            catch (Exception ee)
            {
                Console.WriteLine("Erreur dans la requete get horaire");
                Console.Write(ee.Message);
            }

        }

        private void calendar1_Layout(object sender, LayoutEventArgs e)
        {           
            calendar1.ViewStart = DateTime.Today;
            calendar1.ViewEnd = DateTime.Today.AddDays(7);
            calendar1.AllowItemEdit = false;
            calendar1.AllowItemResize = false;
            //calendar1.AllowNew = false;
          ////  CalendarItem item = new CalendarItem(calendar1, DateTime.Now, DateTime.Now.AddHours(2), "Test ");
         //   calendar1.Items.Add(item);

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
    }
}
