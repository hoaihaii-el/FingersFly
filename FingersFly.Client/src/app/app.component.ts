import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { ShopComponent } from "./features/shop/shop.component";
import { ShopService } from './core/services/shop.service';
import { Product } from './shared/models/product';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent, ShopComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  title = 'FingersFly.Client';
  private shopService = inject(ShopService);
  private products: Product[] = [];

  ngOnInit(): void {
    this.shopService.getProducts().subscribe({
      next: res => this.products = res,
      error: err => console.log(err)
    })
  }
}
