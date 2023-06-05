import { Evento } from '../models/Evento';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
// import { take, map } from 'rxjs/operators';
//import { environment } from '@environments/environment';

import { environment } from '@environments/environment';
// import { PaginatedResult } from '@app/models/Pagination';

@Injectable(
// { providedIn: 'root'}
)
export class EventoService {

  baseURL = environment.apiURL + 'api/eventos';

  constructor(private http: HttpClient) { }

  public getEventos(): Observable<Evento[]> {
    return this.http.get<Evento[]>(this.baseURL);
  }

  public getEventosByTema(tema: string): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.baseURL}/${tema}/tema`);
  }
  public getEventoById(id: number): Observable<Evento> {
    return this.http.get<Evento>(`${this.baseURL}/${id}`);
  }

  public post(evento: Evento): Observable<Evento> {
    return this.http
      .post<Evento>(this.baseURL, evento);
  }

  public put(evento: Evento): Observable<Evento> {
    return this.http
      .put<Evento>(`${this.baseURL}/${evento.id}`, evento);
  }

  public delete(id: number): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`);
  }
}
