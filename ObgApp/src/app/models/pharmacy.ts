import { Demand } from "./demand";
import { Medicine } from "./medicine";
import { Purchase } from "./purchase";

export class Pharmacy {
    name: string | null;
    address: string | null;
    medicines: Medicine[] | null;
    demands: Demand[] | null;
    purchases: Purchase[] | null;

    constructor(name: string | null, address: string | null, medicines: Medicine[] | null, demands: Demand[] | null, purchases: Purchase[] | null) {
        this.name = name;
        this.address = address;
        this.medicines = medicines;
        this.demands = demands;
        this.purchases = purchases;
    }
}