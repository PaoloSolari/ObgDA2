import { Component } from '@angular/core';
import { AuthService } from './services/auth.service';
import { Globals } from './utils/globals';
import { LOGIN, USER_FORM_URL } from './utils/routes';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    title = 'ObgApp';

    public addUser = `/${USER_FORM_URL}`;
    public login = `/${LOGIN}`;

    constructor(
        private _authService: AuthService,
    ) { }
    
    ngOnInit() { }

    public logout(){
        if(Globals.alreadyLogin) {
            // alert('El token: ' + this._authService.getToken() + ' dejará de estar activo.');
            this._authService.removeToken();
            // alert('El token: ' + this._authService.getToken() + ' no debería estar definido.');
            Globals.alreadyLogin = false;
        } else {
            console.log('El usuario ya fue deslogueado.')
        }
    }

    get selectTab() {
        return Globals.selectTab;
    }

    public isLogin(): boolean {
        return Globals.alreadyLogin;
    }

}

// export class TabGroupBasicExample { }