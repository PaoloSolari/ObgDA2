import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { ICreateDemand } from '../interfaces/create-demand';
import { IUpdateDemand } from '../interfaces/update-demand';
import { Demand } from '../models/demand';
import { getDemandFormUrl } from '../utils/routes';

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

    public postDemand(demandToAdd: ICreateDemand, token: string): Observable<Demand> {
        let headers = new HttpHeaders();
        headers = headers.append('token', token);
        return this._http.post<Demand>(`${environment.API_HOST_URL}/demand`, demandToAdd, { headers });
    }

    public putDemand(demandToUpdate: IUpdateDemand, token: string): Observable<Demand> {
        let headers = new HttpHeaders();
        headers = headers.append('token', token);
        console.log(getDemandFormUrl(demandToUpdate.IdDemand));
        let idDemand = getDemandFormUrl(demandToUpdate.IdDemand);
        console.log(`${environment.API_HOST_URL}/`+idDemand);
        return this._http.put<Demand>(`${environment.API_HOST_URL}/`+idDemand, demandToUpdate, { headers }); // (#)
    }

}
