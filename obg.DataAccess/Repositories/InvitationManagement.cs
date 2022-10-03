using Microsoft.EntityFrameworkCore;
using obg.DataAccess.Context;
using obg.DataAccess.Interface.Interfaces;
using obg.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace obg.DataAccess.Repositories
{
    public class InvitationManagement : IInvitationManagement
    {
        private ObgContext ObgContext { get; set; }
        public InvitationManagement(ObgContext obgContext)
        {
            this.ObgContext = obgContext;
        }

        public void InsertInvitation(Invitation invitation)
        {
            // Obtienes en una variable 'pharmacyOfInvitation' la farmacia de la invitación.
            //Pharmacy pharmacyOfInvitation = ObgContext.Pharmacies.Where<Pharmacy>(p => p.Name.Equals(invitation.Pharmacy.Name)).AsNoTracking().FirstOrDefault();
            //Pharmacy newPharmacy = DuplicatePharmacy(pharmacyOfInvitation);
            //invitation.Pharmacy = null;
            ObgContext.Invitations.Add(invitation);
            ObgContext.SaveChanges();

            // Luego, una vez tengas la invitación en la base de datos, las relacionas.
            //Invitation invitationFromDB = ObgContext.Invitations.Where<Invitation>(i => i.IdInvitation.Equals(invitation.IdInvitation)).AsNoTracking().FirstOrDefault();
            //invitationFromDB.Pharmacy = newPharmacy;
            //ObgContext.Invitations.Update(invitationFromDB);
            //ObgContext.SaveChanges();
        }

        private Pharmacy DuplicatePharmacy(Pharmacy pharmacyOfInvitation)
        {
            Pharmacy newPharmacy = new Pharmacy();
            newPharmacy.Name = pharmacyOfInvitation.Name;
            newPharmacy.Address = pharmacyOfInvitation.Address;
            newPharmacy.Owner = pharmacyOfInvitation.Owner;
            newPharmacy.Employees = pharmacyOfInvitation.Employees;
            newPharmacy.Medicines = pharmacyOfInvitation.Medicines;
            return newPharmacy;
        }

        public IEnumerable<Invitation> GetInvitations()
        {
            return ObgContext.Invitations.ToList();
        }

        public Invitation GetInvitationById(string id)
        {
            return ObgContext.Invitations.Where<Invitation>(i => i.IdInvitation.Equals(id)).AsNoTracking().FirstOrDefault();
        }

        public Invitation GetInvitationByCode(int code)
        {
            return ObgContext.Invitations.Where<Invitation>(i => i.UserCode == code).Include("Pharmacy").AsNoTracking().FirstOrDefault();
            //return ObgContext.Invitations.Where<Invitation>(i => i.UserCode == code).AsNoTracking().FirstOrDefault();
        }
        public void UpdateInvitation(Invitation invitation)
        {
            ObgContext.Invitations.Attach(invitation);
            ObgContext.Entry(invitation).State = EntityState.Modified;
            ObgContext.SaveChanges();
        }

        public void DeleteInvitation(Invitation invitation)
        {
            ObgContext.Invitations.Remove(invitation);
            ObgContext.SaveChanges();
        }

        public bool IsIdInvitationRegistered(string idInvitation)
        {
            Invitation invitation = ObgContext.Invitations.Where<Invitation>(i => i.IdInvitation.Equals(idInvitation)).AsNoTracking().FirstOrDefault();
            if(invitation != null)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        public bool IsNameRegistered(string name)
        {
            Invitation invitation = ObgContext.Invitations.Where<Invitation>(i => i.UserName.Equals(name)).AsNoTracking().FirstOrDefault();
            if(invitation != null)
            {
                return true;
            } 
            else
            {
                return false;
            }
        }

        public bool IsCodeRegistered(int code)
        {
            Invitation invitation = ObgContext.Invitations.Where<Invitation>(i => i.UserCode == code).AsNoTracking().FirstOrDefault();
            if (invitation != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}