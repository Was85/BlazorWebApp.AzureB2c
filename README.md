# BlazorWebApp.AzureB2c

This project is a Blazor Web application integrated with Azure AD B2C for authentication.

## Features

- Blazor WebAssembly front-end
- Azure AD B2C authentication
- Basic example of protected routes

## Prerequisites

- .NET SDK 6.0 or later
- Azure AD B2C tenant

  ## Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/Was85/BlazorWebApp.AzureB2c.git
   cd BlazorWebApp.AzureB2c

2. Configure Azure AD B2C:

Register a new application in your Azure AD B2C tenant.
Set the redirect URI to https://localhost:5001/authentication/login-callback.
Note the Application (client) ID and Tenant ID.

3. Update your-Settings.json with your Azure AD B2C configuration:
  ```json
  {
    "AzureAdB2C": {
      "Authority": "https://<tenant>.b2clogin.com/tfp/<tenant>.onmicrosoft.com/<policy>",
      "ClientId": "<client-id>",
      "ValidateAuthority": true
    }
  }
 ```

## Usage
The home page is accessible to everyone.
The Fetch Data page requires authentication.
