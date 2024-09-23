import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CurrencyPipe } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatDivider } from '@angular/material/divider';
import { ShopService } from '../../../core/services/shop.service';
import { Product } from '../../../shared/models/product';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [
    CurrencyPipe,
    MatButton,
    MatIcon,
    MatFormField,
    MatInput,
    MatLabel,
    MatDivider
  ],
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.scss'
})
export class ProductDetailComponent implements OnInit {
  private shopService = inject(ShopService);
  private activatedRoute = inject(ActivatedRoute);
  product?: Product;

  ngOnInit(): void {
    this.getProduct();
  }

  getProduct() {
    const productId = this.activatedRoute.snapshot.paramMap.get('id');
    if (!productId) {
      return;
    }
    this.shopService.getProduct(+productId).subscribe({
      next: (product: Product) => {
        this.product = product;
      },
      error: (error: any) => {
        console.error(error);
      }
    });
  }
}
