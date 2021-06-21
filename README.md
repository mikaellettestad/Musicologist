# Musicologist 1.0

### Requirements
* Visual Studio (ASP .NET Core 3.1)
* SQL Management Studio
* Microsoft SQL Server

### Setup (example courses will be available in next version)
1. Clone repository
2. Open solution
3. Add appsettings.json:

```javascript
{
  "ConnectionStrings": {
    "DefaultConnection": ""
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
```
4. Include your connection string
5. Run application
