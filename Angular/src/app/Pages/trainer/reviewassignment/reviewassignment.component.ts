import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { NavbarComponent } from "../../../components/navbar/navbar.component";
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SafeResourceUrl } from '@angular/platform-browser';
import { DomSanitizer } from '@angular/platform-browser';  // Make sure to import DomSanitizer

@Component({
  selector: 'app-reviewassignment',
  templateUrl: './reviewassignment.component.html',
  styleUrls: ['./reviewassignment.component.css'],
  standalone: true,
  imports: [NavbarComponent, CommonModule, FormsModule]
})
export class ReviewassignmentComponent implements OnInit {
  assignments: any[] = [];  // Holds the fetched assignments
  error: string | null = null;  // Holds any error message
  successMessage: string | null = null;

  constructor(
    private http: HttpClient,
    private router: Router,
    private sanitizer: DomSanitizer
  ) {}

  // Initialize the component and fetch assignments
  ngOnInit(): void {
    this.fetchAssignments();
  }

  /**
   * Fetch assignments for the trainer using an API call
   */
  fetchAssignments(): void {
    const token = localStorage.getItem('jwt_token');  // Get JWT token from localStorage
    if (!token) {
      this.error = 'User not authenticated.';
      return;
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    // Make API call to fetch assignments
    this.http.get<any>('http://localhost:5171/api/Assignment/TrainerAssignments', { headers })
      .subscribe(
        (response) => {
          // Check if response is empty or doesn't contain assignments
          if (response && response.length > 0) {
            console.log(response);
            this.assignments = response;  // Assign the fetched assignments to the component's array
            this.error = null;  // Reset error if data is fetched successfully
          } else {
            this.error = 'No assignments found for this trainer.';
          }
        },
        (error) => {
          // Handle HTTP request errors
          this.error = 'Error fetching assignments for the trainer.';
          console.error('Error fetching data:', error);
        }
      );
  }

  /**
   * Update marks for a specific assignment
   * @param assignmentId - The ID of the assignment to update
   * @param newMarks - The new marks to update
   */
  updateMarks(assignmentId: number, newMarks: number): void {
    const token = localStorage.getItem('jwt_token'); // Get JWT token from localStorage
    console.log("From the update marks");
  
    if (!token) {
      this.error = 'User not authenticated.';
      return;
    }
  
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
  
    // Prepare the data to send in the request
    const updateData = {
      Marks: newMarks
    };
  
    // Send PUT request to update the marks
    this.http.put<any>(`http://localhost:5171/api/Assignment/${assignmentId}`, updateData, { headers })
      .subscribe(
        (response) => {
          // Handle successful mark update
          console.log(updateData);
          this.successMessage = 'Marks updated successfully!';
          setTimeout(() => {
            this.successMessage = null;
          }, 3000);
          this.fetchAssignments();  // Refresh the assignments list after updating marks
        },
        (error) => {
          // Handle errors when updating marks
          this.successMessage = 'Error updating the marks';
          console.error('Error updating marks:', error);
        }
      );
  }
  

  /**
   * Handle mark input validation
   * Ensure that marks are within the valid range (0 to totalMarks)
   */
  validateMarks(marks: number, totalMarks: number): boolean {
    return marks >= 0 && marks <= totalMarks;
  }
  // Sanitize the URL using DomSanitizer to avoid security errors
getSafeUrl(url: string) {
  console.log('URL passed for sanitization:', url);
  if (url) {
    // Ensure the URL is not null/undefined and then sanitize it
    return this.sanitizer.bypassSecurityTrustResourceUrl(url);
  }
  return '';  // Return an empty string if the URL is invalid
}



}
