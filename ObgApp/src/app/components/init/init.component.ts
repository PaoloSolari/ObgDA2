import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { catchError, of, take } from 'rxjs';
import { Session } from 'src/app/models/session';
import { RoleUser, User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { SessionService } from 'src/app/services/session.service';
import { UserService } from 'src/app/services/user.service';
import { Globals } from 'src/app/utils/globals';

@Component({
    selector: 'app-init',
    templateUrl: './init.component.html',
    styleUrls: ['./init.component.css'],
})
export class InitComponent implements OnInit {

    public selectTab: number = Globals.selectTab;
    public activedRole: string = '';
    public alreadyLogin: boolean = Globals.alreadyLogin;

    constructor(
        private _sessionService: SessionService,
        private _authService: AuthService,
        private _userService: UserService,
    ) { }

    ngOnInit(): void {

        // [Obtener la sesión actual para obtener el nombre y a partir de él, setear el rol]
        if (this.alreadyLogin) {
            this._sessionService.getSessionByToken(this._authService.getToken()!)
                .pipe(
                    take(1),
                    catchError((err => {
                        if (err.status != 200) {
                            alert(`${err.error.errorMessage}`);
                            console.log(`Error: ${err.error.errorMessage}`)
                        } else {
                            console.log(`Ok: ${err.error.text}`);
                        }
                        return of(err);
                    }))
                )
                .subscribe((session: Session) => {
                    this._userService.getUserByName(session.userName!)
                        .pipe(
                            take(1),
                            catchError((err => {
                                if (err.status != 200) {
                                    alert(`${err.error.errorMessage}`);
                                    console.log(`Error: ${err.error.errorMessage}`)
                                } else {
                                    console.log(`Ok: ${err.error.text}`);
                                }
                                return of(err);
                            }))
                        )
                        .subscribe((user: User) => {
                            if (user) {
                                this.activedRole = this.getRoleReverse(user.role!);
                                this._authService.setUserRole(this.activedRole);
                            }
                        })
                })
        }
    }

    private getRoleReverse(role: RoleUser): string {
        if (role == RoleUser.Administrator) return 'Administrator';
        if (role == RoleUser.Owner) return 'Owner';
        if (role == RoleUser.Employee) return 'Employee';
        return 'User';
    }

    public isLogin() {
        return Globals.alreadyLogin;
    }

    public actualRole(role: string): boolean {
        return role == this._authService.getUserRole();
    }

}
