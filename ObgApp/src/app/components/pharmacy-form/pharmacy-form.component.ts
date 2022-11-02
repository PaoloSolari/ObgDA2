import { Component, Injectable, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Globals } from '../../utils/globals';
import { INIT } from '../../utils/routes';
import { PharmacyService } from '../../services/pharmacy.service';
import { Router } from '@angular/router';
import { ICreatePharmacy } from 'src/app/interfaces/create-pharmacy';
import { NoSpace } from 'src/app/validators/noEmptyString.validator';

@Component({
    selector: 'app-pharmacy-form',
    templateUrl: './pharmacy-form.component.html',
    styleUrls: ['./pharmacy-form.component.css'],
    // encapsulation: ViewEncapsulation.ShadowDom,
})
export class PharmacyFormComponent implements OnInit {

    public backUrl = `/${INIT}`;

    public pharmacyForm = new FormGroup({
        name: new FormControl(undefined, [Validators.required, NoSpace]),
        address: new FormControl(undefined, [Validators.required, NoSpace]),
        // name: new FormControl(),
        // address: new FormControl(),
    })

    constructor(
        private _pharmacyService: PharmacyService,
    ) { }

    public get nameForm() { return this.pharmacyForm.value.name!; }
    public get addressForm() { return this.pharmacyForm.value.address!; }
    
    ngOnInit(): void {
        Globals.selectTab = 0;
    }

    public createPharmacy(): void {
        if (this.pharmacyForm.valid) {
            const pharmacyFromForm: ICreatePharmacy = {
                name: this.nameForm,
                address: this.addressForm,
            };
            const pharmacyName = this._pharmacyService.postPharmacy(pharmacyFromForm);
            if (pharmacyName) {
                alert('Farmacia de nombre ' + pharmacyFromForm.name + ' y dirección ' + pharmacyFromForm.address + ' creada correctamente.');
                this.clearForm();
            }
        } else {
            alert('Debe ingresar todos los datos solictados');
        }
    }

    public clearForm(){
        this.pharmacyForm.reset();
    }

}