import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class ChoferService {
  private base = `${environment.apiUrl}/choferes`;
  
  constructor(private http: HttpClient) {}

  private handleError(error: HttpErrorResponse) {
    console.error('Ocurrió un error:', error);
    return throwError(() => 'Hubo un error al procesar la solicitud. Por favor, verifique el backend.');
  }

  getAll(): Observable<any[]> { 
    return this.http.get<any[]>(this.base).pipe(
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
