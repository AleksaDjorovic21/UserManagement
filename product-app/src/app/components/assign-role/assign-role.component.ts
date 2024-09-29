import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common';
import { User } from '../../../models/user.model';
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
    this.loading = true; 
    this.authService.getAllUsers().subscribe(
        (data: User[]) => {
            this.users = data;
            this.filteredUsers = this.users.filter(user => user.role !== 'Admin');
            this.loading = false; 
        },
        (error: any) => {
            console.error('Error fetching users', error);
            alert('Could not fetch users. Please try again later.');
            this.loading = false; 
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

