using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CentreSportifLib.dao;
using CentreSportifLib.dto;

namespace CentreSportifLib.service
{
    public class ServicePersonne
    {
        private PersonneDAO personneDAO { set; get; }
        public ServicePersonne(PersonneDAO personneDAO)
        {
            this.personneDAO = personneDAO;
        }
        #region personneDTO
        public void register(PersonneDTO personneDTO)
        {
            personneDAO.addPersonne(personneDTO);
        }
        public List<PersonneDTO> getAll()
        {
            return personneDAO.getAllPersonnes();
            /*
            String result = "{ result: [";
            personneDAO.getAll().ForEach(delegate( PersonneDTO p )
            {
                result += p.ToString() +", ";
            });
            result += "]}";
            return result;*/
        }
        public PersonneDTO findById(PersonneDTO personneDTO)
        {
            return personneDAO.getPersonne(personneDTO);
        }
        public void update(PersonneDTO personneDTO)
        {
            this.personneDAO.updatePersonne(personneDTO);
        }
        public void delete(PersonneDTO personneDTO)
        {
            this.personneDAO.deletePersonne(personneDTO);
        }
        #endregion personne


        public void getAdresse(PersonneDTO personneDTO)
        {
            this.personneDAO.getAdresse(personneDTO);
        }
        public void getAllAbonnements(PersonneDTO personneDTO) 
        {
            this.personneDAO.getAllAbonnements(personneDTO);
        }

        public void getAllPresences(PersonneDTO personneDTO)
        {
            this.personneDAO.getAllPresences(personneDTO);
        }
        public void getAllPaiements(PersonneDTO personneDTO) 
        {
            this.personneDAO.getAllPaiements(personneDTO);
        }
        //TODO get horaire
    }
}
