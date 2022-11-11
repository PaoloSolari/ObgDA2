import { Pharmacy } from "../models/pharmacy";
import { RoleUser } from "../models/user";

export interface ICreateUser {
    Name: string | null;
    Code: number | null;
}

export interface ICreateEmployee {
    Name: string | null;
    Code: number | null;
    Pharmacy: Pharmacy | null;
}


    // Address: string | null; // (Default = null)
    // Email: string | null;
    // Password: string | null; // (Default = null)
    // RegisterDate: string | null; // (Default = null)
    // Role: RoleUser | null; // (Default = en la siguiente SPA se le asigna)