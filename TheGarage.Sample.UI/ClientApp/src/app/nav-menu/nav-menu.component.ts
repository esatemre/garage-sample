import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from '../_services';
import { Token } from '../_models';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  currentToken: Token;

  constructor(private router: Router, private authService: AuthService) {
    this.authService.currentToken.subscribe(x => this.currentToken = x);
  }

  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
