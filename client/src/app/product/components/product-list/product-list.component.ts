import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Product } from '../../models/product';
import { ProductItemComponent } from '../product-item/product-item.component';
import { NgFor } from '@angular/common';

@Component({
  selector: 'product-list',
  imports: [ProductItemComponent, NgFor],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css',
})
export class ProductListComponent {
  @Input() products: Product[] = [];

  @Output() editProductEvent = new EventEmitter<Product>();

  editProduct(product: Product) {
    this.editProductEvent.emit(product);
  }
}
