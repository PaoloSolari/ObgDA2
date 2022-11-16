import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IExporter } from '../interfaces/exporter';

@Injectable({
    providedIn: 'root'
})
export class ExporterService {

    private _exportersBehaviorSubject$: BehaviorSubject<string[] | undefined>;

    constructor(
        private _http: HttpClient,
    ) {
        this._exportersBehaviorSubject$ = new BehaviorSubject<string[] | undefined>(undefined);
    }

    public get exporters$(): Observable<string[] | undefined> {
        return this._exportersBehaviorSubject$.asObservable();
    }

    public getExporters(token: string): Observable<string[]> {
        let headers = new HttpHeaders();
        headers = headers.append('token', token);
        return this._http.get<string[]>(`${environment.API_HOST_URL}/exporter`, {headers}).pipe(
            tap((exporters: string[]) => this._exportersBehaviorSubject$.next(exporters)),
        );
    }

    public getParameters(token: string, typeOfExporter: string): Observable<string[]> {
        let headers = new HttpHeaders();
        headers = headers.append('token', token);
        headers = headers.append('typeOfExporter', typeOfExporter);
        return this._http.get<string[]>(`${environment.API_HOST_URL}/exporter/parameters`, { headers }).pipe(
            tap((exporters: string[]) => this._exportersBehaviorSubject$.next(exporters)),
        );
    }

    public postExporter(token: string, typeOfExporter: string, parametersMap: Map<string, string>) {
        let headers = new HttpHeaders();
        headers = headers.append('token', token);
        headers = headers.append('typeOfExporter', typeOfExporter);
        return this._http.post<string>(`${environment.API_HOST_URL}/exporter`, parametersMap, { headers });
    }   

}
