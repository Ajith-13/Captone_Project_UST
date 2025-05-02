import { Component, OnInit } from '@angular/core';
import { NavbarComponent } from "../../../components/navbar/navbar.component";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-singlemodules',
  standalone: true,
  imports: [NavbarComponent,CommonModule],
  templateUrl: './singlemodules.component.html',
  styleUrl: './singlemodules.component.css'
})
export class SinglemodulesComponent implements OnInit{
  moduleId!: number;  // Holds the module ID from the route
  moduleDetails: any = null;  // Holds the details of the module
  assignments: any[] = [];  // Holds the assignments related to the module
  loading: boolean = true;  // Track loading state
  error: string | null = null;  // Error message

  constructor(
    private route: ActivatedRoute,  // To access route parameters
    private http: HttpClient  // To make API calls
  ) {}

  ngOnInit(): void {
    console.log("From console");
  
    // Retrieve the moduleId from route parameters
    this.route.params.subscribe(params => {
      console.log('Route params:', params); 
      const id = +params['moduleId'];  // Convert the 'id' parameter to a number
      if (!isNaN(id)) {
        this.moduleId = id;  // Valid moduleId
        console.log(this.moduleId);  // This should log the id value
        this.loadModuleDetails();  // Fetch module details
        this.loadAssignments();  // Fetch assignments for the module
      } else {
        this.error = 'Invalid module ID provided.';
        this.loading = false;
      }
    });
    
  }
  

  // Fetch the details of the module from the backend
  loadModuleDetails(): void {
    const token = localStorage.getItem('jwt_token');  // Get JWT token from localStorage

    if (!token) {
      this.error = 'Token not found. Please log in again.';
      this.loading = false;
      return;
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    // Fetch module details from the API using the moduleId
    // In loadModuleDetails()
this.http.get<any>(`http://localhost:5276/api/Module/${this.moduleId}`, { headers })
.subscribe(
  (response) => {
    console.log('Module Details:', response);  // Check API response
    if (response && response.data) {
      console.log(response);
      this.moduleDetails = response.data;
      this.loading = false;
    } else {
      this.error = 'No data found for this module.';
      this.loading = false;
    }
  },
  (error) => {
    console.error('Error fetching module details:', error);  // Log error
    this.error = 'Error fetching module details.';
    this.loading = false;
  }
);

  }

  // Fetch assignments related to the module
  loadAssignments(): void {
    const token = localStorage.getItem('jwt_token');  // Get JWT token from localStorage
  
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
  
    this.http.get<any>(`http://localhost:5171/api/AssignmentQuestion/module/${this.moduleId}`, { headers })
      .subscribe(
        (response) => {
          if (response && response.length > 0) {
            this.assignments = response;  
            console.log('Assignments:', this.assignments); // Log assignments to check
          } else {
            this.error = 'No assignments found for this module.';
          }
        },
        (error) => {
          this.error = 'Error fetching assignments.';
          console.error(error);
        }
      );
  }
  onFileSelected(event: any, assignment: any): void {
    const file: File = event.target.files[0];
    if (file && file.type === 'application/pdf') {
      assignment.file = file;  // Store the selected file in the assignment object
      assignment.submissionStatus = null;  // Reset submission status
    } else {
      assignment.submissionStatus = 'Please upload a PDF file.';
      assignment.file = null;
    }
  }

  // Submit the assignment
  submitAssignment(assignment: any): void {
    const token = localStorage.getItem('jwt_token');  // Get JWT token from localStorage
  
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    if (!assignment.file) {
      assignment.submissionStatus = 'Please select a PDF file before submitting.';
      return;
    }

    const formData = new FormData();
    formData.append('LearnerId', 'learner123');  // Use actual learner ID
    formData.append('FilePath', assignment.file);  // Append the PDF file
    formData.append('AssignmentQuestionId', assignment.id.toString());  // Assign the assignment ID

    // Call API to submit the assignment
    this.http.post('http://localhost:5171/api/Assignment', formData, { headers })
      .subscribe(
        (response: any) => {
          assignment.submissionStatus = 'Assignment submitted successfully!';
        },
        (error: any) => {
          assignment.submissionStatus = 'Error submitting assignment.';
        }
      );
  }
}
