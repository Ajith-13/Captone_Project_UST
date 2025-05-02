import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../auth.service';


export interface Trainer {
  id: string;
  userName: string;
  email: string;
  approvalStatus: string;
  certificatePath?: string;
  resumePath?: string;
}


@Injectable({
  providedIn: 'root'
})
export class TrainerapprovalService {
  private apiUrl = 'http://localhost:7266/api/Auth'; // Adjust if your route is different

  constructor(private http: HttpClient,private adminLoginService: AuthService) {}

 private getAuthHeaders(): { headers: HttpHeaders } {
    const token = this.adminLoginService.getToken();
    return {
      headers: new HttpHeaders({
        Authorization: `Bearer ${token}`
      })
    };
  }

  getAllTrainers(): Observable<Trainer[]> {
    return this.http.get<Trainer[]>(`${this.apiUrl}/all-trainers`, this.getAuthHeaders());
  }
  getTrainerById(id: string): Observable<Trainer> {
    return this.http.get<Trainer>(`${this.apiUrl}/trainer/${id}`,this.getAuthHeaders());
  }
  approveTrainer(trainerId: string, isApproved: boolean): Observable<any> {
    return this.http.post(`${this.apiUrl}/approve-trainer`, {
      trainerId: trainerId,
      isApproved: isApproved
    }, this.getAuthHeaders());
  }
  
}
