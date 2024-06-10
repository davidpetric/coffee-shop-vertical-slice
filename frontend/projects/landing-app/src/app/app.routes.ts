import { Routes } from '@angular/router';
import { AboutUsComponent } from './pages/about-us/about-us.component';
import { HomeComponent } from './pages/home/home.component';
import { ProductsComponent } from './pages/products/products.component';
import { DeliveryComponent } from './pages/delivery/delivery.component';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'about-us',
    component: AboutUsComponent,
  },
  {
    path: 'products',
    component: ProductsComponent,
  },

  {
    path: 'delivery',
    component: DeliveryComponent,
  },
  {
    path: '**',
    redirectTo: '/',
  },
];
