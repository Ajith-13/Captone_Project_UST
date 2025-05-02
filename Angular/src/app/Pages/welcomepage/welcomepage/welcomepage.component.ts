import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { NavbarComponent } from '../../../components/navbar/navbar.component';

@Component({
  selector: 'app-welcomepage',
  standalone: true,
  imports: [FormsModule,NavbarComponent],
  templateUrl: './welcomepage.component.html',
  styleUrl: './welcomepage.component.css'
})
export class WelcomepageComponent {
  router=inject(Router)
  onLogin(){
    
    this.router.navigateByUrl('login')
  }
  onRegister(){
    this.router.navigateByUrl('register')
  }
  admin(){
    this.router.navigateByUrl('administrator')
  }
  trainerlogin(){
    this.router.navigateByUrl('trainer-signin') 
  }
}
