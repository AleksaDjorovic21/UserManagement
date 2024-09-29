import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog'; 
import { ProductService } from '../../services/product.service';
import { Product } from '../../../models/product.model';
import { FormsModule } from '@angular/forms'; 
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-product-dialog',
  standalone: true,
  templateUrl: './create-product-dialog.component.html',
  styleUrls: ['./create-product-dialog.component.css'],
  imports: [FormsModule, CommonModule], 
})
export class CreateProductDialogComponent {
  product: Product = { name: '', price: 0, description: '' }; 
  creationSuccess = false;

  constructor(
    private dialogRef: MatDialogRef<CreateProductDialogComponent>,
    private productService: ProductService,
    private router: Router
    ) {}

  save() {
    if (this.product.name && this.product.price > 0 && this.product.description) {
      this.productService.createProduct(this.product).subscribe(
        () => {
          this.creationSuccess = true;

          // Optionally redirect after a successful creation
          this.router.navigate(['/products']); 
          
          // Close the dialog after saving
          this.dialogRef.close();
        },
        (error) => {
          console.error('Error creating product:', error);
          alert('Failed to create product. Please try again.'); 
        }
      );
    } else {
      alert('Please fill in all required fields.');
    }
  }

  cancel() {
    this.dialogRef.close(); 
  }
}

