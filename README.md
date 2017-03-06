# DexterityAPI

#### Built with Microsoft.EntityFrameworkCore.SqlServer.1.1.0 targeting .NET Framework 4.6.1

The database will eventually be maintained via Entity Framework migrations but right now the structure is churning whilst I'm figuring out
what I need and where I'm putting stuff. So I'm generally just tearing it down and recreating it until I like it.

If you want to build it yourself there is a handy **DexterityDB_BuildScript.sql** you can run against an existing empty SQL Server database.
In theory, creating an EF migration in the DexterityAPI.Data project should achieve the same result but it's not guaranteed just yet.

By default, **DexterityAPI.ConsoleHost/App.config** is configured to connect to a database called **DexterityDB** on the Visual Studio
localdb SQL Server instance:
```xml
<add name="DexterityDB" connectionString="Server = (localdb)\mssqllocaldb; Database = DexterityDB; Trusted_Connection = True;" />
```

*Have a nose around but don't expect much to be working.. Move along.. Nothing to see here yet. :)*

*Just adding projects, general code structure and domain classes so far.*