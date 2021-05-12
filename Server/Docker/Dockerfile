FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY Shook/Shook.sln .
COPY Shook/Server/Server.csproj ./Server/
COPY Shook/Model/Model.csproj ./Model/

RUN dotnet restore

# copy everything else and build app
COPY Shook/. ./Shook
WORKDIR /source/Shook
RUN dotnet publish -c Release -o /app


FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
COPY ./entrypoint.sh ./
ENTRYPOINT ["sh", "entrypoint.sh"]
