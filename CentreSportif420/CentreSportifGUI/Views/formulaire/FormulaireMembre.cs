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
    public partial class FormulaireMembre : Form
    {
        PersonneDTO p;
        string mode;
        public CentreSportifGUI CentreView;
        public FormulaireMembre(PersonneDTO p)
        {
            InitializeComponent();
            if (p != null)
            {

                this.mode = "Modifier";
                this.p = p;
                remplir();
            }
            else
            {
                this.mode = "Créer";
                this.p = new PersonneDTO();
            }
            this.Text = mode;
        }

        private void button1_Click(object sender, EventArgs e) //enregistrer/modifier
        {
            String errorMessage = "";
            AdresseDTO adresse;

                //TODO: Gestion des erreurs
            try
            {
                p.IdPersonne = textBox1.Text;
                p.Prenom = textBox2.Text;
                p.Nom = textBox3.Text;
                p.Email = textBox4.Text;
                p.CodeBarre = textBox5.Text;


                adresse = new AdresseDTO();
                adresse.Numero = textBox8.Text;
                adresse.Rue = textBox9.Text;
                adresse.CodePostal = textBox10.Text;
                adresse.Ville = textBox11.Text;
                adresse.IdPersonne = textBox1.Text;
                adresse.Pays = textBox12.Text;
                p.Adresse = adresse;
                p.Role = "membre";


                if (radioButton2.Checked)
                {
                    p.Sexe = 'F';
                }
                else
                {
                    p.Sexe = 'M';
                }
                if (textBox6.Text.Equals(textBox7.Text))
                {
                    p.MotDePasse = textBox6.Text;

                }
                else 
                {
                    errorMessage = "Mot de passe incorrect";
                    Exception exeptionPWD = new Exception("Mot de passe incorrect");
                    throw exeptionPWD;
                }
                if (this.mode.Equals("Créer"))
                {

                    adresse.IdPersonne = this.CentreView.DbCreateur.ServicePersonne.register(p);
                    this.CentreView.DbCreateur.ServicePersonne.addAdresse(adresse);
                    errorMessage = "Ajout réussit !";
                }
                else if (this.mode.Equals("Modifier"))
                {
                    CentreView.DbCreateur.ServicePersonne.update(p);
                    CentreView.DbCreateur.ServicePersonne.updateAdresse(adresse);
                    errorMessage = "Modification réussit";
                }
            }
            catch (Exception exception)
            {
                if (errorMessage == "")
                {
                    errorMessage = "Informations incorrectes";
                }
                Console.WriteLine(exception);
            }
            finally
            {
                label13.Text = "Message : "+errorMessage;
            }
        }
        

        private void button2_Click(object sender, EventArgs e) //annulé
        {
            this.Dispose();
        }

        private void remplir()
        {
            try
            {
                textBox1.Text = p.IdPersonne;
                textBox2.Text = p.Prenom;
                textBox3.Text = p.Nom;
                textBox4.Text = p.Email;
                dateTimePicker1.Value = p.DateNaissance;

                AdresseDTO adresseDTO = CentreView.DbCreateur.ServicePersonne.getAdresse(p);
                textBox8.Text = adresseDTO.Numero;
                textBox9.Text = adresseDTO.Rue;
                textBox10.Text = adresseDTO.CodePostal;
                textBox11.Text = adresseDTO.Ville;
                try
                {
                    pictureBox1.Image = Image.FromFile("../photos/" + p.IdPersonne + ".jpg");
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                catch
                {
                    throw;
                }


                if (p.Sexe.Equals('f'))
                {
                    radioButton2.Checked = true;
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine("Erreur dans la requete get adresse");
                Console.Write(ee.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e) //Changer 
        {
            //TODO
        }

        private void button4_Click(object sender, EventArgs e) //Captuer 
        {
            //TODO
        }

        private void FormulaireMembre_Load(object sender, EventArgs e)
        {
            this.CentreView = (CentreSportifGUI)this.Owner;
        }

    }
}
