import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import notify from 'devextreme/ui/notify';
import { EmployeeService } from '../employee.service';

@Component({
  selector: 'app-employeesummary',
  templateUrl: './employeesummary.component.html',
  styleUrls: ['./employeesummary.component.css']
})
export class EmployeesummaryComponent implements OnInit {

  employeeDatasource : any = [];
  showLoader : boolean = false;
  paging : boolean = true;
  showDeletePopup:boolean = false;
  deleteKey : number = 0;

  constructor(private router : Router, private service : EmployeeService) { }

  ngOnInit() {
    this.showLoader = true;
    this.getSummary();
  }

  getSummary(){
    this.service.getEmployees().subscribe(data => {
      if(data.httpStatus == 200){
        this.employeeDatasource = data.result;
      }else{  
        this.showToaster(data.message, "error");
      }
      this.showLoader = false;
    });
  }

  gotoCreateEmployee(){
    this.router.navigate(['employee/0']);
  }

  editClick(item) {
    this.router.navigate(["employee/" + item.id]);
  }

  deleteClick(item){
    this.showDeletePopup = true;
    this.deleteKey = item.id;
  }

  deleteEmployee(){
    //this.router.navigate(["employee/" + item.id]);
    this.service.deleteEmployee(this.deleteKey).subscribe(data => {
      if(data.httpStatus == 200){
        this.showDeletePopup = false;
        this.getSummary();
      }else{  
        this.showToaster(data.message, "error");
        this.showLoader = false;
      }
      
    }, error => {
      console.log(error);
      this.showLoader = false;
    });
    
    
  }

  closeDeletePopup(){
    this.showDeletePopup = false;
  }

  showToaster(message: string, status: string) {
    notify({
      message: message,
      height: 50,
      width: 300,
      position: {
        my: "top right",
        at: "top right",
        offset: "-20 20"
      }
    }, status, 3000);
  }

}
