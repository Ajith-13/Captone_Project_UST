import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../Services/auth.service';
import { NavbarComponent } from '../../../components/navbar/navbar.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-trainersignin',
  standalone: true,
  imports: [FormsModule, NavbarComponent,CommonModule],
  templateUrl: './trainersignin.component.html',
  styleUrl: './trainersignin.component.css'
})
export class TrainersigninComponent {
  formData = {
    email: '',
    password: ''
  };
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router) {}

  onTrainerLogin() {
    // console.log('Calling login method');
    // alert("logggg");
    this.authService.login(this.formData, 'trainer').subscribe({
      next: (response) => {
        // console.log('Login successful', response);
        const role = this.authService.getUserRole();
        // alert(role);
        console.log("role"+role);
        if (role === 'TRAINER') {
          this.router.navigate(['/trainerlandingpage']);
        } else {
          this.showAlert('Unauthorized', 'You are not allowed to access this page.');
          this.authService.removeToken();
        }
      },
      error: (err) => {
        this.showAlert('Login Failed', err.error?.message || 'An unexpected error occurred.');
      }
    });
  }

  trainerRegister() {
    this.router.navigateByUrl('trainer-signup');
  }

  showAlert(title: string, message: string) {
    const titleElement = document.getElementById('alertTitle');
    const messageElement = document.getElementById('alertText');
    const alertBox = document.getElementById('alertMessage');

    if (titleElement && messageElement && alertBox) {
      titleElement.innerText = title;
      messageElement.innerHTML = message;
      alertBox.style.display = 'block';
      setTimeout(() => alertBox.style.display = 'none', 5000);
    }
  }
  trainerPage(){
    this.router.navigateByUrl('Trainermainpage')
  }
}
