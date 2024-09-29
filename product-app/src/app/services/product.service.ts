import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root' 
})
export class ProductService {
  private baseUrl = 'http://localhost:5099/api/Product/';

  constructor(private http: HttpClient) {}

  getProducts(): Observable<any> {
    return this.http.get(this.baseUrl);
  }

  getProduct(id: number): Observable<any> {
    return this.http.get(`${this.baseUrl}${id}`);
  }

  createProduct(product: any): Observable<any> {
    return this.http.post(this.baseUrl, product);
  }

  updateProduct(product: any): Observable<any> {
    return this.http.put(`${this.baseUrl}${product.id}`, product);
  }

  deleteProduct(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}${id}`);
  }
}
