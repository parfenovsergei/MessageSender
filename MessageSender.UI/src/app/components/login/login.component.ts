import { Component, OnInit } from '@angular/core';
import { FormGroup , FormControl, Validators , FormGroupDirective } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginResponse } from 'src/app/response/loginResponse';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(private authService: AuthService, private router: Router){
    this.loginForm = new FormGroup({
      "Email": new FormControl("", [
        Validators.required,
        Validators.email
      ]),
      "Password": new FormControl("", [
        Validators.required
      ])
    });
  }

  get email(){
    return this.loginForm.controls["Email"].value;
  }

  get password(){
    return this.loginForm.controls["Password"].value;
  }

  login(){
    this.authService
      .login(
        this.email,
        this.password)
      .subscribe((response: LoginResponse) => 
      {
        if(response.token != null){
          localStorage.setItem("Token", response.token);
          this.authService.decodeToken(response.token);
          this.authService.showMessage(response.message, "OK");
          this.router.navigate(['message']);
        }
        else
          this.authService.showMessage(response.message, "OK");
          this.loginForm = new FormGroup(null);
      });
  }
}
