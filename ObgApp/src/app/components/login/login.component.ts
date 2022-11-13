import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { catchError, of, take } from 'rxjs';
import { ICreateSession } from 'src/app/interfaces/create-session';
import { Session } from 'src/app/models/session';
import { AuthService } from 'src/app/services/auth.service';
import { SessionService } from 'src/app/services/session.service';
import { INIT } from 'src/app/utils/routes';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    public backUrl = `/${INIT}`;

    public loginForm = new FormGroup({
        userName: new FormControl(),
        password: new FormControl(),
    })

    public get userNameForm() { return this.loginForm.value.userName!; }
    public get passwordForm() { return this.loginForm.value.password!; }

    constructor(
        private _sessionService: SessionService,
        private _authService: AuthService,
        private _router: Router,
    ) { }

    ngOnInit(): void {
    }

    public login(): void {
        if(this.loginForm.valid) {
            const sessionToAdd: ICreateSession = {
                UserName: this.userNameForm,
            }
            let password = this.passwordForm;
            this._sessionService.postSession(sessionToAdd, password)
            .pipe(
                take(1),
                catchError((err => {
                    console.log({err});
                    return of(err);
                }))
            )
            .subscribe((session: Session) => {
                // [El endpoint retorna el Token]
                if (session) {
                    this._sessionService.getSessionByName(this.userNameForm)
                        .pipe(
                            take(1),
                            catchError((err => {
                                console.log({ err });
                                return of(err);
                            }))
                        )
                        .subscribe((session: Session) => {
                            if(session) {
                                console.log(session.token);
                                this._authService.setToken(session.token!);
                                this._router.navigateByUrl(INIT);
                            }
                        })
                }
            })
        }
    }

}
