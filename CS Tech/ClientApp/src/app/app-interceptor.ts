import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core'
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable()
export class AppInterceptor implements HttpInterceptor {

  constructor(private router: Router) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    var cloned = request;
    var tempStore = JSON.parse(localStorage.getItem("csTechToken"));
    if (tempStore && tempStore.token) {
      cloned = request.clone({ headers: request.headers.set("Authorization", "Bearer " + tempStore.token) })
    }
    return Observable.create(observer => {
      const subscription = next.handle(cloned)
        .subscribe(event => {
          if (event instanceof HttpResponse) {
            observer.next(event);
            if (event.status == 401) {
              localStorage.clear();
              sessionStorage.clear();
              this.router.navigate(["/login"]);
            }
          }
        },
          error => {
            observer.error(error);
            if (error.status == 401) {
              localStorage.clear();
              sessionStorage.clear();
              this.router.navigate(["/login"]);
            }
          },
          () => {
            observer.complete();
          });
      return () => {

        subscription.unsubscribe();
      };
    });
  }

}
