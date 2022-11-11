export class Session {
    idSession: string | null;
    userName: string | null;
    token: string | null;

    constructor(idSession: string | null, userName: string | null, token: string | null) {
        this.idSession = idSession;
        this.userName = userName;
        this.token = token;
    }
}