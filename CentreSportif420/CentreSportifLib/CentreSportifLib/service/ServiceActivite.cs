﻿using CentreSportifLib.dao;
using CentreSportifLib.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CentreSportifLib.service
{
    public class ServiceActivite
    {
        ActiviteDAO activiteDAO;
        public ServiceActivite(ActiviteDAO activiteDAO)
        {
            this.activiteDAO = activiteDAO;
        }

        public void creer(ActiviteDTO a)
        {
            activiteDAO.add(a);
        }
        public List<ActiviteDTO> getAll()
        {
            return activiteDAO.getAll();
        }
        public ActiviteDTO findById(String idActivite)
        {
            return activiteDAO.get(idActivite);
        }
        public void modifier(ActiviteDTO a)
        {
            activiteDAO.update(a);
        }

        public void delete(ActiviteDTO a)
        {
            activiteDAO.delete(a);
        }
    }
}