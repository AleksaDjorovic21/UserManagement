import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RegisterComponent } from './register.component';
import { HttpClientTestingModule } from '@angular/common/http/testing'; 
import { AuthService } from '../../services/auth.service'; 
import { Router } from '@angular/router'; 
import { of } from 'rxjs';

// Create a mock AuthService
class MockAuthService {
  register(user: any) {
    return of('Registration successful'); 
  }
}

describe('RegisterComponent', () => {
  let component: RegisterComponent;
  let fixture: ComponentFixture<RegisterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RegisterComponent, HttpClientTestingModule], 
      providers: [
        { provide: AuthService, useClass: MockAuthService },
        { provide: Router, useValue: { navigate: jasmine.createSpy('navigate') } } 
      ],
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
