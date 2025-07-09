import { Injectable } from '@angular/core';
import { UserManager, UserManagerSettings, User } from 'oidc-client';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private userManager: UserManager;
  private user: User | null = null;

  constructor() {
    const b2c = environment.b2c;
    const settings: UserManagerSettings = {
      authority: `https://${b2c.tenant}.b2clogin.com/${b2c.tenant}.onmicrosoft.com/${b2c.policy}/v2.0`,
      client_id: b2c.clientId,
      redirect_uri: `${window.location.origin}/login-callback`,
      response_type: 'code',
      scope: `openid profile ${b2c.apiScope}`,
      post_logout_redirect_uri: window.location.origin
    };

    this.userManager = new UserManager(settings);
  }

  login(): Promise<void> {
    return this.userManager.signinRedirect();
  }

  async completeLogin(): Promise<User> {
    const user = await this.userManager.signinRedirectCallback();
    this.user = user;
    return user;
  }

  async getUser(): Promise<User | null> {
    if (this.user) {
      return this.user;
    }
    this.user = await this.userManager.getUser();
    return this.user;
  }

  async isAuthenticated(): Promise<boolean> {
    const user = await this.getUser();
    return !!user && !user.expired;
  }

  async logout(): Promise<void> {
    this.user = null;
    return this.userManager.signoutRedirect();
  }

  async logoutLocal(): Promise<void> {
    await this.userManager.removeUser();
    this.user = null;
  }

  async getAccessToken(): Promise<string | null> {
    const user = await this.getUser();
    return user && !user.expired ? user.access_token : null;
  }
}
