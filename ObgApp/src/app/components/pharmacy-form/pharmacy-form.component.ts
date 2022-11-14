import { Component, Injectable, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Globals } from '../../utils/globals';
import { INIT } from '../../utils/routes';
import { PharmacyService } from '../../services/pharmacy.service';
import { Router } from '@angular/router';
import { ICreatePharmacy } from 'src/app/interfaces/create-pharmacy';
import { NoSpace } from 'src/app/validators/noEmptyString.validator';
import { catchError, of, take } from 'rxjs';
import { Pharmacy } from 'src/app/models/pharmacy';
import { AuthService } from 'src/app/services/auth.service';

@Component({
    selector: 'app-pharmacy-form',
    templateUrl: './pharmacy-form.component.html',
    styleUrls: ['./pharmacy-form.component.css'],
})
export class PharmacyFormComponent implements OnInit {

    public backUrl = `/${INIT}`;

    public pharmacyForm = new FormGroup({
        name: new FormControl(),
        address: new FormControl(),
    })

    constructor(
        private _pharmacyService: PharmacyService,
        private _authService: AuthService,
    ) { }

    public get nameForm() { return this.pharmacyForm.value.name; }
    public get addressForm() { return this.pharmacyForm.value.address; }
    
    ngOnInit(): void {
        Globals.selectTab = 0;
    }

    public createPharmacy(): void {
        if (this.pharmacyForm.valid) {
            const pharmacyToAdd: ICreatePharmacy = {
                Name: this.nameForm!,
                Address: this.addressForm!,
            };
            this._pharmacyService.postPharmacy(pharmacyToAdd, this._authService.getToken()!)
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
            .subscribe((pharmacy: Pharmacy) => {
                // [El endpoint devuelve el nombre de la farmacia recién creada]
                if(pharmacy) {
                    alert('Farmacia creada');
                    this.cleanForm();
                }
            })
        }
    }

    public cleanForm(): void{
        this.pharmacyForm.reset();
    }

}