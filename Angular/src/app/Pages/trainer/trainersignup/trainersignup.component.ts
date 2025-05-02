import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { NavbarComponent } from '../../../components/navbar/navbar.component';
import { TrainersignupService } from '../../../Services/trainer/trainersignup.service';

@Component({
  selector: 'app-trainersignup',
  standalone: true,
  imports: [FormsModule,NavbarComponent],
  templateUrl: './trainersignup.component.html',
  styleUrl: './trainersignup.component.css'
})
export class TrainersignupComponent {
  formData = {
    userName: '',
    email: '',
    password: '',
    certificate: null as File | null,
    resume: null as File | null
  };
 
  constructor(private trainerSignupService: TrainersignupService, private router: Router) {}

  onFileChange(event: any, fileType: 'certificate' | 'resume') {
    if (event.target.files && event.target.files[0]) {
      this.formData[fileType] = event.target.files[0];  // Set the file in the formData object
    }
  }
  
  onTrainerRegistration() {
    // Validate that all fields are filled
    if (!this.formData.userName || !this.formData.email || !this.formData.password || !this.formData.certificate || !this.formData.resume) {
      console.log(this.formData);
      alert("Please fill all fields and upload both certificate and resume.");
      return;
    }
  
    // Call the service to register the trainer
    this.trainerSignupService.registerTrainer(this.formData).subscribe({
      next: (response) => {
        console.log('Trainer registration successful', response);
        this.showSuccessAlert('Registration successful!', 'You are registered successfully! Please log in.');
        setTimeout(() => {
          this.router.navigateByUrl('/trainer-signin');
        }, 3000);  // Navigate to login page after successful registration
      },
      error: (err) => {
        console.error('Trainer registration failed', err);
  
        // Check if the error contains specific messages like DuplicateUserName or DuplicateEmail
        if (err.error && err.error.errors) {
          err.error.errors.forEach((e: { description: string }) => {
            this.showAlert("Failed", e.description);
          });
        } else {
          this.showAlert("Failed", 'An unexpected error occurred. Please try again.');
        }        
      }
    });
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
  showAlert(title: string, message: string) {
    // Set the alert title and message
    document.getElementById('alertTitle')!.innerText = title;
    document.getElementById('alertText')!.innerHTML = message; // Use innerHTML to allow line breaks
  
    // Show the alert
    const alertElement = document.getElementById('alertMessage')!;
    alertElement.style.display = 'block';
  
    // Optionally, hide the alert after a few seconds
    setTimeout(() => {
      alertElement.style.display = 'none';
    }, 5000); // Hide after 5 seconds
  }
  
}

