import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private http: HttpClient) { }

  getEmployee(employeeId: number): Observable<any> {
    return this.http.get("employeeCon/getEmployee/" + employeeId);
  }

  deleteEmployee(employeeId: number): Observable<any> {
    return this.http.get("employeeCon/deleteEmployee/" + employeeId);
  }

  getEmployees(): Observable<any> {
    return this.http.get("employeeCon/getEmployees");
  }

  verifyEmail(data): Observable<any> {
    return this.http.post("employeeCon/verifyEmail", data);
  }

  addEmployee(data): Observable<any> {
    return this.http.post("employeeCon/addEmployee", data);
  }

  updateEmployee(data): Observable<any> {
    return this.http.post("employeeCon/updateEmployee", data);
  }

  uploadFiles(data, key): Observable<any> {
    var body = new FormData();
    body.append("files", data, data.name);
    return this.http.post("employeeCon/UploadDoc/" + key, body, { responseType: 'text' });
  }

}
