﻿using System;
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

        public String creer(GroupeDTO groupeDTO)
        {
            return groupeDAO.add(groupeDTO);
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


        public List<GroupeDTO> getAllByActivite(String  idActivite)
        {
            return groupeDAO.getAllByActivite(idActivite);
        }
        public GroupeDTO findById(String idGroupe)
        {
            return groupeDAO.get(idGroupe);
        }

        public void update(GroupeDTO p)
        {
            this.groupeDAO.update(p);
        }

        public void delete(GroupeDTO p)
        {
            this.groupeDAO.delete(p);
        }

        public void addSeance(SeanceDTO seanceDTO)
        {
            groupeDAO.addSeance(seanceDTO);
        }

        public List<SeanceDTO> getAllSeancesByGroupId(String idGroupe)
        {
            return this.groupeDAO.getAllSeancesByGroupId(idGroupe);
        }
    }
}