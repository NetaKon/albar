import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Product } from '../../models/product';
import { Category } from '../../models/category';

const generateFilter = (category?: string, name?: string) => {
  return (product: Product) => {
    if (category && category !== 'All' && product.category.name !== category) {
      return false;
    }

    if (name && !product.name.toLowerCase().includes(name.toLowerCase())) {
      return false;
    }

    return true;
  };
};

@Component({
  selector: 'product-filter',
  imports: [CommonModule, FormsModule],
  templateUrl: './product-filter.component.html',
  styleUrl: './product-filter.component.css',
})
export class ProductFilterComponent {
  @Input() categories: Category[] = [];

  @Input() filter!: (product: Product) => boolean;
  @Output() filterChange = new EventEmitter<(product: Product) => boolean>();

  selectedCategory: string = 'All';
  searchText: string = '';

  updateFilter() {
    this.filter = generateFilter(this.selectedCategory, this.searchText);
    this.filterChange.emit(this.filter);
  }
}
