import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { NavbarComponent } from '../../../components/navbar/navbar.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-usersignup',
  standalone: true,
  imports: [FormsModule,NavbarComponent,CommonModule],
  templateUrl: './usersignup.component.html',
  styleUrls: ['./usersignup.component.css']  // Corrected styleUrl to styleUrls
})
export class UsersignupComponent {
  private http = inject(HttpClient);
  private router = inject(Router);
  formData = {
    username: '',
    email: '',
    password: ''
  };

  formError: string | null = null;

  // Show custom alert with dynamic error message
  showErrorAlert(title: string, message: string) {
    const alertBox = document.getElementById('alertMessage') as HTMLElement;
    const alertText = document.getElementById('alertText') as HTMLElement;
    const alertTitle = document.getElementById('alertTitle') as HTMLElement;
  
    alertTitle.innerText = title;
    alertText.innerText = message;
  
    // Show the alert
    alertBox.style.display = 'block';
  
    // Hide the alert after 5 seconds
    setTimeout(() => {
      alertBox.style.display = 'none';
    }, 5000);
  }
  
  showSuccessAlert(title: string, message: string) {
    const alertBox = document.getElementById('alertSuccessMessage') as HTMLElement;
    const alertTitle = document.getElementById('alertSuccessTitle') as HTMLElement;
    const alertText = document.getElementById('alertSuccessText') as HTMLElement;

    alertTitle.innerText = title;
    alertText.innerText = message;

    // Show the alert
    alertBox.style.display = 'block';

    // Hide the alert after 3 seconds
    setTimeout(() => {
      alertBox.style.display = 'none';
    }, 3000);
  }
  onuserRegister() {
    if (!this.formData.username || !this.formData.email || !this.formData.password) {
      this.formError = 'All fields are required!';
      return;
    }
    const registerPayload = new FormData();
    registerPayload.append('UserName', this.formData.username);
    registerPayload.append('Email', this.formData.email);
    registerPayload.append('Password', this.formData.password);
  
    this.http.post('http://localhost:7266/api/Auth/register/learner', registerPayload, { responseType: 'text' })
      .subscribe({
        next: (response) => {
          console.log(response);
          this.showSuccessAlert('Registration successful!', 'You are registered successfully! Please log in.');
          setTimeout(() => {
            this.router.navigateByUrl('/login');
          }, 3000);
        },
        error: (err) => {
          console.error('Registration failed', err);
          const errorMessage = err?.error || 'An unexpected error occurred. Please try again later.';
          this.showErrorAlert('Failed!', errorMessage);
          console.error('Error details:', err?.error);
        }
      });
  }
  
}
