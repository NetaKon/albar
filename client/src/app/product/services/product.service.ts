import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { Product } from '../models/product';
import { Category } from '../models/category';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private baseUrl = 'http://localhost:5242/api';
  private productsUrl = `${this.baseUrl}/products`;
  private categoriesUrl = `${this.baseUrl}/categories`;

  constructor(private http: HttpClient) {}

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.productsUrl, {
      withCredentials: true,
    });
  }

  getCategories(): Observable<Category[]> {
    return this.http.get<Product[]>(this.categoriesUrl, {
      withCredentials: true,
    });
  }

  updateProduct(product: Product): Observable<Product> {
    return this.http
      .put<void>(`${this.productsUrl}/${product.id}`, product, {
        withCredentials: true,
      })
      .pipe(map(() => product));
  }

  addProduct(product: Product): Observable<Product> {
    const productToAdd = {
      name: product.name,
      price: product.price,
      unitsInStock: product.unitsInStock,
      categoryId: product.category.id,
    };
    return this.http.post<Product>(this.productsUrl, productToAdd, {
      withCredentials: true,
    });
  }
}
