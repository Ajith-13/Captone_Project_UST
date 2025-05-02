import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from '../../../components/navbar/navbar.component';
import { AuthService } from '../../../Services/auth.service';

@Component({
  selector: 'app-administratorlogin',
  standalone: true,
  imports: [FormsModule,NavbarComponent],
  templateUrl: './administratorlogin.component.html',
  styleUrl: './administratorlogin.component.css'
})
export class AdministratorloginComponent {
  formData = {
    email: '',
    password: ''
  };
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
  
  errorMessage = '';
  constructor(private adminService: AuthService, private router: Router) {}
  onadminLogin() {
    // alert(this.formData);
    const loginPayload = {
      email: this.formData.email,
      password: this.formData.password
    };
    console.log(this.formData);
    this.adminService.login(loginPayload,'admin').subscribe({
    
      next: (response) => {
        console.log('Login successful', response);

        this.router.navigateByUrl('/admin-landingpage');
      },
      error: (err) => {
      
        const errorMessage = err?.error || 'An unexpected error occurred. Please try again later.';

        this.showErrorAlert('Failed!', errorMessage);
          console.error('Error details:', err?.error);
      },
    });
  }
}
