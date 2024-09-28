import { Component, inject, input } from '@angular/core';
import { CartItem } from '../../../shared/models/cart';
import { RouterLink } from '@angular/router';
import { CurrencyPipe } from '@angular/common';
import { CartService } from '../../../core/services/cart.service';

@Component({
  selector: 'app-cart-item',
  standalone: true,
  imports: [RouterLink, CurrencyPipe],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.scss'
})
export class CartItemComponent {
  item = input.required<CartItem>();
  cartService = inject(CartService);

  increaseQuantity() {
    this.cartService.addItemToCart(this.item());
  }

  decreaseQuantity() {
    this.cartService.removeItemFromCart(this.item().productId);
  }

  removeItem() {
    this.cartService.removeItemFromCart(this.item().productId, this.item().quantity);
  }
}
