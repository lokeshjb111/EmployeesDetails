import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpClient) { }

  getEmployee(employeeId: number): Observable<any> {
    return this.http.get("employee/getEmployee" + employeeId);
  }

  verifyEmail(data): Observable<any> {
    return this.http.post("employee/verifyEmail", data);
  }

  addEmployee(data): Observable<any> {
    return this.http.post("employee/addEmployee", data);
  }

}
