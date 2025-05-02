import { Component } from '@angular/core';
import { NavbarComponent } from '../../../components/navbar/navbar.component';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-addcourse',
  standalone: true,
  imports: [NavbarComponent,ReactiveFormsModule,CommonModule],
  templateUrl: './addcourse.component.html',
  styleUrl: './addcourse.component.css'
})
export class AddcourseComponent {
  courseForm: FormGroup; // Declare the formGroup
  thumbnailImageFile: File | null = null;
  message: string = '';

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router
  ) {
    // Initialize the formGroup here to avoid the error
    this.courseForm = this.fb.group({
      title: ['', Validators.required], // Title is required
      description: [''], // Description is optional
    });
  }

  ngOnInit(): void {
    // You can add any additional initialization logic here if needed
  }

  // Event handler for file input change
  onFileChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input?.files?.length) {
      this.thumbnailImageFile = input.files[0];
    } else {
      this.thumbnailImageFile = null;
    }
  }

  // Handle form submission
  onSubmit(): void {
    if (!this.thumbnailImageFile) {
      this.message = 'Thumbnail image is required.';
      return;
    }

    this.courseForm.markAllAsTouched();

    if (this.courseForm.invalid) {
      this.message = 'Please fill all required fields';
      return;
    }

    const formData = new FormData();
    formData.append('Title', this.courseForm.get('title')?.value);
    formData.append('Description', this.courseForm.get('description')?.value);
    formData.append('ThumbnailImage', this.thumbnailImageFile);

    const token = localStorage.getItem('jwt_token');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`
    });

    this.http.post('http://localhost:5276/api/Course', formData, { headers })
      .subscribe({
        next: (response: any) => {
          this.showSuccessAlert('Success!', 'Course Added Successfully');
          setTimeout(() => {
          }, 3000); 
        },
        error: (err) => {
          console.error('Error adding course:', err);
          this.message = err?.error?.message || 'An error occurred while adding the course.';
          this.showAlert("Failed", err.message)
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
