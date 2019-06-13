# DotNetClientSqlServerAPI
MyLifeline .Net Client API that will write Device traffic to a SQL database.

## Introduction
This API only have a single endpoint api/log that will be used with the MyLifeline site software to push traffic from MyLifeline to your SQL database. See below Testing for instructions to test the endpoint.

## Database
In your MS SQL Server, create a new blank database. The application will create the necessary structure.

## Environment Variables
Two environment variables are required:
1. MLLTOKEN
2. MLLSQL

MLLTOKEN: A SHA-1 key used for authorization can be generated here [Generate a SHA1 key](https://passwordsgenerator.net/sha1-hash-generator/).
MLLSQL: The connection string to your SQL Server instance.

## Installation

### IIS and Windows Server

1. Install [.Net Core Hosting Bundle](https://www.microsoft.com/net/permalink/dotnetcore-current-windows-runtime-bundle-installer)
2. Create a folder under C:\inetpub\wwwroot
3. Copy the Release from the Release\{version} folder to wwwroot folder.
4. Create a new Site in IIS
5. Set the location to the folder in wwwroot you created in point 2.
6. Right click on the App Pool for this new Site and set .Net CLR Version to No Managed Code.
7. Do an iisreset.

### Nginx and Linux
This application can also be run on Linux. Follow these [instructions](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-2.2) from Microsoft.

### Docker
This is also available on [Docker Hub](https://hub.docker.com/r/mylifelineio/clientsqlapi)
```
docker pull mylifelineio/clientsqlapi
```

## Testing
You will be required to use a tool like [Postman](https://www.getpostman.com/), [Fiddler](https://www.telerik.com/fiddler) or [CURL](https://curl.haxx.se/) in order to send a call to the endpoint.

### Send
Type: POST \
Address: http://yourserver:yourport/api/log

### Header
```
content-type:application/json
Authorization: Bearer yoursha1tokengenerated
```
### Body
DeviceID must be a minimum of 11 characters, maximum 17.
```
{"DeviceID":"123456789012"}
```

## Setup in MyLifeline

1. Go to your site https://yoursubdomain.mylifeline.io
2. Go to Admin > API
3. Click on Config under Your API
4. Enter the SHA-1 key generated in Authorization Header
5. Enter Address like http://yourserver:yourport/api/log
6. Choose the traffic options you would like to receive.
7. Save
8. Ensure you have traffic by going to the Monitor page.
9. Check your Database table DeviceLog for records.

