import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employeesummary',
  templateUrl: './employeesummary.component.html',
  styleUrls: ['./employeesummary.component.css']
})
export class EmployeesummaryComponent implements OnInit {

  constructor(private router : Router) { }

  ngOnInit() {
  }

  gotoCreateEmployee(){
    this.router.navigate(['employee/0']);
  }

}
