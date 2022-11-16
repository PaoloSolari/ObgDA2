import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { INIT } from '../utils/routes';

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {

    constructor(
        private _authService: AuthService,
        private _router: Router,
    ) { }

    canActivate(): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
        if (!this._authService.isAuthenticated()) {
            // this._router.navigateByUrl(LOGIN); // deberían redireccionar al login
            this._router.navigateByUrl(INIT); // deberían redireccionar al login
            return false;
        }
        return true;
    }

}
