import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { DemandFormComponent } from './components/demand-form/demand-form.component';
import { DemandListComponent } from './components/demand-list/demand-list.component';
import { InitComponent } from './components/init/init.component';
import { InvitationFormComponent } from './components/invitation-form/invitation-form.component';
import { InvitationListComponent } from './components/invitation-list/invitation-list.component';
import { LoginComponent } from './components/login/login.component';
import { MedicineFormComponent } from './components/medicine-form/medicine-form.component';
import { MedicineListComponent } from './components/medicine-list/medicine-list.component';
import { PharmacyFormComponent } from './components/pharmacy-form/pharmacy-form.component';
import { PurchaseListComponent } from './components/purchase-list/purchase-list.component';
import { UserFormComponent } from './components/user-form/user-form.component';
import { UserUpdateComponent } from './components/user-update/user-update.component';
import { DEMAND_FORM_URL, DEMAND_LIST_URL, INIT, INVITATION_FORM_URL, INVITATION_LIST_URL, LOGIN, MEDICINE_FORM_URL, MEDICINE_LIST_URL, PHARMACY_FORM_URL, PURCHASE_LIST_URL, USER_FORM_URL, USER_UPDATE_URL } from './utils/routes';

const routes: Routes = [
    
    // Login:
    { path: LOGIN, component: LoginComponent },

    // Register User:
    { path: USER_FORM_URL, component: UserFormComponent },
    { path: USER_UPDATE_URL, component: UserUpdateComponent },
    
    // Menu principal:
    { path: INIT, component: InitComponent },

    // Administrator:
    { path: PHARMACY_FORM_URL, component: PharmacyFormComponent },
    { path: INVITATION_FORM_URL, component: InvitationFormComponent },
    { path: INVITATION_LIST_URL, component: InvitationListComponent },

    // Owner:
    { path: DEMAND_LIST_URL, component: DemandListComponent },

    // Employee:
    { path: MEDICINE_FORM_URL, component: MedicineFormComponent },
    { path: MEDICINE_LIST_URL, component: MedicineListComponent },
    { path: DEMAND_FORM_URL, component: DemandFormComponent },
    { path: PURCHASE_LIST_URL, component: PurchaseListComponent },
    
    { path: '**', redirectTo: '' } // Este siempre debe ir al final, sino no funciona el resto.

];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
