import { Component, OnInit } from '@angular/core';
import { FormGroup , FormControl, Validators , FormGroupDirective } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  
  constructor(private authService: AuthService, private router: Router){
    this.registerForm = new FormGroup({
      "Email": new FormControl("", [
        Validators.required,
        Validators.email
      ]),
      "Password": new FormControl("", [
        Validators.required,
        Validators.minLength(6),
        Validators.maxLength(30)
      ])
    });
  }
  
  ngOnInit() {

  }

  registration(form: FormGroup){
    this.authService
    .registration(
      form.controls["Email"].value,
      form.controls["Password"].value)
    .subscribe((result: string) => (console.log(result)));
    this.router.navigateByUrl('login')
  }
}
