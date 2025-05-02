import { Component, OnInit } from '@angular/core';
import { NavbarComponent } from '../../../components/navbar/navbar.component';
import { AboutUsComponent } from "../../homepage/navigationpage/about-us/about-us.component";
import { ActivatedRoute, RouterModule } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-viewmodule',
  standalone: true,
  imports: [NavbarComponent, AboutUsComponent,CommonModule,RouterModule],
  templateUrl: './viewmodule.component.html',
  styleUrl: './viewmodule.component.css'
})
export class ViewmoduleComponent implements OnInit{
  courseId: number = 0;
  modules: any[] = [];

  constructor(private route: ActivatedRoute, private http: HttpClient) {}

  ngOnInit(): void {
    this.courseId = +this.route.snapshot.paramMap.get('courseId')!;
    this.loadModules();
  }

  loadModules(): void {
    this.http.get<any>(`http://localhost:5276/api/Module/course/${this.courseId}`)
      .subscribe(
        (response) => {
          this.modules = response.data;
          console.log('Modules:', this.modules);
        },
        (error) => {
          console.error('Error fetching modules', error);
        }
      );
  }
}
