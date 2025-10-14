import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class ChicoService {
  private base = `${environment.apiUrl}/chicos`;
  constructor(private http: HttpClient) {
    console.log('API URL:', environment.apiUrl);
    console.log('Base URL:', this.base);
  }
  getAll(): Observable<any[]> { return this.http.get<any[]>(this.base); }
  getById(id: string): Observable<any> { return this.http.get<any>(`${this.base}/${id}`); }
  create(item: any): Observable<any> { return this.http.post<any>(this.base, item); }
  update(id: string, item: any): Observable<any> { return this.http.put<any>(`${this.base}/${id}`, item); }
  delete(id: string): Observable<any> { return this.http.delete<any>(`${this.base}/${id}`); }
}
