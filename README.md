# Hotelie

A sample *WPF* application implemented based on *domain-driven* pattern and *MVVM* pattern on presentation layer.

## Business

A simple hotel management application with simple features:

- User management.
- Room management.
- Lease management.
- Bill management.
- Revenue/usage report.

Language: Vietnamese.

## Requirements

Runtime:

- Windows 8.1+.
- .NET Framework 4.0.
- Microsoft SQL Server 2014+.

Developments:

- Visual Studio 2017+.
- NuGet Package manager. 

## Installation

First, open solution in Visual Studio.

Then, open NuGet Package Console and run following command to install dependencies.

``` bash
Update-Package -reinstall
```

Now, you can proceed to rebuild solution.

Done! Now you can run the application. First, set project `Hotelie.Presentation` as *Startup Project* then run in either *Debug*/*Release* mode (F5 key shortcut in *Visual Studio*).

The application then fail to connect to SQL server. Now, open your SQL server management/command line tool and create new database, name it whatever you like.

Open application, switch to *Connection Settings* screen and set proper *server name* & *database name*.

Done!