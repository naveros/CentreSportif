using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CentreSportifLib.dao;
using CentreSportifLib.dto;

namespace CentreSportifLib.service
{
    public class ServiceGroupe
    {
        private GroupeDAO groupeDAO {set;get;}

        public ServiceGroupe(GroupeDAO groupeDAO){
        this.groupeDAO = groupeDAO;
        }

        public void creer(GroupeDTO groupeDTO)
        {
            groupeDAO.add(groupeDTO);
        }
        public List<GroupeDTO> getAll()
        {
            return groupeDAO.getAll();
            /*
            String result = "{ result: [";
            groupeDAO.getAll().ForEach(delegate( GroupeDTO p )
            {
                result += p.ToString() +", ";
            });
            result += "]}";
            return result;*/
        }


        public List<GroupeDTO> getAllByActivite(int  idActivite)
        {
            return groupeDAO.getAllByActivite(idActivite);
        }
        public GroupeDTO findById(GroupeDTO groupeDTO)
        {
            return groupeDAO.get(groupeDTO);
        }

        public void update(GroupeDTO p)
        {
            this.groupeDAO.update(p);
        }

        public void delete(GroupeDTO p)
        {
            this.groupeDAO.delete(p);
        }

        public List<SeanceDTO> getAllSeances(GroupeDTO groupeDTO)
        {
            return this.groupeDAO.getAllSeances(groupeDTO);
        }
    }
}
