export const b2cPolicies = {
     names: {
         signUpSignIn: "b2c_1_susi",
         editProfile: "b2c_1_edit_profile"
     },
     authorities: {
         signUpSignIn: {
             authority: "https://adrianilewicz.b2clogin.com/adrianilewicz.onmicrosoft.com/b2c_1_susi",
         },
         editProfile: {
             authority: "https://adrianilewicz.b2clogin.com/adrianilewiczonmicrosoft.com/b2c_1_edit_profile"
         }
     },
     authorityDomain: "adrianilewicz.b2clogin.com"
 };
 
 
export const msalConfig: Configuration = {
     auth: {
         clientId: '68cfea04-39b2-47ca-83a8-d9241a2d8d29',
         authority: b2cPolicies.authorities.signUpSignIn.authority,
         knownAuthorities: [b2cPolicies.authorityDomain],
         redirectUri: '/', 
     },
    // More configuration here
 }

export const protectedResources = {
  todoListApi: {
    endpoint: "https://localhost:5001/api/races",
    scopes: ["https://adrianilewicz.onmicrosoft.com/api/races.read"],
  },
}