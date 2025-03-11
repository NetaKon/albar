import { Component } from '@angular/core';
import { ProductModule } from './product/product.module';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
  imports: [ProductModule],
})
export class AppComponent {}
