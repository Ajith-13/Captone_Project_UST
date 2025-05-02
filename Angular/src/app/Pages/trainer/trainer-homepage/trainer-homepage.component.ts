import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-trainer-homepage',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './trainer-homepage.component.html',
  styleUrl: './trainer-homepage.component.css'
})
export class TrainerHomepageComponent {
  router=inject(Router)
  showCourses = false;

  courses: string[] = [
    'C', 'C++', 'Python', 'Dot Net', 'Java', 'Java Script', 'HTML', 'CSS',
    'DBMS', 'DSA', 'Frontend', 'Backend', 'Full-Stack', 'Machine Learning',
    'Deep Learning', 'Data Scientist', 'Artificial Intelligence', 'Data Engineer', 'Software Engineer'
  ];

  toggleCourses() {
    this.showCourses = !this.showCourses;
  }

}
