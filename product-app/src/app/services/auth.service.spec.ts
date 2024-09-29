import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing'; 
import { AuthService } from './auth.service';
import { Login } from '../../models/login.model';

describe('AuthService', () => {
  let service: AuthService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule], 
    });
    service = TestBed.inject(AuthService);
    httpTestingController = TestBed.inject(HttpTestingController); 
  });

  afterEach(() => {
    httpTestingController.verify(); 
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should login a user', () => {
    const mockUser = { username: 'testUser', token: '12345' };
    const loginCredentials: Login = { email: 'testUser', password: 'password' }; 

    service.login(loginCredentials).subscribe(user => {
      expect(user).toEqual(mockUser);
    });

    const req = httpTestingController.expectOne('http://localhost:5099/api/ApplicationUser/login');
    expect(req.request.method).toEqual('POST');
    req.flush(mockUser); 
  });
});
