using obg.BusinessLogic.Interface.Interfaces;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace obg.BusinessLogic.Logics
{
    public class PharmacyService: IPharmacyService
    {
        private readonly IPharmacyManagement _pharmacyManagement;
        private readonly ISessionManagement _sessionManagement;

        public PharmacyService(){}
        public PharmacyService(IPharmacyManagement pharmacymanagement, ISessionManagement sessionManagement)
        {
            _pharmacyManagement = pharmacymanagement;
            _sessionManagement = sessionManagement;
        }

        public string InsertPharmacy(Pharmacy pharmacy, string token)
        {
            if (IsPharmacyValid(pharmacy))
            {
                Session session = _sessionManagement.GetSessionByToken(token);
                _pharmacyManagement.InsertPharmacy(pharmacy, session);
            }
            return pharmacy.Name;
        }
        
        private bool IsPharmacyValid(Pharmacy pharmacy)
        {
            if (pharmacy == null)
            {
                throw new PharmacyException("Farmacia inválida.");
            }
            if (pharmacy.Name == null || pharmacy.Name.Length < 1 || pharmacy.Name.Length > 50)
            {
                throw new PharmacyException("Nombre inválido.");
            }
            if (IsNameRegistered(pharmacy.Name))
            {
                throw new PharmacyException("El nombre ya está registrado");
            }
            if (pharmacy.Address == null || pharmacy.Address.Length < 1)
            {
                throw new PharmacyException("Dirección inválida.");
            }
            return true;
        }

        private bool IsNameRegistered(string name)
        {
            return _pharmacyManagement.IsNameRegistered(name);
        }

    }
}
