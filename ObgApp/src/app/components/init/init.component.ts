import { Component, OnInit } from '@angular/core';
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
    styleUrls: ['./init.component.css']
})
export class InitComponent implements OnInit {

    public selectTab: number = Globals.selectTab;
    public activedRole: string = '';

    constructor(
        private _sessionService: SessionService,
        private _authService: AuthService,
        private _userService: UserService,
    ) { }

    ngOnInit(): void {

        // [Obtener la sesión actual para obtener el nombre y a partir de él, setear el rol]
        console.log(this._authService.getToken());
        this._sessionService.getSessionByToken(this._authService.getToken()!)
            .pipe(
                take(1),
                catchError((err) => {
                    console.log({ err });
                    return of(err);
                }),
            ).subscribe((session: Session) => {
                console.log(session.userName);
                this._userService.getUserByName(session.userName!).pipe(
                    take(1),
                    catchError((err) => {
                        console.log({ err });
                        return of(err);
                    }),
                ).subscribe((user: User) => {
                    console.log(user);
                    if (user) {
                        console.log(user.role);
                        this.activedRole = this.getRoleReverse(user.role!);
                        console.log('El rol obtenido es: ' + this.activedRole);
                    }
                })
            })
    }

    private getRoleReverse(role: RoleUser): string {
        if(role == RoleUser.Administrator) return 'Administrator';
        if(role == RoleUser.Owner) return 'Owner';
        return 'Employee';
    }

}
