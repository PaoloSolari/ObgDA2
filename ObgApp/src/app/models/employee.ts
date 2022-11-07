import { Pharmacy } from "./pharmacy";
import { RoleUser, User } from "./user";

export class Employee extends User {

    Pharmacy: Pharmacy | null;

    constructor(Name: string | null, Code: number | null, Email: string | null, Password: string | null, Address: string | null, Role: RoleUser | null, RegisterDate: string | null, Pharmacy: Pharmacy | null) {
        super(Name, Code, Email, Password, Address, Role, RegisterDate);
        this.Pharmacy = Pharmacy;
    }
}