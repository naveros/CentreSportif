using CentreSportifLib.dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CentreSportifGUI.Views.formulaire
{
    public partial class FormulaireSeance : Form
    {
        GroupeDTO g;
        public CentreSportifGUI CentreView;
        public FormulaireSeance(GroupeDTO g)
        {
            InitializeComponent();
            this.g = g;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime tomorrow = DateTime.Today.AddDays(1);
            // The (... + 7) % 7 ensures we end up with a value in the range [0, 6]
            int daysUntilTuesday = ((int)DayOfWeek.Tuesday - (int)tomorrow.DayOfWeek + 7) % 7;
            DateTime nextTuesday = tomorrow.AddDays(daysUntilTuesday);
        }

        private void FormulaireSeance_Load(object sender, EventArgs e)
        {
            CentreView = (CentreSportifGUI)this.Owner;
            comboBox3.DataSource = Enum.GetNames(typeof(DayOfWeek));
            dateTimePicker1.ShowUpDown = true;
            dateTimePicker1.CustomFormat = "hh";
            dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        }
    }
}
