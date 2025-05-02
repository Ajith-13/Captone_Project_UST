import { Component } from '@angular/core';
import { NavbarComponent } from '../../../components/navbar/navbar.component';
import { NavigationpageComponent } from "../../homepage/navigationpage/navigationpage.component";

@Component({
  selector: 'app-trainerlandingpage',
  standalone: true,
  imports: [NavbarComponent, NavigationpageComponent],
  templateUrl: './trainerlandingpage.component.html',
  styleUrl: './trainerlandingpage.component.css'
})
export class TrainerlandingpageComponent {

}
