import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LogoutComponent } from './logout/logout.component';
import { EmployeesummaryComponent } from './employee/employeesummary/employeesummary.component';
import { EmployeeeditComponent } from './employee/employeeedit/employeeedit.component';
import { EmployeeService } from './employee/employee.service';
import { AuthComponent } from './layout/auth/auth.component';
import { ModuleComponent } from './layout/module/module.component';
import { DxNumberBoxModule, DxSelectBoxModule, DxTextBoxModule } from 'devextreme-angular';
import { LoginService } from './login/login.service';
import { AuthGuard } from './auth-guard';
import { AppInterceptor } from './app-interceptor';



@NgModule({
  declarations: [
    AppComponent, LoginComponent, DashboardComponent, EmployeesummaryComponent, EmployeeeditComponent,
    AuthComponent, ModuleComponent, LogoutComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    DxTextBoxModule,
    DxNumberBoxModule,
    DxSelectBoxModule,
    RouterModule.forRoot([
      { path: '', redirectTo: '/login', pathMatch: 'full' },
      {
        path: '',
        component: AuthComponent,
        children: [
          {
            path: 'logout',
            component: LogoutComponent
          },
          { path: 'login', component: LoginComponent },
        ]
      },
      {
        path: '',
        component: ModuleComponent,
        children: [
          { path: 'dashboard', canActivate: [AuthGuard], component: DashboardComponent },
          { path: 'employees', canActivate: [AuthGuard], component: EmployeesummaryComponent },
          { path: 'employee/:id', canActivate: [AuthGuard], component: EmployeeeditComponent },
          { path: '**', redirectTo: '/login', pathMatch: 'full' }
        ]
      }


    ])
  ],
  providers: [EmployeeService, LoginService, AuthGuard,  { provide: HTTP_INTERCEPTORS, useClass: AppInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
