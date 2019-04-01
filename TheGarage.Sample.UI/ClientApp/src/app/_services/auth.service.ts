import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';

import { Token } from '../_models';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private currentTokenSubject: BehaviorSubject<Token>;
  public currentToken: Observable<Token>;

  constructor(private http: HttpClient) {
    this.currentTokenSubject = new BehaviorSubject<Token>(JSON.parse(localStorage.getItem('currentToken')));
    this.currentToken = this.currentTokenSubject.asObservable();
  }

  public get currentTokenValue(): Token {
    return this.currentTokenSubject.value;
  }

  login(username: string, password: string) {
    return this.http.post<any>(`${environment.apiUrl}/account/token`, { username, password })
      .pipe(map(token => {
        // login successful if there's a jwt token in the response
        if (token && token.token) {
          // store token details and jwt token in local storage to keep user logged in between page refreshes
          localStorage.setItem('currentToken', JSON.stringify(token));
          this.currentTokenSubject.next(token);
        }
        return token;
      }));
  }

  logout() {
    // remove token from local storage to log user out
    localStorage.removeItem('currentToken');
    this.currentTokenSubject.next(null);
  }
}
