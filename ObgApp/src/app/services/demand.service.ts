import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ICreateDemand } from '../interfaces/create-demand';
import { Demand } from '../models/demand';

@Injectable({
    providedIn: 'root'
})
export class DemandService {

    private _demandsBehaviorSubject$: BehaviorSubject<Demand[] | undefined>;

    constructor(
        private _http: HttpClient,
    ) { 
        this._demandsBehaviorSubject$ = new BehaviorSubject<Demand[] | undefined>(undefined);
    }

    public get demands$(): Observable<Demand[] | undefined> {
        return this._demandsBehaviorSubject$.asObservable();
    }

    public getDemands(token: string): Observable<Demand[]> {
        let headers = new HttpHeaders();
        headers = headers.append('token', token);
        return this._http.get<Demand[]>(`${environment.API_HOST_URL}/demand`, { headers })
        .pipe(
            tap((demands: Demand[]) => this._demandsBehaviorSubject$.next(demands)),
        );
    }

    public postDemand(demandToAdd: ICreateDemand): Observable<Demand> {
        console.log(demandToAdd);
        let headers = new HttpHeaders();
        headers = headers.append('token', 'AABBCC'); // Sería el del empleado 'Paolo'.
        return this._http.post<Demand>(`${environment.API_HOST_URL}/demand`, demandToAdd, { headers });
    }

}
