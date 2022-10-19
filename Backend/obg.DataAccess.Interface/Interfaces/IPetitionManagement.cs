using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.DataAccess.Interface.Interfaces
{
    public interface IPetitionManagement
    {
        void InsertPetition(Petition petition);
        IEnumerable<Petition> GetPetitions();
        Petition GetPetitionById(string id);
        void UpdatePetition(Petition petition);
        void DeletePetition(Petition petition);
        bool IsIdPetitionRegistered(string id); 
        bool IsMedicineCodeOk(string code); 
    }
}
