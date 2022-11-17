import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { catchError, of, take } from 'rxjs';
import { ICreateUser } from '../../interfaces/create-user';
import { Employee } from '../../models/employee';
import { User } from '../../models/user';
import { UserService } from '../../services/user.service';
import { Globals } from '../../utils/globals';
import { getUserFormUrl, INIT, USER_FORM_URL, USER_UPDATE_URL } from '../../utils/routes';

@Component({
    selector: 'app-user-form',
    templateUrl: './user-form.component.html',
    styleUrls: ['./user-form.component.css']
})
export class UserFormComponent implements OnInit {

    public backUrl = `/${INIT}`;
    public reset: boolean = false;

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
        Globals.userOk = true; // [Dejamos listo para el correcto registro de usuario]
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
                    if(err.status != 200){
                        alert(`${err.error.errorMessage}`);
                        console.log(`Error: ${err.error.errorMessage}`)
                        Globals.userOk = false; // [Hacemos que el registro no siga avanzando]
                        this.cleanForm(); // [Limpiamos el formulario]
                    } else {
                        console.log(`Ok: ${err.error.text}`);
                    }
                    return of(err);
                }))
            )
            .subscribe((user: User) => {
                // [El endpoint nos devuelve el nombre del usuario registrado]
                if(user) {
                    if(Globals.userOk){
                        // this._router.navigateByUrl(USER_UPDATE_URL);
                        this.navigateToConfirmUser(this.nameForm);
                    }
                }
            })
        }
    }

    public cleanForm(): void{
        this.formReset(this.userForm);
    }

    formReset(form: FormGroup) {

        form.reset();
    
        Object.keys(form.controls).forEach(key => {
          form.get(key)!.setErrors(null) ; // [El '!' no estaba, me lo exigía]
        });
    }

    public navigateToConfirmUser(name: string): void {
        this._router.navigateByUrl(`/${getUserFormUrl(name)}`)
    }

}