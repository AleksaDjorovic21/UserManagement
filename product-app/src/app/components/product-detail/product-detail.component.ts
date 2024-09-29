import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { AuthService } from '../../services/auth.service';
import { CommonModule } from '@angular/common'; 
import { RouterModule } from '@angular/router'; 

@Component({
  selector: 'app-product-detail',
  standalone: true,
  templateUrl: './product-detail.component.html',
  imports: [CommonModule, RouterModule],
})
export class ProductDetailComponent implements OnInit {
  product: any = {};
  isAdmin: boolean = false;

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    protected router: Router,
    private authService: AuthService 
  ) {}

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.productService.getProduct(+id).subscribe((data) => {
        this.product = data;
      });
    } else {
      this.router.navigate(['/products']);
    }
  }

  deleteProduct() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.productService.deleteProduct(+id).subscribe(() => {
        this.router.navigate(['/products']);
      });
    }
  }
}
