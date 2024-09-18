import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:7135/api/'
  private http = inject(HttpClient);

  getProducts() {
    return this.http.get<any>(this.baseUrl + 'products');
  }
}
