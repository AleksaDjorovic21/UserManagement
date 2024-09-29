import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms'; 
import { CommonModule } from '@angular/common';
import { Register } from '../../../models/register.model';

@Component({
  selector: 'app-register',
  standalone: true,
  templateUrl: './register.component.html',
  styleUrl: `./register.component.css`,
  imports: [FormsModule, CommonModule], 
})
export class RegisterComponent {
  user: Register = { 
    firstName: '', 
    lastName: '', 
    email: '', 
    password: ''       
  };
  successMessage: string = ''; 

  constructor(private authService: AuthService, private router: Router) {}

  register() {
    this.authService.register(this.user).subscribe(
      (response) => {
        this.successMessage = response; 
  
        setTimeout(() => {
          this.router.navigate(['/login']);
        }, 1000);
      },
      (error) => {
        console.log('Registration failed', error);
      }
    );
  }
}
