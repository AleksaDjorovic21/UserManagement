import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AssignRoleComponent } from './assign-role.component';
import { HttpClientTestingModule } from '@angular/common/http/testing'; 
import { AuthService } from '../../services/auth.service';

describe('AssignRoleComponent', () => {
  let component: AssignRoleComponent;
  let fixture: ComponentFixture<AssignRoleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AssignRoleComponent, HttpClientTestingModule], 
      providers: [AuthService], 
    })
    .compileComponents();

    fixture = TestBed.createComponent(AssignRoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});


