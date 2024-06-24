#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 4000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Gateway/SpotLights.Routing.Gateway/SpotLights.Routing.Gateway.csproj", "src/Gateway/SpotLights.Routing.Gateway/"]
RUN dotnet restore "./src/Gateway/SpotLights.Routing.Gateway/SpotLights.Routing.Gateway.csproj"
COPY . .
WORKDIR "/src/src/Gateway/SpotLights.Routing.Gateway"
RUN dotnet build "./SpotLights.Routing.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SpotLights.Routing.Gateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpotLights.Routing.Gateway.dll"]
