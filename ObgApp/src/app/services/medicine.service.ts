import { compileDeclareClassMetadata } from '@angular/compiler';
import { Injectable } from '@angular/core';
import { ICreateMedicine } from '../interfaces/create-medicine';
import { Medicine, PresentationMedicine } from '../models/medicine';

import { HttpClient, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, map, tap, catchError } from 'rxjs';
import { environment } from '../../environments/environment';
import { IDeleteResponse } from '../interfaces/delete-response.interface';
import { query } from '@angular/animations';

@Injectable({
    providedIn: 'root'
})

export class MedicineService {

    private _medicinesBehaviorSubject$: BehaviorSubject<Medicine[] | undefined>;

    constructor(
        private _http: HttpClient,
    ) {
        this._medicinesBehaviorSubject$ = new BehaviorSubject<Medicine[] | undefined>(undefined);
    }

    public get medicines$(): Observable<Medicine[] | undefined> {
        return this._medicinesBehaviorSubject$.asObservable();
    }

    public getMedicines(employeeName: string): Observable<Medicine[]> {
        let headers = new HttpHeaders();
        let params = new HttpParams();
        params = params.append('employeeName', employeeName);
        return this._http.get<Medicine[]>(`${environment.API_HOST_URL}/medicine`, { params }).pipe(
            tap((medicines: Medicine[]) => this._medicinesBehaviorSubject$.next(medicines)),
        );
    }

    public getMedicinesFilter(pharmacyName: string): Observable<Medicine[]> {
        let headers = new HttpHeaders();
        let params = new HttpParams();
        params = params.append('pharmacyName', pharmacyName);
        console.log(pharmacyName);
        return this._http.get<Medicine[]>(`${environment.API_HOST_URL}/medicine/stock`, { params }).pipe(
            tap((medicines: Medicine[]) => this._medicinesBehaviorSubject$.next(medicines)),
        );
    }

    public getMedicineByCode(medicineCode: string): Observable<Medicine> {
        return this._http.get<Medicine>(`${environment.API_HOST_URL}/medicine/${medicineCode}`);
    }

    public postMedicine(medicineToAdd: ICreateMedicine, token: string): Observable<Medicine> {
        let headers = new HttpHeaders();
        headers = headers.append('token', token);
        return this._http.post<Medicine>(`${environment.API_HOST_URL}/medicine`, medicineToAdd, { headers });
    }

    public putMedicine(medicineToUpdate: Medicine): Observable<Medicine> {
        return this._http.put<Medicine>(`${environment.API_HOST_URL}/medicine/${medicineToUpdate.code}`, medicineToUpdate);
    }

    public deleteMedicine(medicineCode: string | null, token: string): Observable<IDeleteResponse> {
        let headers = new HttpHeaders();
        headers = headers.append('token', token);
        return this._http.delete<IDeleteResponse>(`${environment.API_HOST_URL}/medicine/${medicineCode}`, { headers });
    }

}
