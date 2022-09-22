using System;
using System.Collections.Generic;
using System.Text;
using obg.Domain.Entities;

namespace obg.BusinessLogic
{
    public static class FakeDB
    {
        public static List<Pharmacy> Pharmacies { get; set; }
        public static List<Medicine> Medicines { get; set; }
        public static List<User> Users { get; set; }
        public static List<Administrator> Administrators { get; set; }
        public static List<Owner> Owners { get; set; }
        public static List<Employee> Employees { get; set; }
        public static List<Invitation> Invitations { get; set; }
        public static List<Demand> Demands { get; set; }
        public static List<Petition> Petitions { get; set; }
        public static List<Purchase> Purchases { get; set; }
        public static List<PurchaseLine> PurchaseLines { get; set; }
    }
}
