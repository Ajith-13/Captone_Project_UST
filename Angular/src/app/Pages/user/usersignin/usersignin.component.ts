import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../Services/auth.service';
import { NavbarComponent } from '../../../components/navbar/navbar.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-usersignin',
  standalone: true,
  imports: [FormsModule,NavbarComponent,CommonModule],
  templateUrl: './usersignin.component.html',
  styleUrl: './usersignin.component.css'
})

export class UsersigninComponent {
  formData = {
    email: '',
    password: ''
  };
  errorMessage = '';

  constructor(private userLoginService: AuthService, private router: Router) {}
  onuserLogin() {
    const loginPayload = {
      email: this.formData.email,
      password: this.formData.password
    };
    console.log(this.formData);
    this.userLoginService.login(loginPayload,"user").subscribe({
    
      next: (response) => {
        console.log('Login successful', response);

        alert("Login successfull");
        this.router.navigateByUrl('/user-landingpage');

      },
      error: (error) => {
        console.error('Login failed', error);
        this.errorMessage = 'Invalid username/email or password.';
      },
    });
  }
}
