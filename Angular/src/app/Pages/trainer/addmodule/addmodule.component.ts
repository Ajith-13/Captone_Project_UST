import { Component, OnInit } from '@angular/core';
import { NavbarComponent } from '../../../components/navbar/navbar.component';
import { ViewCourseService } from '../../../Services/trainer/view-course.service';
import { AuthService } from '../../../Services/auth.service';
import { HttpClient, HttpEvent, HttpEventType, HttpHeaders } from '@angular/common/http';
import { catchError, map } from 'rxjs';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-addmodule',
  standalone: true,
  imports: [NavbarComponent,FormsModule,CommonModule],
  templateUrl: './addmodule.component.html',
  styleUrl: './addmodule.component.css'
})
export class AddmoduleComponent implements OnInit{
  courses: any[] = [];
  selectedCourseId: number | null = null;
  title: string = '';
  description: string = '';
  file: File | null = null;
  uploadProgress: number = 0;
  errorMessage: string = '';
  successMessage: string='';

  // Additional properties for the alert logic
  showErrorAlert: boolean = false;
  showSuccessAlert: boolean = false;
  constructor(
    private courseService: ViewCourseService,
    private authService: AuthService,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    this.loadCourses();
  }

  loadCourses(): void {
    // Assuming a service method to get courses created by the current trainer
    this.courseService.getCoursesByTrainer().subscribe(
      (response) => {
        // Ensure response.data is an array
        console.log('Courses:', response.data);
        this.courses = Array.isArray(response.data) ? response.data : [];  // Safeguard to ensure courses is an array
      },
      (error) => {
        console.error('Error loading courses:', error);
      }
    );
  }
  

  onFileChange(event: any): void {
    this.file = event.target.files[0];
  }

  onSubmit(): void {
    if (!this.selectedCourseId || !this.file) {
      this.errorMessage = 'Please select a course and upload a file.';
      return;
    }

    const formData = new FormData();
    formData.append('Title', this.title);
    formData.append('Description', this.description);
    formData.append('File', this.file);

    const headers = new HttpHeaders().set(
      'Authorization',
      `Bearer ${this.authService.getToken()}`
    );

    this.http
      .post<any>(`http://localhost:5276/api/Module?courseId=${this.selectedCourseId}`, formData, {
        headers,
        observe: 'events',
        reportProgress: true,
      })
      .pipe(
        map((event: HttpEvent<any>) => {
          switch (event.type) {
            case HttpEventType.UploadProgress:
              if (event.total) {
                this.uploadProgress = Math.round((100 * event.loaded) / event.total);
              }
              break;
            case HttpEventType.Response:
              console.log('Upload complete:', event.body);
              this.successMessage = 'Module added successfully!';
              this.showSuccessAlert = true;  // Show success alert
              setTimeout(() => this.showSuccessAlert = false, 5000);
              break;
          }
        }),
        catchError((error) => {
          this.errorMessage = 'Failed to add module.';
        this.showErrorAlert = true; // Show error alert
        setTimeout(() => this.showErrorAlert = false, 5000);
          throw error;
        })
      )
      .subscribe();
  }
}
