import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AuthService } from './_services';
import { Token } from './_models';

@Component({ selector: 'app-root', templateUrl: 'app.component.html', styleUrls: ['./app.component.css'] })
export class AppComponent {
  title = 'app';
  currentToken: Token;

  constructor(
    private router: Router,
    private authenticationService: AuthService
  ) {
    this.authenticationService.currentToken.subscribe(x => this.currentToken = x);
  }

  logout() {
    this.authenticationService.logout();
    this.router.navigate(['/login']);
  }
}
