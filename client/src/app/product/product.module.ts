import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsComponent } from './pages/products/products.component';
import { ProductItemComponent } from './components/product-item/product-item.component';
import { ProductListComponent } from './components/product-list/product-list.component';

@NgModule({
  imports: [
    CommonModule,
    ProductItemComponent,
    ProductListComponent,
    ProductsComponent,
  ],
  exports: [ProductsComponent],
})
export class ProductModule {}
