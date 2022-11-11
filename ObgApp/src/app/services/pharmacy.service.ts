import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { ICreatePharmacy } from '../interfaces/create-pharmacy';
import { IDeleteResponse } from '../interfaces/delete-response.interface';
import { Demand } from '../models/demand';
import { Medicine } from '../models/medicine';
import { Pharmacy } from '../models/pharmacy';
import { Purchase } from '../models/purchase';

@Injectable({
    providedIn: 'root'
})
export class PharmacyService {

    private _pharmacies: Pharmacy[] | undefined;
    private _pharmaciesBehaviorSubject$: BehaviorSubject<Pharmacy[] | undefined>;
    private _pharmacyBehaviorSubject$: BehaviorSubject<Pharmacy | undefined>;

    constructor(
        private _http: HttpClient,
    ) { 
        this._pharmaciesBehaviorSubject$ = new BehaviorSubject<Pharmacy[] | undefined>(undefined);
        this._pharmacyBehaviorSubject$ = new BehaviorSubject<Pharmacy | undefined>(undefined);
    }

    public get pharmacies$(): Observable<Pharmacy[] | undefined> {
        return this._pharmaciesBehaviorSubject$.asObservable();
    }

    public get pharmacy$(): Observable<Pharmacy | undefined> {
        return this._pharmacyBehaviorSubject$.asObservable();
    }

    public getPharmacies(): Observable<Pharmacy[]> {
        const headers = new HttpHeaders();
        return this._http.get<Pharmacy[]>(`${environment.API_HOST_URL}/pharmacy`, { headers }).pipe(
            tap((pharmacies: Pharmacy[]) => this._pharmaciesBehaviorSubject$.next(pharmacies)),
        );
    }

    public getPharmacyByName(name: string): Observable<Pharmacy> {
        return this._http.get<Pharmacy>(`${environment.API_HOST_URL}/pharmacy/${name}`);
    }

    public postPharmacy(pharmacyToAdd: ICreatePharmacy): Observable<Pharmacy> {
        let headers = new HttpHeaders();
        headers = headers.append('token', 'XXYYZZ');
        return this._http.post<Pharmacy>(`${environment.API_HOST_URL}/pharmacy`, pharmacyToAdd, { headers });
    }

    public putPharmacy(pharmacyToUpdate: Pharmacy): Observable<Pharmacy> {
        return this._http.put<Pharmacy>(`${environment.API_HOST_URL}/pharmacy/${pharmacyToUpdate.name}`, pharmacyToUpdate);
    }

    public deletePharmacy(pharmacyName: string | null): Observable<IDeleteResponse> {
        return this._http.delete<IDeleteResponse>(`${environment.API_HOST_URL}/pharmacy/${pharmacyName}`);
    }

}
