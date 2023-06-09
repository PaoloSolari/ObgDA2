import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { catchError, of, take } from 'rxjs';
import { ICreateSession } from '../../interfaces/create-session';
import { Session } from '../../models/session';
import { AuthService } from '../../services/auth.service';
import { SessionService } from '../../services/session.service';
import { Globals } from '../../utils/globals';
import { INIT } from '../../utils/routes';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    public backUrl = `/${INIT}`;
    private continue: boolean = false;

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
        this.continue = false;
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
                    if(err.status != 200){
                        alert(`${err.error.errorMessage}`);
                        console.log(`Error: ${err.error.errorMessage}`)
                    } else {
                        console.log(`Ok: ${err.error.text}`);
                        this.continue = true;
                    }
                    return of(err);
                }))
            )
            .subscribe((session: Session) => {
                if (session) {
                    if(this.continue){
                        this._sessionService.getSessionByName(this.userNameForm)
                            .pipe(
                                take(1),
                                catchError((err => {
                                    if(err.status != 200){
                                        alert(`${err.error.errorMessage}`);
                                        console.log(`Error: ${err.error.errorMessage}`)
                                    } else {
                                        console.log(`Ok: ${err.error.text}`);
                                    }
                                    return of(err);
                                }))
                            )
                            .subscribe((session: Session) => {
                                if(session) {
                                    Globals.alreadyLogin = true; // [Ahora, ya hay un usuario logeado]
                                    Globals.selectTab = 0; // [Para comenzar en la pestaña del usuario]
                                    this._authService.setToken(session.token!);
                                    this._router.navigateByUrl(INIT);
                                }
                            })
                    }
                }
            })
        }
    }

}
