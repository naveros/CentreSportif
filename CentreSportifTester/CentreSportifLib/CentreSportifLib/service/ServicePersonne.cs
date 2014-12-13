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
        public String register(PersonneDTO personneDTO)
        {
            return personneDAO.addPersonne(personneDTO);
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
        public List<PersonneDTO> getAllTeachers()
        {
            return personneDAO.getAllByRole("prof");
        }
        public EnseigneDTO getEnseigneByGroupId(String id)
        {
            return personneDAO.getEnseigneByGroupId(id);
        }

        public PersonneDTO findById(int idPersonne)
        {
            return personneDAO.getPersonne(idPersonne);
        }
        public PersonneDTO findByCodeBarre(String codeBarre)
        {
            return personneDAO.getPersonneByCodeBarre(codeBarre);
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

        #region Service Adresse
        public void getAdresse(PersonneDTO personneDTO)
        {
            this.personneDAO.getAdresse(personneDTO);
        }
        public void addAdresse(AdresseDTO adresseDTO)
        {
            this.personneDAO.addAdresse(adresseDTO);
        }
        public void updateAdresse(AdresseDTO adresseDTO)
        {
            this.personneDAO.updateAdresse(adresseDTO);
        }
        #endregion
        #region Service Abonnement
        public List<AbonnementDTO> getAllAbonnements(PersonneDTO personneDTO)
        {
            return this.personneDAO.getAllAbonnements(personneDTO);
        }
        public void addAbonnement(AbonnementDTO a)
        {
            this.personneDAO.addAbonnement(a);
        }
        #endregion
        #region Service Presences
        public void getAllPresences(PersonneDTO personneDTO)
        {
            this.personneDAO.getAllPresences(personneDTO);
        }
        #endregion
        #region Service Paiements
        public List<PaiementDTO> getAllPaiements(PersonneDTO personneDTO)
        {
            return this.personneDAO.getAllPaiements(personneDTO);
        }
        public void addPaiement(PaiementDTO paiementDTO)
        {
            this.personneDAO.addPaiement(paiementDTO);
        }
        #endregion
        #region Service Enseigne
        public void addEnseigne(EnseigneDTO enseigne)
        {
            personneDAO.addEnseigne(enseigne);
        }
        #endregion

        #region Service Message
        public void addMessage(MessageDTO message)
        {
            personneDAO.addMessage(message);
        }
        public void deleteMessage(PersonneDTO personne)
        {
            personneDAO.deleteMessage(personne);
        }
        public List<MessageDTO> getAllMessages(PersonneDTO personne)
        {
            return personneDAO.getAllMessages(personne);
        }
        #endregion
        //TODO get horaire
    }
}
