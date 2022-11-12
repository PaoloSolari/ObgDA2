import { Token } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, Observable, of, take } from 'rxjs';
import { Invitation } from 'src/app/models/invitation';
import { Pharmacy } from 'src/app/models/pharmacy';
import { Session } from 'src/app/models/session';
import { RoleUser } from 'src/app/models/user';
import { SessionService } from 'src/app/services/session.service';
import { ICreateInvitation } from '../../interfaces/create-invitation';
import { InvitationService } from '../../services/invitation.service';
import { PharmacyService } from '../../services/pharmacy.service';
import { Globals } from '../../utils/globals';
import { INIT, INVITATION_LIST_URL } from '../../utils/routes';

@Component({
    selector: 'app-invitation-form',
    templateUrl: './invitation-form.component.html',
    styleUrls: ['./invitation-form.component.css']
})
export class InvitationFormComponent implements OnInit {

    public backUrl = `/${INIT}`;
    // public actualSession: Session = new Session(null, null, null);
    public pharmacyDB: Pharmacy = new Pharmacy(null, null, null, null, null);

    // [En caso de modificación de invitación]
    public name: string = '';
    public code: number = 0;
    public role: string = '';

    public invitationForm = new FormGroup({
        pharmacyControl: new FormControl<Pharmacy | null>(null, Validators.required),
        nameUser: new FormControl(),
        roleControl: new FormControl<string | null>(null, Validators.required),
        codeUser: new FormControl(),
    })

    pharmacyControl = new FormControl<Pharmacy | null>(null, Validators.required);
    pharmacies: Pharmacy[] = []
    roleControl = new FormControl<string | null>(null, Validators.required);
    roles: string[] = ['Administrador', 'Dueño', 'Empleado'];

    public isEditing = false;
    private invitationId?: string;

    constructor(
        private _pharmacyService: PharmacyService,
        private _invitationService: InvitationService,
        private _sessionService : SessionService,
        private _route: ActivatedRoute,
        private _router: Router,
    ) { }


    public get pharmacyForm() { return this.invitationForm.value.pharmacyControl!; }
    public get userNameForm() { return this.invitationForm.value.nameUser!; }
    public get userRoleForm() { return this.invitationForm.value.roleControl!; }
    public get userCodeForm() { return this.invitationForm.value.codeUser!; }

    ngOnInit(): void {
        
        Globals.selectTab = 0;

        // [En caso de haber entrado a la SPA de modificar una invitación]
        const id = this._route.snapshot.params?.['id'];
        if (!!id && id !== 'new') {
            this.isEditing = true;
            this.invitationId = id;
            this.backUrl = `/${INVITATION_LIST_URL}`; // [Para volver correctamente atrás]
            this._invitationService.getInvitationByName(id).pipe(
                take(1),
                catchError((err) => {
                    console.log({ err });
                    return of(err);
                }),
            ).subscribe((invitation: Invitation) => {
                this.setInvitation(invitation);
            });
        }
        
        // [Obtener la sesión actual]
        // this._sessionService.getSessionByToken('XXYYZZ')
        // .pipe(
        //     take(1),
        //     catchError((err) => {
        //         console.log({err});
        //         return of(err);
        //     }),   
        // ).subscribe((session: Session) => {
        //     this.setSession(session);
        // })

        // [Obtener las farmacias de la BD para colocar en el selector de farmacias]
        this._pharmacyService.getPharmacies()
        .pipe(
            take(1),
            catchError((err) => {
                console.log({err});
                return of(err);
            }),
        )
        .subscribe((pharmaciesDB: Pharmacy[] | undefined) => {
            this.pharmacies = pharmaciesDB!;
        })

    }

    private setInvitation(invitation: Invitation): void {
        this.invitationForm.value.nameUser = invitation.userName!;
        this.code = invitation.userCode!;
    }

    // private setSession = (session: Session) => {
    //     this.actualSession.idSession = session.idSession;
    //     this.actualSession.userName = session.userName;
    //     this.actualSession.token = session.token;
    // }

    public createInvitation(): void {
        // [Me traigo desde la BD la farmacia elegida por el administrador]
        this._pharmacyService.getPharmacyByName(this.pharmacyForm.name!).subscribe((pharmacy: Pharmacy) => {
            this.pharmacyDB = pharmacy;
            // [Obtenida la farmacia, creo la invitación]
            this.sendInvitation(this.pharmacyDB);
        })
    }

    private sendInvitation(pharmacyDB: Pharmacy): void {
        
        const invitationToAdd: ICreateInvitation = {
            Pharmacy: this.pharmacyDB,
            UserRole: this.getRole(this.userRoleForm),
            UserName: this.userNameForm,
        };

        this._invitationService.postInvitation(invitationToAdd)
        .pipe(
            take(1),
            catchError((err) => {
                console.log({err});
                return of(err);
            }),
        )
        .subscribe((userCode: any) => {
            // [El endpoint devuelve el código de usuario]
            if(userCode) {
                alert('El código de usuario es: ' + userCode);
                this.cleanForm();
            }
        });
    }

    private getRole(role: string): RoleUser {
        if(role == 'Administrador') return RoleUser.Administrator;
        if(role == 'Dueño') return RoleUser.Owner;
        return RoleUser.Employee;
    }

    private getRoleReverse(role: RoleUser): string {
        if(role == RoleUser.Administrator) return 'Administrador';
        if(role == RoleUser.Owner) return 'Dueño';
        return 'Empleado';
    }

    private updateInvitation(): void {
        // [Me traigo desde la BD la nueva farmacia para la invitación]
        this._pharmacyService.getPharmacyByName(this.pharmacyForm.name!).subscribe((pharmacy: Pharmacy) => {
            this.pharmacyDB = pharmacy;
            // [Obtenida la farmacia, procedo a modificar la invitación]
            this.modifyInvitation();
        })        
    }

    private modifyInvitation(): void {
        if(!!this.invitationId) {
            const invitation = new Invitation(
                this.invitationId as string,
                this.pharmacyDB,
                this.getRole(this.userRoleForm),
                this.userNameForm,
                this.userCodeForm,
                false,
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
                if(invitation) {
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

    public cleanForm(): void{
        this.invitationForm.reset();
    }

}
