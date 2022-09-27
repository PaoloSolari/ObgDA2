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
        protected List<Pharmacy> fakeDB = new List<Pharmacy>();
        //Pharmacy validPharmacy = new Pharmacy("San Roque", "San Roque", null);

        //private readonly IPharmacyManagement _pharmacyManagement;

        //public PharmacyService(IPharmacyManagement pharmacymanagement)
        //{
        //    _pharmacyManagement = pharmacymanagement;
        //}

        public PharmacyService()
        {
            //fakeDB.Add(validPharmacy);

        }

        public Pharmacy InsertPharmacy(Pharmacy pharmacy)
        {
            if (IsPharmacyValid(pharmacy))
            {
                // Se agrega la Pharmacy a la DB: _pharmacyManagement.InsertPharmacy(pharmacy);
                FakeDB.Pharmacies.Add(pharmacy);
            }
            return pharmacy;
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
            foreach (Pharmacy pharmacy in FakeDB.Pharmacies)
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
