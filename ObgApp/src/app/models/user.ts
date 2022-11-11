export enum RoleUser {
    Administrator,
    Owner,
    Employee
}

export class User {

    name: string | null;
    code: number | null;
    email: string | null;
    password: string | null;
    address: string | null;
    role: RoleUser | null;
    registerDate: string | null;

    constructor(name: string | null, code: number | null, email: string | null, password: string | null, address: string | null, role: RoleUser | null, registerDate: string | null) {
        this.name = name;
        this.code = code;
        this.email = email;
        this.password = password;
        this.address = address;
        this.role = role;
        this.registerDate = registerDate;
    }
}