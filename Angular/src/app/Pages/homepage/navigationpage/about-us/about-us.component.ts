import { Component } from '@angular/core';
import { NavbarComponent } from '../../../../components/navbar/navbar.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-about-us',
  standalone: true,
  imports: [NavbarComponent,CommonModule,FormsModule],
  templateUrl: './about-us.component.html',
  styleUrl: './about-us.component.css'
})
export class AboutUsComponent {

}
