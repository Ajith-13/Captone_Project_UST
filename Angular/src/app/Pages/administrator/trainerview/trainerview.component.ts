import { Component } from '@angular/core';
import { Trainer, TrainerapprovalService } from '../../../Services/admin/trainerapproval.service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from '../../../components/navbar/navbar.component';
import { DomSanitizer } from '@angular/platform-browser';  // Import DomSanitizer

@Component({
  selector: 'app-trainerview',
  standalone: true,
  imports: [CommonModule,NavbarComponent],
  templateUrl: './trainerview.component.html',
  styleUrl: './trainerview.component.css'
})
export class TrainerviewComponent {
  trainer!: Trainer;
  baseFileUrl = 'http://localhost:7266/';

  constructor(
    private route: ActivatedRoute,
    private trainerService: TrainerapprovalService,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
    const trainerId = this.route.snapshot.paramMap.get('id');
    if (trainerId) {
      this.trainerService.getTrainerById(trainerId).subscribe({
        next: (data) => this.trainer = data,
        error: (err) => console.error('Error fetching trainer:', err)
      });
    }
  }

  getCertificateUrl() {
    return this.sanitizer.bypassSecurityTrustResourceUrl(this.trainer.certificatePath ?? '');
  }

  getResumeUrl() {
    return this.sanitizer.bypassSecurityTrustResourceUrl(this.trainer.resumePath ?? '');
  }
  isImage(filePath: string): boolean {
    const imageExtensions = ['.jpg', '.jpeg', '.png', '.gif'];
    const ext = filePath.toLowerCase().slice(-4);
    return imageExtensions.includes(ext);
  }
  
  isPdf(filePath: string): boolean {
    return filePath.toLowerCase().endsWith('.pdf');
  }
  
  isDocument(filePath: string): boolean {
    const documentExtensions = ['.doc', '.docx', '.xls', '.xlsx', '.txt'];
    const ext = filePath.toLowerCase().slice(-4);
    return documentExtensions.includes(ext);
  }
  toggleApproval(isApproved: boolean): void {
    if (!this.trainer) return;
  
    this.trainerService.approveTrainer(this.trainer.id, isApproved).subscribe({
      next: (res) => {
        this.trainer!.approvalStatus = isApproved ? 'Approved' : 'Pending';
      },
      error: (err) => {
        console.error('Approval action failed:', err);
      }
    });
  }
  
}
