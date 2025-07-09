import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login-callback',
  template: '<p>Logging in...</p>'
})
export class LoginCallbackComponent implements OnInit {
  constructor(private auth: AuthService, private router: Router) {}

  async ngOnInit() {
    await this.auth.completeLogin();
    this.router.navigate(['/']);
  }
}
