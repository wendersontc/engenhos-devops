import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { WorkItem } from './model/work-item';
import { catchError, tap, map } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({'Content-Type': 'application/json'})
};

const apiUrl = 'http://localhost:9000/api/workitem';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      console.error(error);

      return of(result as T);
    };
  }

  getWorkItens (): Observable<WorkItem[]> {
    return this.http.get<WorkItem[]>(apiUrl)
      .pipe(
        tap(produtos => console.log('leu todos os itens')),
        catchError(this.handleError('getWorkItens', []))
      );
  }

}
