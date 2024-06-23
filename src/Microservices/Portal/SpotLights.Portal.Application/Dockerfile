#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Microservices/Portal/SpotLights.Portal.Application/SpotLights.Portal.Application.csproj", "src/Microservices/Portal/SpotLights.Portal.Application/"]
COPY ["src/Microservices/Portal/SpotLights.Portal.Admin/SpotLights.Admin.csproj", "src/Microservices/Portal/SpotLights.Portal.Admin/"]
COPY ["src/Microservices/Portal/SpotLights.Portal.Domain/SpotLights.Portal.Domain.csproj", "src/Microservices/Portal/SpotLights.Portal.Domain/"]
COPY ["src/Common.Library/Common.Library/SpotLights.Common.Library.csproj", "src/Common.Library/Common.Library/"]
COPY ["src/Microservices/Portal/SpotLights.Portal.Shared/SpotLights.Portal.Shared.csproj", "src/Microservices/Portal/SpotLights.Portal.Shared/"]
COPY ["src/Microservices/Portal/SpotLights.Portal.Core/SpotLights.Portal.Core.csproj", "src/Microservices/Portal/SpotLights.Portal.Core/"]
COPY ["src/Microservices/Portal/SpotLights.Portal.Infrastructure/SpotLights.Portal.Infrastructure.csproj", "src/Microservices/Portal/SpotLights.Portal.Infrastructure/"]
COPY ["src/Microservices/Portal/SpotLights.Portal.Data/SpotLights.Portal.Data.csproj", "src/Microservices/Portal/SpotLights.Portal.Data/"]
COPY ["src/Microservices/Portal/SpotLights.Portal.Themes.Standard/SpotLights.Themes.Standard.csproj", "src/Microservices/Portal/SpotLights.Portal.Themes.Standard/"]
RUN dotnet restore "./src/Microservices/Portal/SpotLights.Portal.Application/SpotLights.Portal.Application.csproj"
COPY . .
WORKDIR "/src/src/Microservices/Portal/SpotLights.Portal.Application"
RUN dotnet build "./SpotLights.Portal.Application.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SpotLights.Portal.Application.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpotLights.Portal.Application.dll"]
