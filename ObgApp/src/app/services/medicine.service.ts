import { compileDeclareClassMetadata } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { ICreateMedicine } from '../interfaces/create-medicine';
import { Medicine } from '../models/medicine';

@Injectable({
    providedIn: 'root'
})

export class MedicineService {

    private _medicines: Medicine[] | undefined;

    constructor() { 
        this._medicines = this.initializeMedicines();
    }

    private initializeMedicines(): Medicine[] {
        return [
            new Medicine("XXYYZZ", "Paracetamol", "Dolor de cabeza", 20, 0, "20mL", 150, 0, false, false)
        ];
    }

    public getMedicines(): Medicine[] {
        return this._medicines ?? [];
    }

    public postMedicine(medicine: ICreateMedicine): string {
        if(!this._medicines) this._medicines = [];
        // const code = this.generateId(20);
        const medicineToAdd = new Medicine(medicine.code, medicine.name, medicine.symtompsItTreats, medicine.presentation, medicine.quantity, medicine.unit, medicine.price, medicine.stock, medicine.prescription, medicine.isActive);
        this._medicines.push(medicineToAdd);
        return medicine.code;
    }

    // private dec2hex(dec: number) {
    //     return dec.toString(16).padStart(2, "0")
    // }

    // private generateId(len: number): string {
    //     var arr = new Uint8Array((len || 40) / 2)
    //     window.crypto.getRandomValues(arr)
    //     return Array.from(arr, this.dec2hex).join('')
    // }

}
