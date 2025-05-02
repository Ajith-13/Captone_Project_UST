import { Component, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit, OnChanges {
  isLoggedIn = false;
  role: string | null = null;
  userName: string | null = null;

  constructor(private authService: AuthService, private router: Router) {
    console.log('NavbarComponent constructor called');
  }

  ngOnInit(): void {
    console.log('Navbar component initialized');
    this.checkAuth();
  }

  ngOnChanges(changes: SimpleChanges): void {
    console.log('Navbar component changes detected', changes);
    this.checkAuth();
  }

  checkAuth(): void {
    this.isLoggedIn = this.authService.isAuthenticated();
    this.role = this.authService.getUserRole();
    // alert('Role: ' + this.role);
    this.userName = this.authService.getUserName();
  }

  trainerLogin(): void {
    this.router.navigate(['/trainer-signin']);
  }

  adminLogin(): void {
    this.router.navigate(['/administrator']);
  }

  logout(): void {
    this.authService.removeToken();
    this.isLoggedIn = false;
    this.role = null;
    this.userName = null;
    this.router.navigate(['/welcome']);
    }
}
