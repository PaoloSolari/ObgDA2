import { Injectable } from '@angular/core';
import { ICreatePharmacy } from '../interfaces/create-pharmacy';
import { Pharmacy } from '../models/pharmacy';

@Injectable({
    providedIn: 'root'
})
export class PharmacyService {

    private _pharmacies: Pharmacy[] | undefined;

    constructor(
        // De momento nada...
    ) { }

        public postPharmacy(pharmacy: ICreatePharmacy): string {
            if(!this._pharmacies) this._pharmacies = [];
            const pharmacyToAdd = new Pharmacy(pharmacy.name, pharmacy.address);
            this._pharmacies.push(pharmacyToAdd);
            return pharmacy.name;
        }

}
