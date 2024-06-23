#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Microservices/Course/SpotLights.Course.Api/SpotLights.Course.Api.csproj", "src/Microservices/Course/SpotLights.Course.Api/"]
COPY ["src/Microservices/Course/SpotLights.Course.Core/SpotLights.Course.Core.csproj", "src/Microservices/Course/SpotLights.Course.Core/"]
COPY ["src/Microservices/Course/SpotLights.Course.Infrastructure/SpotLights.Course.Infrastructure.csproj", "src/Microservices/Course/SpotLights.Course.Infrastructure/"]
COPY ["src/Microservices/Course/SpotLights.Course.Domain/SpotLights.Course.Domain.csproj", "src/Microservices/Course/SpotLights.Course.Domain/"]
COPY ["src/Common.Library/Common.Library/SpotLights.Common.Library.csproj", "src/Common.Library/Common.Library/"]
RUN dotnet restore "./src/Microservices/Course/SpotLights.Course.Api/SpotLights.Course.Api.csproj"
COPY . .
WORKDIR "/src/src/Microservices/Course/SpotLights.Course.Api"
RUN dotnet build "./SpotLights.Course.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SpotLights.Course.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpotLights.Course.Api.dll"]
