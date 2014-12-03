using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CentreSportifLib.dao;
using CentreSportifLib.dto;

namespace CentreSportifLib.service
{
    class ServiceGroupe
    {
        GroupeDAO groupeDAO;
        public ServiceGroupe(GroupeDAO groupeDAO)
        {
            this.groupeDAO = groupeDAO;
        }

        public void creer(GroupeDTO a)
        {
            groupeDAO.add(a);
        }
        public List<GroupeDTO> getAll()
        {
            return groupeDAO.getAll();
        }
        public GroupeDTO findById(GroupeDTO a)
        {
            return groupeDAO.get(a);
        }
        public void modifier(GroupeDTO a)
        {
            groupeDAO.update(a);
        }
    }
}
