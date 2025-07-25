﻿FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Books.API/Books.API.csproj", "Books.API/"]
COPY ["Books.Application/Books.Application.csproj", "Books.Application/"]
COPY ["Books.Contracts/Books.Contracts.csproj", "Books.Contracts/"]
RUN dotnet restore "Books.API/Books.API.csproj"
COPY . .
WORKDIR "/src/Books.API"
RUN dotnet build "./Books.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Books.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Books.API.dll"]
