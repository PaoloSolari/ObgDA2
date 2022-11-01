import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent, TabGroupBasicExample } from './app.component';
import { MedicineListComponent } from './components/medicine-list/medicine-list.component';
import { MedicineItemComponent } from './components/medicine-item/medicine-item.component';
import { MedicineFormComponent } from './components/medicine-form/medicine-form.component';
import { MenuEmployeeComponent } from './components/menu-employee/menu-employee.component';
import { MenuOwnerComponent } from './components/menu-owner/menu-owner.component';
import { MenuAdministratorComponent } from './components/menu-administrator/menu-administrator.component';
import { BackButtonComponent } from './components/back-button/back-button.component';
import { LoaderComponent } from './components/loader/loader.component';
import { InitComponent } from './components/init/init.component';
import { DemandFormComponent } from './components/demand-form/demand-form.component';
import { PurchaseListComponent } from './components/purchase-list/purchase-list.component';
import { InvitationFormComponent } from './components/invitation-form/invitation-form.component';
import { InvitationListComponent } from './components/invitation-list/invitation-list.component';
import { DemandListComponent } from './components/demand-list/demand-list.component';

// Angular Material (luego se agreagan a los imports)
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTabsModule } from '@angular/material/tabs';
import {MatButtonModule} from '@angular/material/button';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatIconModule} from '@angular/material/icon';
// Form
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';


import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { PharmacyFormComponent } from './components/pharmacy-form/pharmacy-form.component';
import { RouterModule } from '@angular/router';

@NgModule({
    schemas: [CUSTOM_ELEMENTS_SCHEMA], // Para solucionar el tema de Angular Material
    declarations: [
        AppComponent,
        MedicineListComponent,
        MedicineItemComponent,
        MedicineFormComponent,
        MenuEmployeeComponent,
        MenuOwnerComponent,
        MenuAdministratorComponent,
        BackButtonComponent,
        LoaderComponent,
        InitComponent,
        DemandFormComponent,
        PurchaseListComponent,
        PharmacyFormComponent,
        InvitationFormComponent,
        InvitationListComponent,
        DemandListComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule, // Para el [RouterModule]
        // Angular Material:
        BrowserAnimationsModule,
        MatTabsModule,
        MatButtonModule,
        MatToolbarModule,
        MatIconModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
