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
        public FormulaireAbonnement(PersonneDTO p)
        {
            InitializeComponent();
            this.p = p;
        }

    }
}
