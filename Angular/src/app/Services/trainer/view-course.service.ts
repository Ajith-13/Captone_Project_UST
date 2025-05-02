import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../auth.service';

@Injectable({
  providedIn: 'root'
})
export class ViewCourseService {

  private apiUrl = 'http://localhost:5276/api/Course/trainer';
  private url = 'http://localhost:5276/api/Course'; // Replace with your API URL
  

  constructor(private http: HttpClient,private authService:AuthService) {}

  // Method to fetch courses created by the logged-in trainer
  getCoursesByTrainer(): Observable<any> {
    const token =this.authService.getToken(); // Retrieve JWT token from localStorage or sessionStorage
    if (!token) {
      throw new Error('No token found');
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.get<any>(`${this.apiUrl}`, { headers });
  }
  getAllCourses(): Observable<any> {
    const token = this.authService.getToken(); // Retrieve JWT token from localStorage or sessionStorage
    if (!token) {
      throw new Error('No token found');
    }
  
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
  
    // Assuming your API endpoint for getting all courses is something like '/courses'
    return this.http.get<any>(`${this.url}/`, { headers });
  }
  
}
