import { NgClass } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import {
  FormBuilder,
  Validators,
  ReactiveFormsModule,
  FormGroup,
  FormControl,
} from '@angular/forms';

@Component({
  selector: 'lib-auth-lib',
  standalone: true,
  imports: [NgClass, ReactiveFormsModule],
  templateUrl: './auth-lib.component.html',
  styles: ``,
})
export class AuthLibComponent {
  public currentFormTab: 'login' | 'register' = 'login';

  loginForm: FormGroup<LoginForm> = new FormGroup<LoginForm>({
    email: new FormControl(''),
    password: new FormControl(''),
  });

  get email() {
    return this.loginForm.get('email');
  }

  onLoginSubmit(): void {
    console.log('login form');
    if (this.loginForm?.valid) {
      alert(this.loginForm.value);
    }
  }

  onRegisterSubmit(): void {
    console.log('register');
  }
}

interface LoginForm {
  email: FormControl<string | null>;
  password?: FormControl<string | null>;
}
