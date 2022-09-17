using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using obg.Exceptions;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace obg.BusinessLogic.Logics
{
    public class PharmacyService
    {
        protected List<Pharmacy> fakeDB = new List<Pharmacy>();
        private readonly IPharmacyManagement _pharmacyManagement;

        public PharmacyService(IPharmacyManagement pharmacyManagement) 
        { 
            _pharmacyManagement = pharmacyManagement;
        }

        public void InsertPharmacy(Pharmacy pharmacy)
        {
            if (isPharmacyValid(pharmacy) && !IsNameRegistered(pharmacy.Name))
            {
                // Se agreaga la Pharmacy a la DB: _pharmacyManagement.InsertPharmacy(pharmacy);
                fakeDB.Add(pharmacy);
            }
        }

        private bool isPharmacyValid(Pharmacy pharmacy)
        {
            if (pharmacy == null) throw new PharmacyException("Farmacia inválida.");
            if (pharmacy.Name == null || pharmacy.Name.Length < 1 || pharmacy.Name.Length > 50) throw new PharmacyException("Nombre inválido.");
            if (pharmacy.Address == null || pharmacy.Address.Length < 1) throw new PharmacyException("Dirección inválida.");
            if (IsNameRegistered(pharmacy.Name)) throw new PharmacyException("El nombre ya está registrado");
 
            return true;
        }

        private bool IsNameRegistered(string name)
        {
            foreach (Pharmacy pharmacy in this.fakeDB)
            {
                if (name.Equals(pharmacy.Name))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
