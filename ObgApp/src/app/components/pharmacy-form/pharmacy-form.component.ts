import { Component, Injectable, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ValidateString } from '../../validators/string.validator';
import { Globals } from '../../utils/globals';
import { INIT } from '../../utils/routes';
import { PharmacyService } from '../../services/pharmacy.service';
import { Router } from '@angular/router';
import { ICreateMedicine } from 'src/app/interfaces/create-medicine';
import { ICreatePharmacy } from 'src/app/interfaces/create-pharmacy';

@Component({
    selector: 'app-pharmacy-form',
    templateUrl: './pharmacy-form.component.html',
    styleUrls: ['./pharmacy-form.component.css']
})
export class PharmacyFormComponent implements OnInit {

    public backUrl = `/${INIT}`;

    public pharmacyForm = new FormGroup({
        // name: new FormControl(undefined, [Validators.required, ValidateString]),
        // address: new FormControl(undefined, [Validators.required, ValidateString]),
        name: new FormControl(),
        address: new FormControl(),
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