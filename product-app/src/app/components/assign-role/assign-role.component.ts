import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-assign-role',
  standalone: true,
  templateUrl: './assign-role.component.html',
  styleUrls: ['./assign-role.component.css'],
  imports: [CommonModule],
})
export class AssignRoleComponent implements OnInit {
  users: any[] = []; 
  loading: boolean = false;
  filteredUsers: any[] = [];
  successMessage: string = '';

  constructor(private authService: AuthService, private http: HttpClient) {}

  ngOnInit() {
    this.fetchUsers(); 
  }

  fetchUsers() {
    this.authService.getAllUsers().subscribe(
      (users) => {
        this.users = users;
      },
      (error) => {
        if (error.status === 401) {
          console.error('Unauthorized access. Redirecting to login.');
          // Optionally redirect to login or show a message
        } else if (error.status === 403) {
          console.error('Forbidden access. You do not have permission to view users.');
          // Handle forbidden error
        } else {
          console.error('Error fetching users', error);
        }
      }
    );
  }
  
  assignRole(userId: string) {
    if (confirm("Do you want to assign this user as admin?")) {
      this.authService.assignAdminRole(userId).subscribe(
        (response) => {
          this.successMessage = response; 
        },
        (error) => {
        console.log('Error assigning role:', error);
        }
      );
    }
  }
}

