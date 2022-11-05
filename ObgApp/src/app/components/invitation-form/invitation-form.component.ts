import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
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
    })

    pharmacyControl = new FormControl<string | null>(null, Validators.required);
    pharmacies: string[] = ['Farmashop'];
    roleControl = new FormControl<string | null>(null, Validators.required);
    roles: string[] = ['Administrador', 'Dueño', 'Empleado'];

    constructor(
        private _pharmacyService: PharmacyService,
        private _invitationService: InvitationService, 
        private _router: Router,
    ) { }

    public get pharmacyForm() { return this.invitationForm.value.pharmacyControl!; }
    public get userNameForm() { return this.invitationForm.value.nameUser!; }
    public get userRoleForm() { return this.invitationForm.value.roleControl!; }

    ngOnInit(): void {
        Globals.selectTab = 0;
    }

    public createInvitation(): void {
        if(this.invitationForm.valid) {
            const invitationFromForm: ICreateInvitation = {
                pharmacy: this.pharmacyForm,
                userRole: this.userRoleForm,
                userName: this.userNameForm,
            };
            const idInvitation = this._invitationService.postInvitation(invitationFromForm);
            if(idInvitation) {
                alert('Invitación con código ' + idInvitation + ' creada correctamente.');
                this.clearForm();
            }
        } else {
            alert('Debe ingresar todos los datos solicitados');
        }
    }

    public clearForm() {
        this.invitationForm.reset();
    }

}
