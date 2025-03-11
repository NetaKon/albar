import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { ProductListComponent } from '../../components/product-list/product-list.component';
import { Product } from '../../models/product';
import { ProductFilterComponent } from '../../components/product-filter/product-filter.component';
import { ProductFormComponent } from '../../components/product-form/product-form.component';
import { NgIf } from '@angular/common';
import { Category } from '../../models/category';

@Component({
  selector: 'products',
  imports: [
    NgIf,
    ProductListComponent,
    ProductFilterComponent,
    ProductFormComponent,
  ],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css',
})
export class ProductsComponent implements OnInit {
  products: Product[] = [];
  categories: Category[] = [];

  filter: any = () => true;
  editingProduct: Product | null = null;
  isModalOpen = false;

  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.loadProducts();
    this.loadCategories();
  }

  loadProducts() {
    this.productService.getProducts().subscribe({
      next: (data) => (this.products = data),
      error: (error) => console.error('Error loading products:', error),
    });
  }

  loadCategories() {
    this.productService.getCategories().subscribe({
      next: (data) => (this.categories = data),
      error: (error) => console.error('Error loading products:', error),
    });
  }

  openModal(product?: Product) {
    this.editingProduct = product ? { ...product } : null;
    this.isModalOpen = true;
  }

  saveProduct(updatedProduct: Product) {
    if (this.editingProduct) {
      this.productService
        .updateProduct(updatedProduct)
        .subscribe((returnedProduct) => {
          console.log(returnedProduct);
          this.products = this.products.map((p) =>
            p.id === updatedProduct.id ? returnedProduct : p
          );
          this.closeModal();
        });
    } else {
      this.productService.addProduct(updatedProduct).subscribe((newProduct) => {
        this.products.push(newProduct);
        this.closeModal();
      });
    }
  }

  closeModal() {
    this.isModalOpen = false;
    this.editingProduct = null;
  }
}
