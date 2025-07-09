import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, from } from 'rxjs';
import { AuthService } from './auth.service';
import { switchMap } from 'rxjs/operators';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private auth: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return from(this.auth.getAccessToken()).pipe(
      switchMap(token => {
        if (token) {
          const headers = req.headers.set('Authorization', `Bearer ${token}`);
          req = req.clone({ headers });
        }
        return next.handle(req);
      })
    );
  }
}
