import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from "../../../components/navbar/navbar.component";

@Component({
  selector: 'app-assignmentsubmitted',
  standalone: true,
  imports: [CommonModule, NavbarComponent],
  templateUrl: './assignmentsubmitted.component.html',
  styleUrls: ['./assignmentsubmitted.component.css']
})
export class AssignmentsubmittedComponent implements OnInit {
  submittedAssignments: any[] = [];  // Array to hold the assignments
  errorMessage: string = '';  // To show any error message
  apiUrl = 'http://localhost:5171/api/Assignment';  // Your backend URL to fetch assignments

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.fetchSubmittedAssignments();  // Fetch the data when the component initializes
  }

  // Method to fetch the submitted assignments
  fetchSubmittedAssignments(): void {
    const token = localStorage.getItem('jwt_token'); // Get the token from local storage
  
    if (!token) {
      this.errorMessage = 'User not authenticated';  // Handle missing token case
      return;
    }
  
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
  
    // Make HTTP GET request to the API
    this.http.get<any[]>(`${this.apiUrl}/Submitted`, { headers })
      .subscribe(
        (data) => {
          this.submittedAssignments = data;  // Assign the received data to the array
          console.log(this.submittedAssignments); // Log to verify the structure
        },
        (error) => {
          this.errorMessage = 'Error fetching assignments: ' + error.message;  // Handle errors
          console.error(error);
        }
      );
  }
  
}
