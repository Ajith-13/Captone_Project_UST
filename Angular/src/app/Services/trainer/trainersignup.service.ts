import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

interface TrainerRegisterResponse {
  success: string;
}

interface SignUpRequest {
  userName:string;
  email:string;
  password:string;
  certificate: File | null;
  resume: File | null;
}

@Injectable({
  providedIn: 'root'
})
export class TrainersignupService {
  private apiUrl = 'http://localhost:7266/api/Auth/register/trainer';

  constructor(private http: HttpClient) { }
  registerTrainer(data: SignUpRequest): Observable<any> {
    const formData = new FormData();
  
    formData.append('UserName', data.userName);
    formData.append('Email', data.email);
    formData.append('Password', data.password);
  
    // Check if certificate and resume files exist and append them
    if (data.certificate) {
      formData.append('Certificate', data.certificate, data.certificate.name); // Make sure the field names match with backend DTO
    }
  
    if (data.resume) {
      formData.append('Resume', data.resume, data.resume.name);
    }
  
    // Send the POST request with multipart form data
    return this.http.post(this.apiUrl, formData);
  }
  
}
