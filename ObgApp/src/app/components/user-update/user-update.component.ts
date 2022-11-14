import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, of, take } from 'rxjs';
import { IUpdateUser } from 'src/app/interfaces/update-user';
import { Invitation } from 'src/app/models/invitation';
import { RoleUser, User } from 'src/app/models/user';
import { InvitationService } from 'src/app/services/invitation.service';
import { UserService } from 'src/app/services/user.service';
import { INIT, USER_FORM_URL } from 'src/app/utils/routes';

@Component({
    selector: 'app-user-update',
    templateUrl: './user-update.component.html',
    styleUrls: ['./user-update.component.css']
})
export class UserUpdateComponent implements OnInit {

    // public backUrl = `/${USER_FORM_URL}`;
    
    public userRole: RoleUser = RoleUser.Administrator;
    public userName: string = '';

    public userUpdateForm = new FormGroup({
        // name: new FormControl(),
        email: new FormControl(),
        password: new FormControl(),
        address: new FormControl(),
    })

    // public get nameForm() { return this.userUpdateForm.value.name; }
    public get emailForm() { return this.userUpdateForm.value.email; }
    public get passwordForm() { return this.userUpdateForm.value.password; }
    public get addressForm() { return this.userUpdateForm.value.address; }

    constructor(
        private _userService: UserService,
        private _invitationService: InvitationService,
        private _router: Router,
        private _route: ActivatedRoute,
    ) { }

    ngOnInit(): void {
        // [Obtenemos el rol en función del nombre de usuario]
        // const name = this._route.snapshot.params?.['id'];
        const name = this._route.snapshot.params?.['id']; // [Nos quedamos con el nombre del usuario a modificar]
        console.log(name); // Ok.
        this.userName = name;
        // if (!!name) {
        //     this._invitationService.getInvitationByName(name)
        //     .pipe(
        //         take(1),
        //         catchError((err) => {
        //             console.log({ err });
        //             return of(err);
        //         }),
        //     )
        //     .subscribe((invitation: Invitation) => {
        //         this.userRole = invitation.userRole!;
        //     });
        // }


    }

    public updateUser(): void {
        if(this.userUpdateForm.valid) {
            // const date = new Date();
            const userToUpdate: IUpdateUser = {
                // Name: this.nameForm,
                Email: this.emailForm,
                Password: this.passwordForm,
                Address: this.addressForm,
                RegisterDate: new Date().toLocaleDateString(), // [Para obtener la fecha de registro actual]
            }
            this._userService.putUser(userToUpdate, this.userName)
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
            .subscribe((user: User) => {
                // [El endpoint devuelve el nombre del usuairo actualizado]
                if(user) {
                    alert('El usuario ' + user + ' ha sido registrado correctamente.');
                    this._router.navigateByUrl(INIT);
                }
            })
        }
    }

}
