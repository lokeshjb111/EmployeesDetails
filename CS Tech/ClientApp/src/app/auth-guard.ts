import { Injectable } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';


@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private router: Router, private activeRuote : ActivatedRoute) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
      var userData = localStorage.getItem('csTechToken');
      if (userData) {
        var parsedData = JSON.parse(userData);
        if (parsedData && parsedData.token != null && parsedData.token != undefined && parsedData.token != "") {
          var current = new Date();
          var expire = new Date(parsedData.expire);
          if (current.getTime() > expire.getTime()) {
            localStorage.removeItem('csTechToken');
            this.router.navigate(['/login']);
          }
        } else {
          localStorage.removeItem('csTechToken');
          this.router.navigate(['/login']);
        }
        return true;
      }
    
    this.router.navigate(['/login']);

  }
}
