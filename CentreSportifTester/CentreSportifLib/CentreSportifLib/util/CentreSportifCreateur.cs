using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using CentreSportifLib.service;
using CentreSportifLib.dao;

namespace CentreSportifLib
{
    public class CentreSportifCreateur
    {
        private String connectionString = "host=db4free.net; database=centresportif420; user=centresportif420; password=stephane420;";
        private MySqlConnection con;
        private PersonneDAO personneDAO;
        private ActiviteDAO activiteDAO;
        private GroupeDAO groupeDAO;
        private ServicePersonne servicePersonne;
        private ServiceActivite serviceActivite;
        private ServiceGroupe serviceGroupe;

        public CentreSportifCreateur()
        {
            try
            {

                //Init la connexion
                this.con = new MySqlConnection(connectionString);
                //Init les DAOs
                this.personneDAO = new PersonneDAO(this.con);
                this.activiteDAO = new ActiviteDAO(this.con);
                this.groupeDAO = new GroupeDAO(this.con);
                //Init les service
                this.servicePersonne = new ServicePersonne(personneDAO);
                this.serviceActivite = new ServiceActivite(activiteDAO);
                this.serviceGroupe = new ServiceGroupe(groupeDAO);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur dans l'initialisation des singleton details" + e.Message);
            }
        }

        public ServicePersonne ServicePersonne
        {
            get
            {
                return this.servicePersonne;
            }
        }
        public ServiceActivite ServiceActivite
        {
            get
            {
                return this.serviceActivite;
            }
        }
        public ServiceGroupe ServiceGroupe
        {
            get
            {
                return this.serviceGroupe;
            }
        }
    }
}
