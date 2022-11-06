import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, of, take } from 'rxjs';
import { Invitation } from 'src/app/models/invitation';
import { ICreateInvitation } from '../../interfaces/create-invitation';
import { InvitationService } from '../../services/invitation.service';
import { PharmacyService } from '../../services/pharmacy.service';
import { Globals } from '../../utils/globals';
import { INIT } from '../../utils/routes';

@Component({
    selector: 'app-invitation-form',
    templateUrl: './invitation-form.component.html',
    styleUrls: ['./invitation-form.component.css']
})
export class InvitationFormComponent implements OnInit {

    public backUrl = `/${INIT}`;

    public invitationForm = new FormGroup({
        pharmacyControl: new FormControl<string | null>(null, Validators.required),
        nameUser: new FormControl(),
        roleControl: new FormControl<string | null>(null, Validators.required),
        codeUser: new FormControl(),
    })

    pharmacyControl = new FormControl<string | null>(null, Validators.required);
    pharmacies: string[] = ['Farmashop'];
    roleControl = new FormControl<string | null>(null, Validators.required);
    roles: string[] = ['Administrador', 'Dueño', 'Empleado'];

    public isEditing = false;
    private invitationId?: string;

    constructor(
        private _pharmacyService: PharmacyService,
        private _invitationService: InvitationService,
        private _router: Router,
        private _route: ActivatedRoute,
    ) { }

    public get pharmacyForm() { return this.invitationForm.value.pharmacyControl!; }
    public get userNameForm() { return this.invitationForm.value.nameUser!; }
    public get userRoleForm() { return this.invitationForm.value.roleControl!; }
    public get userCodeForm() { return this.invitationForm.value.codeUser!; }

    // public get pharmacy() { return this.invitationForm.get('pharmacy'); }
    // public get name() { return this.invitationForm.get('name'); }
    // public get role() { return this.invitationForm.get('role'); }
    // public get code() { return this.invitationForm.get('code'); }

    ngOnInit(): void {
        
        Globals.selectTab = 0;

        const id = this._route.snapshot.params?.['id'];
        console.log({ id });
        if (!!id && id !== 'new') {
            this.isEditing = true;
            this.invitationId = id;
            this._invitationService.getInvitationById(id).pipe(
                take(1),
                catchError((err) => {
                    console.log({ err });
                    return of(err);
                }),
            ).subscribe((invitation: Invitation) => {
                this.setInvitation(invitation);
            });
        }

    }

    private setInvitation(invitation: Invitation): void {
        this.pharmacyControl.setValue(invitation.pharmacy);
        this.userNameForm?.setValue(invitation.userName);
        this.roleControl?.setValue(invitation.userRole);
        this.userCodeForm?.setValue(invitation.userCode);
    }

    public createInvitation(): void {
        const invitationFromForm: ICreateInvitation = {
            pharmacy: this.pharmacyForm,
            userRole: this.userRoleForm,
            userName: this.userNameForm,
        };
        const idInvitation = this._invitationService.postInvitation(invitationFromForm)
        .pipe(
            take(1),
            catchError((err) => {
                console.log({err});
                return of(err);
            }),
        )
        .subscribe((invitation: Invitation) => {
            if(!!invitation?.idInvitation) {
                alert('Invitación con código ' + idInvitation + ' creada correctamente.');
                this.cleanForm();
            }
        });
    }

    private updateInvitation(): void {
        if(!!this.invitationId) {
            const invitation = new Invitation(
                this.invitationId as string,
                this.pharmacyForm,
                this.userRoleForm,
                this.userNameForm,
                this.userCodeForm,
                false, // Tal vez aquí me debería de traer la invitación de la BD para setearle el que tenía.
            );
            this._invitationService.putInvitation(invitation)
            .pipe(
                take(1),
                catchError((err) => {
                    console.log({err});
                    return of(err);
                }),
            )
            .subscribe((invitation: Invitation) => {
                if(!!invitation?.idInvitation) {
                    alert('Invitación modificada');
                }
            });
        }
    }

    public subitInvitation(): void {
        if(this.invitationForm.valid) {
            if(this.isEditing) {
                this.updateInvitation();
            } else {
                this.createInvitation();
            }
        }
    }

    public clearForm() {
        this.invitationForm.reset();
    }

    public cleanForm(): void{
        this.pharmacyControl?.setValue(null); // Chequear
        this.userNameForm?.setValue(undefined);
        this.roleControl?.setValue(null); // Chequear
        this.userCodeForm?.setValue(undefined);
    }

}
