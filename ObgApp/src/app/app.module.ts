// Módulos:
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

// Router (permitir SPA):
import { AppRoutingModule } from './app-routing.module';
import { RouterModule } from '@angular/router';

// Componentes:
import { AppComponent } from './app.component';
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
import { PharmacyFormComponent } from './components/pharmacy-form/pharmacy-form.component';

// Angular Material (luego se agreagan a los 'imports'):
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTabsModule } from '@angular/material/tabs';
import { MatButtonModule } from '@angular/material/button';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatTableModule } from '@angular/material/table';
// Form (Angular Material)
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';

// Servicios (luego se agregan a los 'providers'):
import { PharmacyService } from './services/pharmacy.service';
import { MedicineService } from './services/medicine.service';
import { LoadingService } from './services/loading.service';
import { InvitationService } from './services/invitation.service';

// Conexión con el BackEnd:
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { UserFormComponent } from './components/user-form/user-form.component';
import { UserUpdateComponent } from './components/user-update/user-update.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { DemandService } from './services/demand.service';
import { SessionService } from './services/session.service';
import { UserService } from './services/user.service';
import { LoadingInterceptor } from './interceptors/loading.interceptor';
import { AuthService } from './services/auth.service';
import { AuthGuard } from './guards/auth.guard';
import { RoleGuard } from './guards/role.guard';
import { LoginComponent } from './components/login/login.component';
import { BuyComponent } from './components/buy/buy.component';
import { MenuBuyComponent } from './components/menu-buy/menu-buy.component';
import { BuyListComponent } from './components/buy-list/buy-list.component';
import { ExporterFormComponent } from './components/exporter-form/exporter-form.component';


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
        DemandListComponent,
        UserFormComponent,
        UserUpdateComponent,
        LoginComponent,
        BuyComponent,
        MenuBuyComponent,
        BuyListComponent,
        ExporterFormComponent
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
        MatCardModule,
        MatChipsModule,
        MatTableModule,
        // Conexión con BackEnd:
        HttpClientModule,
    ],
    providers: [
        AuthService,
        DemandService,
        InvitationService,
        LoadingService, 
        MedicineService, 
        PharmacyService,
        SessionService,
        UserService, 
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true,
        },
        // {
        //     provide: HTTP_INTERCEPTORS,
        //     useClass: LoadingInterceptor,
        //     multi: true,
        // },
        AuthGuard,
        RoleGuard,
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
