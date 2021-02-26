import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-module',
  templateUrl: './module.component.html',
  styleUrls: ['./module.component.css']
})
export class ModuleComponent implements OnInit {

  
  userName : string = "";
  
  constructor(private router :Router) { }

  ngOnInit() {

    var userData = localStorage.getItem('csTechToken');
        if (userData) {
          this.userName = userData['name'];
        } else {
          this.router.navigate(['/login']);
        }
  }

}
