import { Demand } from "./demand";
import { Medicine } from "./medicine";
import { Purchase } from "./purchase";

export class Pharmacy {

    name: string;
    address: string;
    medicines: Medicine[] | undefined;
    demands: Demand[] | undefined;
    purhases: Purchase[] | undefined;

    // constructor(name: string, address:string, medicines: Medicine[] | undefined, demands: Demand[] | undefined, purhases: Purchase[] | undefined) {
    constructor(name: string, address:string) {
        this.name = name;
        this.address = address;
        this.medicines = <Medicine[]>[];
        this.demands = <Demand[]>[];
        this.purhases = <Purchase[]>[];
    }
}