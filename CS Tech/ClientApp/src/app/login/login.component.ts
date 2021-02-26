import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginService } from './login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  isSubmitted: boolean = false;
  errorMsg: string = "";
  showLoader: boolean = false;
  
  constructor(private service: LoginService, private router: Router, private _fb: FormBuilder) {
    this.buildForm();
   }

  ngOnInit() {
  }

  buildForm() {
    this.loginForm = this._fb.group({
      email: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required)
    });
  }

  Login() {
    this.showLoader = true;
    this.isSubmitted = true;
    if (this.loginForm.valid) {
      this.service.authenticateUser(this.loginForm.value).subscribe(data => {
        console.log("result", data);
        if (data.result.isvalid) {
          localStorage.setItem("csTechToken", JSON.stringify(data.result));
            this.router.navigate(["/dashboard"]);
        } else {
          this.errorMsg = "Invalid Email / Password";
          this.showLoader = false;
        }
      });
    } else{
      this.showLoader = false;
    }
    
  }

  

}
