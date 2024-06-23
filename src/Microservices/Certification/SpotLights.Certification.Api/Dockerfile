#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5003

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Microservices/Certification/SpotLights.Certification.Api/SpotLights.Certification.Api.csproj", "src/Microservices/Certification/SpotLights.Certification.Api/"]
COPY ["src/Microservices/Certification/SpotLights.Certification.Core/SpotLights.Certification.Core.csproj", "src/Microservices/Certification/SpotLights.Certification.Core/"]
COPY ["src/Microservices/Certification/SpotLights.Certification.Infrastructure/SpotLights.Certification.Infrastructure.csproj", "src/Microservices/Certification/SpotLights.Certification.Infrastructure/"]
COPY ["src/Microservices/Certification/SpotLights.Certification.Domain/SpotLights.Certification.Domain.csproj", "src/Microservices/Certification/SpotLights.Certification.Domain/"]
COPY ["src/Common.Library/Common.Library/SpotLights.Common.Library.csproj", "src/Common.Library/Common.Library/"]
RUN dotnet restore "./src/Microservices/Certification/SpotLights.Certification.Api/SpotLights.Certification.Api.csproj"
COPY . .
WORKDIR "/src/src/Microservices/Certification/SpotLights.Certification.Api"
RUN dotnet build "./SpotLights.Certification.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SpotLights.Certification.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpotLights.Certification.Api.dll"]
