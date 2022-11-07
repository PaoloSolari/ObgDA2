import { Pharmacy } from "./pharmacy";
import { RoleUser, User } from "./user";

export class Administrator extends User {

    Pharmacies: Pharmacy[] | null;

    constructor(Name: string | null, Code: number | null, Email: string | null, Password: string | null, Address: string | null, Role: RoleUser | null, RegisterDate: string | null, Pharmacies: Pharmacy[] | null) {
        super(Name, Code, Email, Password, Address, Role, RegisterDate);
        this.Pharmacies = Pharmacies;
    }
}