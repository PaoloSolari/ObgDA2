import { Pharmacy } from "./pharmacy";
import { RoleUser, User } from "./user";

export class Owner extends User {

    pharmacy: Pharmacy | null;

    constructor(name: string | null, code: number | null, email: string | null, password: string | null, address: string | null, role: RoleUser | null, registerDate: string | null, pharmacy: Pharmacy | null) {
        super(name, code, email, password, address, role, registerDate);
        this.pharmacy = pharmacy;
    }
}