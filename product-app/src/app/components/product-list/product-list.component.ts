import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service'; 
import { RouterModule } from '@angular/router'; 
import { CommonModule } from '@angular/common';
import { Product } from '../../../models/product.model';
import { CreateProductDialogComponent } from '../create-product-dialog/create-product-dialog.component';
import { MatDialog } from '@angular/material/dialog'; 

@Component({
  selector: 'app-product-list',
  standalone: true,
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],
  imports: [RouterModule, CommonModule],
})
export class ProductListComponent implements OnInit {
  products: any[] = [];
  isAdmin: boolean = false;
  private dialogRef: any;

  constructor(
    private productService: ProductService,
    private router: Router,
    private authService: AuthService, 
    private dialog: MatDialog 
  ) {}

  ngOnInit() {
    this.loadProducts();

    // Check if the user is logged in and their roles
    const userRoles = this.authService.getUserRoles();
    this.isAdmin = userRoles.includes('Admin'); 

    // Subscribe to the currentUser for any updates
    this.authService.currentUser.subscribe(user => {
      if (user) {
        this.isAdmin = user.roles.includes('Admin'); 
      }
    });
  }

  openCreateProductForm() {
    if (this.dialogRef) {
      this.dialogRef.close();
    }

    // Open a new dialog and store its reference
    this.dialogRef = this.dialog.open(CreateProductDialogComponent, {
      panelClass: 'custom-dialog-container',
      disableClose: true,
      width: '400px',
      height: 'auto',
      position: { right: '20px', top: '20px' }, 
    });

    // Clear the reference when the dialog is closed
    this.dialogRef.afterClosed().subscribe(() => {
      this.dialogRef = null; 
    });
  }

  loadProducts() {
    this.productService.getProducts().subscribe(
      (data) => {
        this.products = data.map((product: any) => ({
          ...product,
          showDescription: false 
        }));
      },
      (error) => {
        console.error('Error fetching products:', error);
      }
    );
  }

    editProduct(id: number) {
    this.router.navigate(['/products/edit', id]);
  }

    deleteProduct(id: number) {
    this.productService.deleteProduct(id).subscribe(() => {
      this.loadProducts(); 
    });
  }

  toggleDescription(product: Product): void {
    product.showDescription = !product.showDescription; 
  }

  navigateToAssignRole() {
    this.router.navigate(['/assign-role']); 
  }
}
