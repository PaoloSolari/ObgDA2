import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MedicineListComponent } from './components/medicine-list/medicine-list.component';
import { MedicineItemComponent } from './components/medicine-item/medicine-item.component';
import { MedicineFormComponent } from './components/medicine-form/medicine-form.component';

@NgModule({
  declarations: [
    AppComponent,
    MedicineListComponent,
    MedicineItemComponent,
    MedicineFormComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
