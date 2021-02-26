import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { EmployeeService } from '../employee.service';
import notify from 'devextreme/ui/notify';

@Component({
  selector: 'app-employeeedit',
  templateUrl: './employeeedit.component.html',
  styleUrls: ['./employeeedit.component.css']
})
export class EmployeeeditComponent implements OnInit {

  key: number;
  editForm: FormGroup;
  submitAttempt: boolean = false;
  isEmailPatternValid: boolean = false;
  emailPattern: any = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
  isEmailIdValid: boolean = false;
  validateData: any;
  mobileErrormsg: string = "";
  designationDatasource: any = ["HR", "Manager", "Sales"];
  showLoader: boolean = false;

  constructor(private router: Router, private activatedRoute: ActivatedRoute, private service: EmployeeService, private _fb: FormBuilder) {
    this.buildForm();
  }

  ngOnInit() {

    this.editForm.controls['designation'].setValue("Sales");
    this.activatedRoute.params.subscribe(para => {
      this.key = para.id;
      if (para.id != 0) {
        this.service.getEmployee(this.key).subscribe(data => {
        });
      }

    });
  }

  buildForm() {
    this.editForm = this._fb.group({
      id: new FormControl(),
      name: new FormControl('', [Validators.required]),
      email: new FormControl('', [Validators.required]),
      mobile: new FormControl('', [Validators.required]),
      designation: new FormControl('HR', [Validators.required]),
      gender: new FormControl('male', [Validators.required]),
      mca: new FormControl(false),
      bca: new FormControl(false),
      bsc: new FormControl(false),
      //file: new FormControl('', [Validators.required]),
      // name : new FormControl('', [Validators.required]),
      // name : new FormControl('', [Validators.required]),
    });
  }

  saveEmployee() {
    this.submitAttempt = true;
  }

  emailIdValidation(e) {
    if (this.key == 0) {
      if (e.event.target.value != undefined && e.event.target.value != "") {
        this.verifyEmail();
      }
    }
  }

  createEmployee() {
    this.showLoader = true;
    this.submitAttempt = true;
    if (this.editForm.valid && (!this.isEmailPatternValid && !this.isEmailIdValid) && this.mobileErrormsg == "" && !(!this.editForm.controls['mca'].value && !this.editForm.controls['bca'].value && !this.editForm.controls['bsc'].value)) {

      this.validateData = {
        'emailId': (this.editForm.controls['email'] && this.editForm.controls['email'].value != undefined && this.editForm.controls['email'].value == "") ? "" : this.editForm.controls['email'].value,
        'employeeId': this.key != undefined  ? 0 : this.key
      };

      this.service.verifyEmail(this.validateData).subscribe(validateData => {
        if (validateData) {
          this.isEmailIdValid = validateData.isEmailIdExist;

          if (!validateData.isEmailIdExist) {
            if (this.editForm.valid) {
              debugger;
              this.editForm.controls['id'].setValue(this.key);
              this.service.addEmployee(this.editForm.value).subscribe(result => {
                if (result.httpStatus == 200) {

                  this.showToaster('success', result.message);
                  this.router.navigate(['/employees']);
                }
                else {
                  this.showToaster('error', result.message);
                }
              }, error => {
                this.showToaster("error", error.message);
              });
            } else {
              this.isEmailIdValid = validateData.isEmailIdExist;
              this.showLoader = false;
            }

          }
        }
      });
    } else {
      this.showLoader = false;
    }

  }


  verifyEmail() {
    this.validateData = {
      'emailId': (this.editForm.controls['email'] && this.editForm.controls['email'].value != undefined && this.editForm.controls['email'].value == "") ? "" : this.editForm.controls['email'].value,
      'employeeId': this.key != undefined && this.key == 0 ? "" : this.key
    };

    this.service.verifyEmail(this.validateData).subscribe(validateData => {
      if (validateData) {
        this.isEmailIdValid = validateData.isEmailIdExist == true ? validateData.isEmailIdExist : false;
      }
    });
  }

  isNumeric(value) {
    return value.match(/^\d+$/);
  }

  verifyMobileNo(event) {
    if (event.value != "") {
      var status = this.isNumeric(event.value);

      if (status) {
        if (event.value.length != 10) {
          this.mobileErrormsg = "Mobile No should be equal to 10 digits";
        } else {
          this.mobileErrormsg = "";
        }
      } else {
        this.mobileErrormsg = "Invalid Mobile No";
      }

      console.log("mo", event);
    } else {
      this.mobileErrormsg = "Mobile No is required";
    }
  }

  emailIdValueChange(e) {
    if (e.value && e.value != "") {
      if (e.value.toString().trim().match(this.emailPattern)) {
        this.isEmailPatternValid = false;
        this.isEmailIdValid = false;
      }
      else {
        this.isEmailPatternValid = true;
        this.isEmailIdValid = false;
      }
    }
    else {
      this.isEmailPatternValid = false;
      this.isEmailIdValid = false;
    }
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
