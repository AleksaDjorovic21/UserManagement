import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ProductFormComponent } from './product-form.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ProductService } from '../../services/product.service';
import { AuthService } from '../../services/auth.service';
import { of } from 'rxjs';
import { ActivatedRoute } from '@angular/router';

// Create a mock ProductService
class MockProductService {
  getProduct(id: number) {
    return of({ id, name: 'Test Product', price: 100, description: 'Test Description' }); 
  }

  updateProduct(product: any) {
    return of(null); 
  }

  createProduct(product: any) {
    return of(null); 
  }
}

// Create a mock AuthService
class MockAuthService {
  currentUser = of({ roles: ['Admin'] }); 
}

// Create a mock ActivatedRoute
class MockActivatedRoute {
  snapshot = {
    paramMap: {
      get: (key: string) => {
        if (key === 'id') return '1'; 
        return null;
      }
    }
  };
}

describe('ProductFormComponent', () => {
  let component: ProductFormComponent;
  let fixture: ComponentFixture<ProductFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, ProductFormComponent],
      providers: [
        { provide: ProductService, useClass: MockProductService }, 
        { provide: AuthService, useClass: MockAuthService }, 
        { provide: ActivatedRoute, useClass: MockActivatedRoute } 
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(ProductFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

