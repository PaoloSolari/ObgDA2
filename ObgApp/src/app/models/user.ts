export enum RoleUser {
    Administrator,
    Owner,
    Employee
}

export class User {

    Name: string | null;
    Code: number | null;
    Email: string | null;
    Password: string | null;
    Address: string | null;
    Role: RoleUser | null;
    RegisterDate: string | null;

    constructor(Name: string | null, Code: number | null, Email: string | null, Password: string | null, Address: string | null, Role: RoleUser | null, RegisterDate: string | null) {
        this.Name = Name;
        this.Code = Code;
        this.Email = Email;
        this.Password = Password;
        this.Address = Address;
        this.Role = Role;
        this.RegisterDate = RegisterDate;
    }
}