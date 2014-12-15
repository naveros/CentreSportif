using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Imaging;
using CentreSportifLib.dto;

namespace CentreSportifGUI.Views.formulaire
{
    public partial class FormulaireMembre : Form
    {
        PersonneDTO personneDTO;
        AdresseDTO adresseDTO;
        string mode;
        public CentreSportifGUI CentreView;
        Image photo;
        public FormulaireMembre(PersonneDTO personneDTO)
        {
            InitializeComponent();
            if (personneDTO != null)
            {
                this.mode = "Modifier";
                this.personneDTO = personneDTO;
            }
            else
            {
                this.mode = "Créer";
                this.personneDTO = new PersonneDTO();
            }
            this.Text = mode;
        }

        private void button1_Click(object sender, EventArgs e) //enregistrer/modifier
        {
            String errorMessage = "";
            try
            {
                personneDTO.IdPersonne = textBox1.Text;
                personneDTO.Prenom = textBox2.Text;
                personneDTO.Nom = textBox3.Text;
                personneDTO.DateNaissance = dateTimePicker1.Value;
                personneDTO.Email = textBox4.Text;
                personneDTO.CodeBarre = textBox5.Text;

                adresseDTO = new AdresseDTO();
                adresseDTO.Numero = textBox8.Text;
                adresseDTO.Rue = textBox9.Text;
                adresseDTO.CodePostal = textBox10.Text;
                adresseDTO.Ville = textBox11.Text;
                adresseDTO.IdPersonne = textBox1.Text;
                adresseDTO.Pays = textBox12.Text;
                personneDTO.Adresse = adresseDTO;
                personneDTO.Role = "membre";

                if (radioButton2.Checked)
                {
                    personneDTO.Sexe = 'F';
                }
                else
                {
                    personneDTO.Sexe = 'M';
                }

                if (textBox6.Text.Equals(textBox7.Text))
                {
                    personneDTO.MotDePasse = textBox6.Text;

                }
                else
                {
                    errorMessage = "Mot de passe incorrect";
                    Exception exeptionPWD = new Exception("Mot de passe incorrect");
                    throw exeptionPWD;
                }

                if (this.mode.Equals("Créer"))
                {
                    photo.Save("../photos/" + personneDTO.IdPersonne + ".jpg", ImageFormat.Jpeg); /////////
                    adresseDTO.IdPersonne = this.CentreView.DbCreateur.ServicePersonne.register(personneDTO);
                    this.CentreView.DbCreateur.ServicePersonne.addAdresse(adresseDTO);
                    errorMessage = "Ajout réussit !";
                }
                else if (this.mode.Equals("Modifier"))
                {
                    CentreView.DbCreateur.ServicePersonne.update(personneDTO);
                    CentreView.DbCreateur.ServicePersonne.updateAdresse(adresseDTO);
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
                label13.Text = "Message : " + errorMessage;
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
                textBox1.Text = personneDTO.IdPersonne;
                textBox2.Text = personneDTO.Prenom;
                textBox3.Text = personneDTO.Nom;
                textBox4.Text = personneDTO.Email;
                dateTimePicker1.Value = personneDTO.DateNaissance;
                textBox8.Text = adresseDTO.Numero;
                textBox9.Text = adresseDTO.Rue;
                textBox10.Text = adresseDTO.CodePostal;
                textBox11.Text = adresseDTO.Ville;
                textBox12.Text = adresseDTO.Pays;
                try
                {
                    if (File.Exists("../photos/" + personneDTO.IdPersonne + ".jpg"))
                    {
                        pictureBox1.Image = Image.FromFile("../photos/" + personneDTO.IdPersonne + ".jpg");
                        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
                catch
                {
                    throw;
                }

                if (personneDTO.Sexe.Equals('f'))
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
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "%USERPROFILE%";
            openFileDialog1.Filter = "Jpeg files (*.jpg)|*.jpg";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            photo = System.Drawing.Image.FromStream(myStream);
                            pictureBox1.Image = Image.FromStream(myStream);
                            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void FormulaireMembre_Load(object sender, EventArgs e)
        {
            this.CentreView = (CentreSportifGUI)this.Owner;
            if (this.mode.Equals("Modifier"))
            {
                adresseDTO = CentreView.DbCreateur.ServicePersonne.getAdresse(personneDTO.IdPersonne);
                remplir();
            }
        }
    }
}
