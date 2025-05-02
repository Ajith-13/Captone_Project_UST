import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NavbarComponent } from '../../../components/navbar/navbar.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-singlemodule',
  standalone: true,
  imports: [NavbarComponent, CommonModule],
  templateUrl: './singlemodule.component.html',
  styleUrls: ['./singlemodule.component.css']
})
export class SinglemoduleComponent implements OnInit {
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
    // Retrieve the moduleId from route parameters
    this.route.params.subscribe(params => {
      const id = +params['id'];  // Convert the 'id' parameter to a number
      if (!isNaN(id)) {
        this.moduleId = id;  // Valid moduleId
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
    this.http.get<any>(`http://localhost:5276/api/Module/${this.moduleId}`, { headers })
      .subscribe(
        (response) => {
          if (response && response.data) {
            this.moduleDetails = response.data;  // Store module details
            this.loading = false;
          } else {
            this.error = 'No data found for this module.';
            this.loading = false;
          }
        },
        (error) => {
          this.error = 'Error fetching module details.';
          console.error(error);
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
  
}
