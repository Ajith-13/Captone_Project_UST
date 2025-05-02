import { Component } from '@angular/core';
import { NavbarComponent } from "../../../components/navbar/navbar.component";
import { ViewCourseService } from '../../../Services/trainer/view-course.service';
import { AuthService } from '../../../Services/auth.service';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-viewcourses',
  standalone: true,
  imports: [NavbarComponent,RouterModule,CommonModule],
  templateUrl: './viewcourses.component.html',
  styleUrl: './viewcourses.component.css'
})
export class ViewcoursesComponent {
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
    const userId = this.authService.getToken(); // Extract trainerId from the JWT token
    console.log("from course view"+userId);
    if (!userId) {
      this.errorMessage = 'Trainer not authenticated.';
      this.loading = false;
      return;
    }

    this.courseService.getAllCourses().subscribe(
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
