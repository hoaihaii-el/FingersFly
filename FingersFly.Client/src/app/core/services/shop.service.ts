import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ShopParams } from '../../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:7135/api/'
  private http = inject(HttpClient);

  types: string[] = [];
  brands: string[] = [];

  getProducts(shopParams: ShopParams) {
    let params = new HttpParams();

    if (shopParams.brands.length > 0) {
      params = params.append('brands', shopParams.brands.join(','));
    }

    if (shopParams.types.length > 0) {
      params = params.append('types', shopParams.types.join(','));
    }

    if (shopParams.sort) {
      if (shopParams.sort.includes('price')) {
        params = params.append('sortCol', 'price');
        params = params.append('sortType', shopParams.sort.includes('Asc') ? 'asc' : 'desc');
      }
      else {
        params = params.append('sortCol', 'name');
        params = params.append('sortType', 'asc');
      }
    }

    if (shopParams.search) {
      params = params.append('search', shopParams.search);
    }

    params = params.append('pageIndex', shopParams.pageIndex);
    params = params.append('pageSize', shopParams.pageSize);

    return this.http.get<any>(this.baseUrl + 'products', { params });
  }

  getBrands() {
    if (this.brands.length > 0) {
      return;
    }

    return this.http.get<string[]>(this.baseUrl + 'products/brands').subscribe({
      next: res => this.brands = res
    })
  }

  getTypes() {
    if (this.types.length > 0) {
      return;
    }

    return this.http.get<string[]>(this.baseUrl + 'products/types').subscribe({
      next: res => this.types = res
    })
  }
}
