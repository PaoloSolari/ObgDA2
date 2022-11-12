import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { catchError, of, take } from 'rxjs';
import { ICreateUser } from 'src/app/interfaces/create-user';
import { Employee } from 'src/app/models/employee';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import { getUserFormUrl, INIT, USER_FORM_URL, USER_UPDATE_URL } from 'src/app/utils/routes';

@Component({
    selector: 'app-user-form',
    templateUrl: './user-form.component.html',
    styleUrls: ['./user-form.component.css']
})
export class UserFormComponent implements OnInit {

    public backUrl = `/${INIT}`;

    public userForm = new FormGroup({
        name: new FormControl(),
        code: new FormControl(),
    })

    public get nameForm() { return this.userForm.value.name!; }
    public get codeForm() { return this.userForm.value.code!; }

    constructor(
        private _userService: UserService,
        private _router: Router,
    ) { }

    ngOnInit(): void {
    }

    public createUser(): void{
        if(this.userForm.valid) {
            const userToAdd: ICreateUser = {
                Name: this.nameForm,
                Code: this.codeForm,
            };
            this._userService.postUser(userToAdd)
            .pipe(
                take(1),
                catchError((err => {
                    console.log({err});
                    return of(err);
                }))
            )
            .subscribe((user: User) => {
                // [El endpoint nos devuelve el nombre del usuario registrado]
                if(user) {
                    // this._router.navigateByUrl(USER_UPDATE_URL);
                    this.navigateToConfirmUser(this.nameForm);
                }
            })
        }
    }

    
    public navigateToConfirmUser(name: string): void {
        this._router.navigateByUrl(`/${getUserFormUrl(name)}`)
    }

}