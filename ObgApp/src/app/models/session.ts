export class Session {
    IdSession: string | null;
    UserName: string | null;
    Token: string | null;

    constructor(IdSession: string | null, UserName: string | null, Token: string | null) {
        this.IdSession = IdSession;
        this.UserName = UserName;
        this.Token = Token;
    }
}