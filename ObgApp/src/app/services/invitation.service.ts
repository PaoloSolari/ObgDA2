import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ICreateInvitation } from '../interfaces/create-invitation';
import { Invitation } from '../models/invitation';
import { Pharmacy } from '../models/pharmacy';

@Injectable({
    providedIn: 'root'
})
export class InvitationService {

    private _invitationsBehaviorSubject$: BehaviorSubject<Invitation[] | undefined>;

    constructor(
        private _http: HttpClient,
    ) {
        this._invitationsBehaviorSubject$ = new BehaviorSubject<Invitation[] | undefined>(undefined);
    }

    public get invitations$(): Observable<Invitation[] | undefined> {
        return this._invitationsBehaviorSubject$.asObservable();
    }

    public getInvitations(): Observable<Invitation[]> {
        // const headers = new HttpHeaders();
        // return this._http.get<Invitation[]>(`${environment.API_HOST_URL}/invitation`, { headers }).pipe(
        //     tap((invitations: Invitation[]) => this._invitationsBehaviorSubject$.next(invitations)),
        // );
        return this._http.get<Invitation[]>(`${environment.API_HOST_URL}/invitation`).pipe(
            tap((invitations: Invitation[]) => this._invitationsBehaviorSubject$.next(invitations)),
        );
    }

    // public getInvitationById(invitationId: string): Observable<Invitation> {
    //     return this._http.get<Invitation>(`${environment.API_HOST_URL}/invitation/${invitationId}`);
    // }

    public getInvitationByName(name: string): Observable<Invitation> {
        return this._http.get<Invitation>(`${environment.API_HOST_URL}/invitation/${name}`);
    }

    public postInvitation(invitationToAdd: ICreateInvitation): Observable<Invitation> {
        let headers = new HttpHeaders();
        headers = headers.append('token', 'XXYYZZ');
        headers = headers.append('pharmacyName', invitationToAdd.Pharmacy!.name!);
        return this._http.post<Invitation>(`${environment.API_HOST_URL}/invitation`, invitationToAdd, { headers });
    }

    public putInvitation(invitationToUpdate: Invitation): Observable<Invitation> {
        return this._http.put<Invitation>(`${environment.API_HOST_URL}/invitation/${invitationToUpdate.idInvitation}`, invitationToUpdate);
    }

}
