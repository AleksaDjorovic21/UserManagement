import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { AuthService } from '../../services/auth.service';
import { FormsModule } from '@angular/forms'; 
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-form',
  standalone: true,
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css'],
  imports: [FormsModule, CommonModule],
})
export class ProductFormComponent implements OnInit {
  product: any = { name: '', price: 0, description: '' };
  isAdmin: boolean = false;
  updateSuccess: boolean = false;

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService 
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.productService.getProduct(+id).subscribe((data) => {
        this.product = data;
      });
    }

    // Check if the user is an admin
    this.authService.currentUser.subscribe(user => {
      this.isAdmin = user && user.roles.includes('Admin');
    });
  }

  save() {
    if (!this.isAdmin) {
      alert('Only Admins can add or edit products!');
      return;
    }

    if (this.product.id) {
      this.productService.updateProduct(this.product).subscribe(() => {
        this.updateSuccess = true;
        this.router.navigate(['/products']);
      });
    } else {
      this.productService.createProduct(this.product).subscribe(() => {
        this.updateSuccess = true;
        this.router.navigate(['/products']);
      });
    }
  }
}
