import { Routes } from '@angular/router';
import { ShopComponent } from './features/shop/shop.component';
import { HomeComponent } from './features/home/home.component';
import { ProductDetailComponent } from './features/shop/product-detail/product-detail.component';
import { CartComponent } from './features/cart/cart.component';
import { CheckoutComponent } from './features/checkout/checkout.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'shop', component: ShopComponent },
    { path: 'shop/:id', component: ProductDetailComponent },
    { path: 'cart', component: CartComponent },
    { path: 'checkout', component: CheckoutComponent },
    { path: '**', redirectTo: '', pathMatch: 'full' }
];
