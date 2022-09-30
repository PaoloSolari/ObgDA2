using Microsoft.EntityFrameworkCore;
using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace obg.DataAccess.Repositories
{
    public class PetitionManagement : IPetitionManagement
    {
        private ObgContext ObgContext { get; set; }
        public PetitionManagement(ObgContext obgContext)
        {
            this.ObgContext = obgContext;
        }

        public void InsertPetition(Petition petition)
        {
            ObgContext.Petitions.Add(petition);
            ObgContext.SaveChanges();
        }

        public IEnumerable<Petition> GetPetitions()
        {
            return ObgContext.Petitions.ToList();
        }

        public Petition GetPetitionById(string id)
        {
            return ObgContext.Petitions.Where<Petition>(d => d.IdPetition == id).AsNoTracking().FirstOrDefault();
        }

        public void UpdatePetition(Petition petition)
        {
            ObgContext.Petitions.Attach(petition);
            ObgContext.Entry(petition).State = EntityState.Modified;
            ObgContext.SaveChanges();
        }

        public void DeletePetition(Petition petition)
        {
            ObgContext.Petitions.Remove(petition);
            ObgContext.SaveChanges();
        }
    }
}
