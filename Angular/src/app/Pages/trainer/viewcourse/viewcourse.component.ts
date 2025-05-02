import { Component } from '@angular/core';
import { NavbarComponent } from '../../../components/navbar/navbar.component';
import { ViewCourseService } from '../../../Services/trainer/view-course.service';
import { AuthService } from '../../../Services/auth.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-viewcourse',
  standalone: true,
  imports: [NavbarComponent,CommonModule,RouterModule],
  templateUrl: './viewcourse.component.html',
  styleUrl: './viewcourse.component.css'
})
export class ViewcourseComponent {
  courses: any[] = [];
  loading = false;
  errorMessage: string = '';

  constructor(
    private courseService: ViewCourseService,
    private authService: AuthService // Assuming AuthService holds the user data and token
  ) {}

  ngOnInit(): void {
    this.fetchCourses();
  }

  // Fetch courses created by the logged-in trainer
  fetchCourses() {
    this.loading = true;
    const trainerId = this.authService.getToken(); // Extract trainerId from the JWT token
    console.log("from course view"+trainerId);
    if (!trainerId) {
      this.errorMessage = 'Trainer not authenticated.';
      this.loading = false;
      return;
    }

    this.courseService.getCoursesByTrainer().subscribe(
      (response) => {
        this.courses = response.data; // Assuming response has a data field with courses
        console.log(this.courses);
        this.loading = false;
      },
      (error) => {
        this.errorMessage = 'Failed to load courses.';
        this.loading = false;
      }
    );
  }
}
