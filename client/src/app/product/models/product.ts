import { Category } from './category';

export class Product {
  constructor(
    public id: number,
    public name: string,
    public category: Category,
    public price: number,
    public unitsInStock: number
  ) {}
}
