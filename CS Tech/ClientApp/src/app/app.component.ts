import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';


  constructor(private router :Router){

  }

  ngOnInit(): void {
    var routedPath = window.location.href.substr(window.location.href.lastIndexOf('/') + 1);
    if (routedPath == "" || routedPath == "login") {
        var userData = localStorage.getItem('csTechToken');
        if (userData) {
          this.router.navigate(['/dashboard']);
        } else {
          this.router.navigate(['/login']);
        }
      
      
    } 
  }
  
}
