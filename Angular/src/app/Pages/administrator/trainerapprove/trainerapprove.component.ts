import { Component, inject } from '@angular/core';
import { NavbarComponent } from '../../../components/navbar/navbar.component';
import { Trainer, TrainerapprovalService } from '../../../Services/admin/trainerapproval.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-trainerapprove',
  standalone: true,
  imports: [NavbarComponent,CommonModule],
  templateUrl: './trainerapprove.component.html',
  styleUrl: './trainerapprove.component.css'
})
export class TrainerapproveComponent {
  router=inject(Router)

  trainers: Trainer[] = [];

  constructor(private trainerService: TrainerapprovalService) {}

  ngOnInit(): void {
    this.trainerService.getAllTrainers().subscribe({
      next: (data) => this.trainers = data,
      error: (err) => console.error('Error fetching trainers:', err)
    });
  }

  approveTrainer(trainerId: string, isCurrentlyPending: boolean): void {
    this.trainerService.approveTrainer(trainerId, isCurrentlyPending).subscribe({
      next: (response) => {
        console.log(response);
        this.trainerService.getAllTrainers().subscribe({
          next: (trainers) => {
            this.trainers = trainers; // Update the local trainers array
          },
          error: (err) => {
            console.error('Error fetching updated trainers:', err);
          }
        });
      },
      error: (err) => {
        console.error('Error approving trainer:', err);
      }
    });
  }
  
  toggleApproval(trainer: Trainer): void {
    const isCurrentlyPending = trainer.approvalStatus.toLowerCase() === 'pending';
  
    this.trainerService.approveTrainer(trainer.id, isCurrentlyPending).subscribe({
      next: (response) => {
        console.log(response);
        // Fetch updated trainer list and update UI
        this.trainerService.getAllTrainers().subscribe({
          next: (trainers) => {
            this.trainers = trainers;
          },
          error: (err) => {
            console.error('Error fetching updated trainers:', err);
          }
        });
      },
      error: (err) => {
        console.error('Error updating approval status:', err);
      }
    });
  }
  
  
  viewTrainer(trainer: Trainer): void {
    this.router.navigate(['/trainer-details', trainer.id]);
  }
}
