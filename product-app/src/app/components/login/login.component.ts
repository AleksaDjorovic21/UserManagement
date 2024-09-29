import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Login } from '../../../models/login.model';

@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.component.html',
  styleUrl: `./login.component.css`,
  imports: [FormsModule],
})
export class LoginComponent {
  loginData: Login = { email: '', password: '' };

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    this.authService.login(this.loginData).subscribe(
      () => {
        this.router.navigate(['/products']); 
      },
      (error: any) => {
        console.error('Login failed', error);
      }
    );
  }
  
  goToRegister() {
    this.router.navigate(['/register']); 
  }

}
