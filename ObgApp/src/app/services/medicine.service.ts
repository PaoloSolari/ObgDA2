import { compileDeclareClassMetadata } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { ICreateMedicine } from '../interfaces/create-medicine';
import { Medicine } from '../models/medicine';

@Injectable({
    providedIn: 'root'
})

export class MedicineService {

    private _medicines: Medicine[] | undefined;

    constructor(
        // De momento nada...
    ) { 
        this._medicines = this.initializeMedicines();
    }

    private initializeMedicines(): Medicine[] {
        return [
            new Medicine("XXYYZZ", "Paracetamol", "Dolor de cabeza", "Cápsulas", 20, "mL", 150, 0, "Sí", false),
            new Medicine("XXYYZZ", "Paracetamol", "Dolor de cabeza", "Cápsulas", 20, "mL", 150, 0, "Sí", false),
            new Medicine("XXYYZZ", "Paracetamol", "Dolor de cabeza", "Cápsulas", 20, "mL", 150, 0, "Sí", false),
            new Medicine("XXYYZZ", "Paracetamol", "Dolor de cabeza", "Cápsulas", 20, "mL", 150, 0, "Sí", false),
            new Medicine("XXYYZZ", "Paracetamol", "Dolor de cabeza", "Cápsulas", 20, "mL", 150, 0, "Sí", false),
            new Medicine("XXYYZZ", "Paracetamol", "Dolor de cabeza", "Cápsulas", 20, "mL", 150, 0, "Sí", false),
            new Medicine("XXYYZZ", "Paracetamol", "Dolor de cabeza", "Cápsulas", 20, "mL", 150, 0, "Sí", false),
            new Medicine("XXYYZZ", "Paracetamol", "Dolor de cabeza", "Cápsulas", 20, "mL", 150, 0, "Sí", false),
            new Medicine("XXYYZZ", "Paracetamol", "Dolor de cabeza", "Cápsulas", 20, "mL", 150, 0, "Sí", false),
            new Medicine("XXYYZZ", "Paracetamol", "Dolor de cabeza", "Cápsulas", 20, "mL", 150, 0, "Sí", false),
        ];
    }

    public getMedicines(): Medicine[] {
        return this._medicines ?? [];
    }

    public postMedicine(medicine: ICreateMedicine): string {
        if(!this._medicines) this._medicines = [];
        const stock = 0; // Default.
        const isActive = false; // Default.
        const medicineToAdd = new Medicine(medicine.code, medicine.name, medicine.symtomps, medicine.presentation, medicine.quantity, medicine.unit, medicine.price, stock, medicine.prescription, isActive);
        this._medicines.push(medicineToAdd);
        return medicine.code;
    }

}
