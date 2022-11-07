import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { ICreateSession } from '../interfaces/create-session';
import { IDeleteResponse } from '../interfaces/delete-response.interface';
import { Session } from '../models/session';

@Injectable({
    providedIn: 'root'
})
export class SessionService {

    private _sessionsBehaviorSubject$: BehaviorSubject<Session[] | undefined>;

    constructor(
        private _http: HttpClient,
    ) {
        this._sessionsBehaviorSubject$ = new BehaviorSubject<Session[] | undefined>(undefined);
    }

    public get sessions$(): Observable<Session[] | undefined> {
        return this._sessionsBehaviorSubject$.asObservable();
    }

    public getSessions(): Observable<Session[]> {
        return this._http.get<Session[]>(`${environment.API_HOST_URL}/session`).pipe(
            tap((sessions: Session[]) => this._sessionsBehaviorSubject$.next(sessions)),
        );
    }

    public getSessionByToken(token: string): Observable<Session> {
        return this._http.get<Session>(`${environment.API_HOST_URL}/session/${token}`);
    }

    public postSession(sessionToAdd: ICreateSession): Observable<Session> {
        return this._http.post<Session>(`${environment.API_HOST_URL}/session`, sessionToAdd);
    }

    public putSession(sessionToUpdate: Session): Observable<Session> {
        return this._http.put<Session>(`${environment.API_HOST_URL}/session/${sessionToUpdate.IdSession}`, sessionToUpdate);
    }

    public deleteSession(IdSession: string): Observable<IDeleteResponse> {
        return this._http.delete<IDeleteResponse>(`${environment.API_HOST_URL}/session/${IdSession}`);
    }

}
