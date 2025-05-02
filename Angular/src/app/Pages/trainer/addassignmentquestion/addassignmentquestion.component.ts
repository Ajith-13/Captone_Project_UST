import { Component, OnInit } from '@angular/core';
import { NavbarComponent } from '../../../components/navbar/navbar.component';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../../../Services/auth.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-addassignmentquestion',
  standalone: true,
  imports: [NavbarComponent,FormsModule,CommonModule],
  templateUrl: './addassignmentquestion.component.html',
  styleUrl: './addassignmentquestion.component.css'
})
export class AddassignmentquestionComponent implements OnInit {
  assignment = {
    title: '',
    description: '',
    dueDate: '',
    totalMarks: 0,
    courseId: null,
    applyToAllModules: true,  // Default to applying to all modules
    moduleId: null as number | number[] | null,  // Store selected module ids
  };
  
  courses: any[] = [];
  modules: any[] = [];
  alertMessage: string = ''; // Holds the message for alert
  alertTitle: string = ''; // Holds the title for alert
  showSuccess: boolean = false; // Flag to show success message
  showError: boolean = false;
  constructor(private http: HttpClient, private authService: AuthService) {}

  ngOnInit(): void {
    this.loadCourses();
  }

  // Load courses based on the trainer's access
  loadCourses() {
    this.http.get<any>('http://localhost:5276/api/Course').subscribe(
      (response) => {
        console.log(response);  // Check what the response looks like
        if (response && response.data) {
          this.courses = response.data;  // Extract the array from the response object
        }
      },
      (error) => {
        console.error('Error fetching courses:', error);
      }
    );
  }
  

  // Load modules when a course is selected
  loadModules() {
    if (this.assignment.courseId) {
      this.http
        .get<any>(`http://localhost:5276/api/Module/course/${this.assignment.courseId}`)
        .subscribe((response) => {
          console.log('API response:', response);
          if (response && Array.isArray(response.data)) {
            this.modules = response.data; // Access modules inside 'data'
          } else {
            console.error('Expected modules array inside "data", but got:', response);
          }
        });
    }
  }
  
  

  // Handle form submission
  onSubmit() {
    // If applying to all modules, set moduleId to null (not needed)
    if (this.assignment.applyToAllModules) {
      this.assignment.moduleId = null;
    } else if (Array.isArray(this.assignment.moduleId) && this.assignment.moduleId.length > 0) {
      // Safely assign the first moduleId if it's an array
      this.assignment.moduleId = this.assignment.moduleId[0];
    }
  
    console.log('Selected Module ID:', this.assignment.moduleId);
    console.log('Assignment to be sent:', this.assignment);
  
    // Retrieve the token from localStorage
    const token = localStorage.getItem('jwt_token'); // Adjust based on where you store your token
  
    // Create headers with the Authorization token
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
  
    // Send the assignment data to the API with the Authorization header
    this.http.post('http://localhost:5171/api/AssignmentQuestion', this.assignment, { headers }).subscribe(
      (response) => {
        console.log('Assignment question added:', response);
        this.showSuccess = true;
        this.showError = false; // Hide error message if successful
        this.alertTitle = 'Success';
        this.alertMessage = 'Assignment question added successfully.';
        setTimeout(() => {
          this.showSuccess = false;
        }, 3000);
        this.resetForm();
        // Handle success (e.g., show success message or reset form)
      },
      (error) => {
        console.error('Error adding assignment question:', error);
        // Handle error (e.g., show error message)
        this.showError = true;
        this.showSuccess = false; // Hide success message if there's an error
        this.alertTitle = 'Error';
        this.alertMessage = 'Failed to add assignment question. Please try again.';
        setTimeout(() => {
          this.showSuccess = false;
        }, 3000); 
      }
    );
  }
  resetForm() {
    // Reset the form fields after successful submission (optional)
    this.assignment = {
      title: '',
      description: '',
      dueDate: '',
      totalMarks: 0,
      courseId: null,
      applyToAllModules: true,
      moduleId: null
    };
  }
  
}