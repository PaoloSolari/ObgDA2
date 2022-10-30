import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { INIT, MEDICINE_LIST_URL } from '../../utils/routes';
import { ICreateMedicine } from '../../interfaces/create-medicine';
import { MedicineService } from '../../services/medicine.service';
import { ValidateString } from '../../validators/string.validator';

@Component({
    selector: 'app-medicine-form',
    templateUrl: './medicine-form.component.html',
    styleUrls: ['./medicine-form.component.css']
})
export class MedicineFormComponent implements OnInit {

    public backUrl = `/${INIT}`;

    public medicineForm = new FormGroup({
        code: new FormControl(undefined, [Validators.required, ValidateString]),
        name: new FormControl(undefined, [Validators.required, ValidateString]),
        symtompsItTreats: new FormControl(undefined, [Validators.required, ValidateString]),
        presentation: new FormControl(undefined, [Validators.required]),
        quantity: new FormControl(undefined, [Validators.required, Validators.min(0)]),
        unit: new FormControl(undefined, [Validators.required, ValidateString]),
        price: new FormControl(undefined, [Validators.required, Validators.min(0)]),
        stock: new FormControl(undefined, [Validators.required, Validators.min(0)]),
        prescription: new FormControl(undefined, [Validators.required]),
        isActive: new FormControl(undefined, [Validators.required])
    })

    constructor(
        private _medicineService: MedicineService,
        private _router: Router,
    ) { }

    public get code() { return this.medicineForm.get('code'); }
    public get name() { return this.medicineForm.get('name'); }
    public get symtompsItTreats() { return this.medicineForm.get('symtompsItTreats'); }
    public get presentation() { return this.medicineForm.get('presentation'); }
    public get quantity() { return this.medicineForm.get('quantity'); }
    public get unit() { return this.medicineForm.get('unit'); }
    public get price() { return this.medicineForm.get('price'); }
    public get stock() { return this.medicineForm.get('stock'); }
    public get prescription() { return this.medicineForm.get('prescription'); }
    public get isActive() { return this.medicineForm.get('isActive'); }

    public ngOnInit(): void {}

    public createMedicine(): void {
        if(this.medicineForm.valid) {
            const medicine : ICreateMedicine = {
                code: this.medicineForm.value.code!, // El '!' es para prometerle a TypeScript que no va a ser null.
                name: this.medicineForm.value.name!,
                symtompsItTreats: this.medicineForm.value.symtompsItTreats!,
                presentation: this.medicineForm.value.presentation!,
                quantity: this.medicineForm.value.quantity!,
                unit: this.medicineForm.value.unit!,
                price: this.medicineForm.value.price!,
                stock: this.medicineForm.value.stock!,
                prescription: this.medicineForm.value.prescription!,
                isActive:  this.medicineForm.value.isActive!,
            };
            const medicineId = this._medicineService.postMedicine(medicine);
            if(!!medicineId) { // ¿What's this?
                alert('Medicamento creado correctamente con código: ' + medicineId)
                this._router.navigateByUrl(MEDICINE_LIST_URL);
            } else{
                alert('El medicamento no se ha creado correctamente con código: ' + medicineId)
            }
        }
    }

}
