# Racing Digital

This repository contains an ASP.NET and Angular based project.

## Azure AD B2C

Both the API and the portal are configured to authenticate using Azure AD B2C. Configure the B2C settings in `appsettings.json` for each project and in the Angular environment files.

- `Instance` – B2C login URL
- `Domain` – tenant domain (e.g. `contoso.onmicrosoft.com`)
- `ClientId` – application ID registered in your tenant
- `SignUpSignInPolicyId` – policy used for sign‑up and sign‑in

The Angular client reads the same settings from `environment.ts`.
