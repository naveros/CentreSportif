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
        GroupeDTO g;
        string mode;
        public CentreSportifGUI CentreView;
        public FormulaireGroupe(GroupeDTO g)
        {
            InitializeComponent();

            if (g != null)
            {

                this.mode = "Modifier";
                this.g = g;
                remplir();
            }
            else
            {
                this.mode = "Créer";
                this.g = new GroupeDTO();
            }
            this.Text = mode;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label4.Text = "Message : ";
            try
            {
                g.IdGroupe = textBox1.Text;
                g.IdActivite = textBox2.Text;
                g.NumeroGroupe = textBox3.Text;


                if (this.mode.Equals("Créer"))
                {
                    CentreView.DbCreateur.ServiceGroupe.creer(g);
                    label4.Text += "Le groupe " + g.NumeroGroupe + " a bien été crée";
                    CentreView.RefreshTableGroupe();
                }
                else if (this.mode.Equals("Modifier"))
                {
                    CentreView.DbCreateur.ServiceGroupe.update(g);
                    label4.Text += "Le groupe " + g.NumeroGroupe + " a bien été modifié";
                    CentreView.RefreshTableGroupe();
                }
            }
            catch (Exception)
            {
                label4.Text = "Informations incorrectes";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            
        }

        private void remplir()
        {
            textBox1.Text = g.IdActivite;
            textBox3.Text = g.NumeroGroupe;
        }

        private void FormulaireGroupe_Load(object sender, EventArgs e)
        {
            CentreView = (CentreSportifGUI)this.Owner;
            if (this.mode.Equals("Modifier")) 
            {
                //Remplir le comboBox des activité
                List<ActiviteDTO> activites = CentreView.DbCreateur.ServiceActivite.getAll();
                var bindingList1 = new BindingList<ActiviteDTO>(activites);
                var source1 = new BindingSource(bindingList1, null);
                comboBox1.DataSource = source1;
                //Remplir le comboBox des profs
                List<PersonneDTO> professeurs = CentreView.DbCreateur.ServicePersonne.getAllTeachers();
                var bindingList2 = new BindingList<PersonneDTO>(professeurs);
                var source2 = new BindingSource(bindingList2, null);
                comboBox2.DataSource = source2;
                //Remplir le comboBox des jours
                comboBox3.DataSource = Enum.GetNames(typeof(Jour));

            }
        }
    }
}
