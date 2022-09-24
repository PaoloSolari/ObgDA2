using System;
using System.Collections.Generic;
using System.Text;
using obg.Domain.Entities;

namespace obg.BusinessLogic
{
    public static class FakeDB
    {
        public static List<Pharmacy> Pharmacies = new List<Pharmacy>();
        public static List<Medicine> Medicines = new List<Medicine>();
        public static List<User> Users = new List<User>();
        public static List<Administrator> Administrators = new List<Administrator>();
        public static List<Owner> Owners = new List<Owner>();
        public static List<Employee> Employees = new List<Employee>();
        public static List<Invitation> Invitations = new List<Invitation>();
        public static List<Demand> Demands = new List<Demand>();
        public static List<Petition> Petitions = new List<Petition>();
        public static List<Purchase> Purchases = new List<Purchase>();
        public static List<PurchaseLine> PurchaseLines = new List<PurchaseLine>();
    }
}
