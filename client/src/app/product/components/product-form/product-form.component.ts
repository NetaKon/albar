import {
  Component,
  EventEmitter,
  Input,
  Output,
  SimpleChanges,
} from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  NonNullableFormBuilder,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Product } from '../../models/product';
import { CommonModule } from '@angular/common';
import { Category } from '../../models/category';

@Component({
  selector: 'product-form',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './product-form.component.html',
  styleUrl: './product-form.component.css',
})
export class ProductFormComponent {
  @Input() product: Product | null = null; // Product to edit (null for adding)
  @Input() categories: Category[] = [];
  @Output() saveProductEvent = new EventEmitter<Product>(); // Emits new or updated product
  @Output() closeModalEvent = new EventEmitter<void>(); // Closes the modal

  productForm: FormGroup;

  constructor(private fb: NonNullableFormBuilder) {
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      categoryId: [1, Validators.required],
      price: [100, [Validators.required, Validators.min(0.01)]],
      unitsInStock: [
        100,
        [Validators.required, Validators.min(0), Validators.pattern(/^\d+$/)],
      ],
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['product'] && this.product) {
      this.productForm.patchValue({
        name: this.product.name,
        categoryId: this.product.category?.id ?? null,
        price: this.product.price,
        unitsInStock: this.product.unitsInStock,
      });
    } else {
      this.productForm.reset();
    }
  }

  saveProduct() {
    if (this.productForm.valid) {
      const formValue = this.productForm.value;

      const selectedCategory = this.categories.find(
        (c) => c.id == formValue.categoryId
      );

      const productToSave: Product = {
        id: this.product?.id ?? 0, // Keep existing ID for updates
        name: formValue.name,
        category: selectedCategory!,
        price: formValue.price,
        unitsInStock: formValue.unitsInStock,
      };

      this.saveProductEvent.emit(productToSave);
      this.closeModal();
    }
  }

  closeModal() {
    this.closeModalEvent.emit(); // Emit event to close modal
  }
}
