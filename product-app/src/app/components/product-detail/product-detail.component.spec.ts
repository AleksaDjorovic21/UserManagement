import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ProductDetailComponent } from './product-detail.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ProductService } from '../../services/product.service';
import { AuthService } from '../../services/auth.service';
import { convertToParamMap, Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';

// Mock services
class MockProductService {
  getProduct(id: number) {
    return of({ id, name: 'Test Product' }); 
  }
  
  deleteProduct(id: number) {
    return of(null); 
  }
}

class MockAuthService {
  isAdmin() {
    return true; 
  }
}

const mockActivatedRoute = {
  snapshot: {
    paramMap: convertToParamMap({ id: '1' }) 
  }
};

describe('ProductDetailComponent', () => {
  let component: ProductDetailComponent;
  let fixture: ComponentFixture<ProductDetailComponent>;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [HttpClientTestingModule, ProductDetailComponent],
      providers: [
        { 
          provide: ActivatedRoute, 
          useValue: mockActivatedRoute 
        },
        { provide: ProductService, useClass: MockProductService },
        { provide: AuthService, useClass: MockAuthService },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(ProductDetailComponent);
    component = fixture.componentInstance;
    router = TestBed.inject(Router); 
    fixture.detectChanges(); 
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch the product on init', () => {
    expect(component.product).toEqual({ id: 1, name: 'Test Product' });
  });
  
  it('should navigate to products list on delete', () => {
    spyOn(router, 'navigate');
    component.deleteProduct();
    expect(router.navigate).toHaveBeenCalledWith(['/products']);
  });
});
