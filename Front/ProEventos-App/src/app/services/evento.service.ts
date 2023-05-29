// import { Evento } from './../models/Evento';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
// import { Observable } from 'rxjs';
// import { take, map } from 'rxjs/operators';
//import { environment } from '@environments/environment';
import { Observable } from 'rxjs';
import { Evento } from '../models/Evento';
// import { PaginatedResult } from '@app/models/Pagination';

@Injectable(
// { providedIn: 'root'}
)
export class EventoService {

  baseURL = 'https://localhost:5001/api/eventos';

  constructor(private http: HttpClient) { }

  public getEventos(): Observable<Evento[]> {
    console.log("aqui");
    return this.http.get<Evento[]>(this.baseURL);
  }

  public getEventosByTema(tema: string): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.baseURL}/${tema}/tema`);
  }
  public getEventoById(id: number): Observable<Evento> {
    return this.http.get<Evento>(`${this.baseURL}/${id}`);
  }
}
