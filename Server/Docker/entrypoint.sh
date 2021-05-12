#!/bin/bash

# Replace credentials for the database in appsettings.json
sed -i "s/server/$DB_SERVER/" appsettings.json
sed -i "s/port/$DB_PORT/" appsettings.json
sed -i "s/database/$DB_NAME/" appsettings.json
sed -i "s/username/$DB_USER/" appsettings.json
sed -i "s/password/$DB_PASSWORD/" appsettings.json

# Start program
dotnet Server.dll