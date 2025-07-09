export const environment = {
  production: true,
  b2c: {
    tenant: '<your-tenant>',
    clientId: '<portal-client-id>',
    policy: 'B2C_1_signupsignin',
    apiScope: 'https://<your-tenant>.onmicrosoft.com/api/user_impersonation'
  }
};
