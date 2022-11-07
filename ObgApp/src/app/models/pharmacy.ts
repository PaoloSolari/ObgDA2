import { Demand } from "./demand";
import { Medicine } from "./medicine";
import { Purchase } from "./purchase";

export class Pharmacy {

    Name: string | null;
    Address: string | null;
    Medicines: Medicine[] | null;
    Demands: Demand[] | null;
    Purchases: Purchase[] | null;

    constructor(Name: string | null, Address: string | null, Medicines: Medicine[] | null, Demands: Demand[] | null, Purchases: Purchase[] | null) {
        this.Name = Name;
        this.Address = Address;
        this.Medicines = Medicines;
        this.Demands = Demands;
        this.Purchases = Purchases;
    }
}