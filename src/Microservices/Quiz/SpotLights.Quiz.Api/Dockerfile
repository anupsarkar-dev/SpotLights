#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Microservices/Quiz/SpotLights.Quiz.Api/SpotLights.Quiz.Api.csproj", "src/Microservices/Quiz/SpotLights.Quiz.Api/"]
COPY ["src/Microservices/Quiz/SpotLights.Quiz.Core/SpotLights.Quiz.Core.csproj", "src/Microservices/Quiz/SpotLights.Quiz.Core/"]
COPY ["src/Microservices/Quiz/SpotLights.Quiz.Infrastructure/SpotLights.Quiz.Infrastructure.csproj", "src/Microservices/Quiz/SpotLights.Quiz.Infrastructure/"]
COPY ["src/Microservices/Quiz/SpotLights.Quiz.Domain/SpotLights.Quiz.Domain.csproj", "src/Microservices/Quiz/SpotLights.Quiz.Domain/"]
COPY ["src/Common.Library/Common.Library/SpotLights.Common.Library.csproj", "src/Common.Library/Common.Library/"]
RUN dotnet restore "./src/Microservices/Quiz/SpotLights.Quiz.Api/SpotLights.Quiz.Api.csproj"
COPY . .
WORKDIR "/src/src/Microservices/Quiz/SpotLights.Quiz.Api"
RUN dotnet build "./SpotLights.Quiz.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SpotLights.Quiz.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SpotLights.Quiz.Api.dll"]
