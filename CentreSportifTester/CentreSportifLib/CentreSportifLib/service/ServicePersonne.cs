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
        public void register(PersonneDTO personneDTO)
        {
            personneDAO.add(personneDTO);
        }
        public List<PersonneDTO> getAll()
        {
            return personneDAO.getAll();
            /*
            String result = "{ result: [";
            personneDAO.getAll().ForEach(delegate( PersonneDTO p )
            {
                result += p.ToString() +", ";
            });
            result += "]}";
            return result;*/
        }
        public void getFullSchedule(PersonneDTO personneDTO)
        {
            //todo
        }
        public PersonneDTO findById(PersonneDTO personneDTO)
        {
            return personneDAO.get(personneDTO);
        }

        public void modifier(PersonneDTO p)
        {
            this.personneDAO.update(p);
        }
    }
}
