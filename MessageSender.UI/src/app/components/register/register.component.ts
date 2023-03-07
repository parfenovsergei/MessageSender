import { Component, OnInit } from '@angular/core';
import { FormGroup , FormControl, Validators , FormGroupDirective } from '@angular/forms';
import { Router } from '@angular/router';
import { catchError } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { RegisterResponse } from 'src/app/models/registerResponse'

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})

export class RegisterComponent implements OnInit {
  registerForm: FormGroup;

  constructor(
    private authService: AuthService,
    private router: Router
    ){
      this.registerForm = new FormGroup({
        "Email": new FormControl("", [
          Validators.required,
          Validators.email
        ]),
        "Password": new FormControl("", [
          Validators.required,
          Validators.minLength(6)
        ]),
        "ConfirmPassword": new FormControl("", [
          Validators.required,
          Validators.minLength(6)
        ])
      })
  }
  
  ngOnInit() {

  }

  get email(){
    return this.registerForm.controls["Email"].value;
  }

  get password(){
    return this.registerForm.controls["Password"].value;
  }

  get confirmPassword(){
    return this.registerForm.controls["ConfirmPassword"].value;
  }

  registration(){
    this.authService
      .registration(
        this.email,
        this.password,
        this.confirmPassword)
      .subscribe((response: RegisterResponse) => {
        if(response.isRegister){
          this.authService.showMessage(response.message, "OK");
          this.router.navigate(['login']);
        }
        else{
          this.authService.showMessage(response.message, "OK");
        }
      })
  }
}
