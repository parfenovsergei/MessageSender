import { Component, OnInit } from '@angular/core';
import { FormGroup , FormControl, Validators , FormGroupDirective } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(private authService: AuthService){
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

  get email()
  {
    return this.loginForm.controls["Email"].value;
  }

  get password()
  {
    return this.loginForm.controls["Password"].value;
  }
  login(){
    this.authService
      .login(
        this.email,
        this.password)
      .subscribe((token: string) => 
        {
          localStorage.setItem("authToken", token);
          this.authService.showMessage("You have successfully logged in", "OK");
        })
  }
}
