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
        public ViewHoraire()
        {
            InitializeComponent();
        }

        private void ViewHoraire_Load(object sender, EventArgs e)
        {
            calendar1.ViewStart = DateTime.Today;
            calendar1.ViewEnd = DateTime.Today.AddDays(7) ;
        }
    }
}
