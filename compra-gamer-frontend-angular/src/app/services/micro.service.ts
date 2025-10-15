import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable, throwError } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class MicroService {
  private base = `${environment.apiUrl}/microescolares`;

  constructor(private http: HttpClient) {
    console.log('MicroService base URL:', this.base);
  }

  private handleError(error: HttpErrorResponse) {
    console.error('Ocurrió un error:', error);
    return throwError(() => 'Hubo un error al procesar la solicitud. Por favor, verifique el backend.');
  }
  getAll(): Observable<any[]> { 
    return this.http.get<any[]>(this.base).pipe(
      tap(response => console.log('API Response:', response)),
      catchError(this.handleError)
    ); 
  }

  getById(id: string): Observable<any> { 
    return this.http.get<any>(`${this.base}/${id}`).pipe(
      catchError(this.handleError)
    ); 
  }

  create(item: any): Observable<any> { 
    return this.http.post<any>(this.base, item).pipe(
      catchError(this.handleError)
    ); 
  }

  update(id: string, item: any): Observable<any> { 
    return this.http.put<any>(`${this.base}/${id}`, item).pipe(
      catchError(this.handleError)
    ); 
  }

  delete(id: string): Observable<any> { 
    return this.http.delete<any>(`${this.base}/${id}`).pipe(
      catchError(this.handleError)
    ); 
  }
}
