import { Component } from '@angular/core';
import { AuthLibComponent } from '../../../../../auth-lib/src/public-api';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [AuthLibComponent],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.css',
})
export class AuthComponent {}
