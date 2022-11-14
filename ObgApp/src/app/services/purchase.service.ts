import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ICreatePurchase } from '../interfaces/create-purchase';
import { Purchase } from '../models/purchase';

@Injectable({
    providedIn: 'root'
})
export class PurchaseService {

    private _purchasessBehaviorSubject$: BehaviorSubject<Purchase[] | undefined>;

    constructor(
        private _http: HttpClient,
    ) {
        this._purchasessBehaviorSubject$ = new BehaviorSubject<Purchase[] | undefined>(undefined);
    }

    public get purchases$(): Observable<Purchase[] | undefined> {
        return this._purchasessBehaviorSubject$.asObservable();
    }

    public postPurchase(purchaseToAdd: ICreatePurchase, email: string): Observable<Purchase> {
        console.log(purchaseToAdd);
        console.log(email);
        let headers = new HttpHeaders();
        headers = headers.append('buyerEmail', email);
        return this._http.post<Purchase>(`${environment.API_HOST_URL}/purchase`, purchaseToAdd, { headers });
    }

}
