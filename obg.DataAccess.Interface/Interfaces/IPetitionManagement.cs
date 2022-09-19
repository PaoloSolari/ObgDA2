using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace obg.DataAccess.Interface.Interfaces
{
    public interface IPetitionManagement
    {
        public void InsertPetition(Petition petition);
    }
}
