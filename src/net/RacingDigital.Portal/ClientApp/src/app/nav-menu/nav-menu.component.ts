import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  userName: string | null = null;
  isLoggedIn = false;

  constructor(private auth: AuthService) { }

  async ngOnInit() {
    const user = await this.auth.getUser();
    this.isLoggedIn = !!user && !user.expired;
    this.userName = this.isLoggedIn ? (user?.profile?.name as string ?? '') : null;
  }

  async logout() {
    await this.auth.logoutLocal();
    this.isLoggedIn = false;
    this.userName = null;
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
