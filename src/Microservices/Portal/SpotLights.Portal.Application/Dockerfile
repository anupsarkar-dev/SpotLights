# Use .NET 8.0 SDK with Alpine for the build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

# Install npm in a separate step to handle errors better
RUN apk add --no-cache nodejs npm

ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files and restore as distinct layers
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

# Copy the rest of the source code
COPY . .

# Run npm install and build in the SpotLights.Admin project
WORKDIR "./src/Microservices/Portal/SpotLights.Portal.Admin/assets"
RUN npm install
RUN npm run build:Release

# Build the SpotLights.Portal.Application project

# RUN dotnet build "./src/Microservices/Portal/SpotLights.Portal.Application/SpotLights.Portal.Application.csproj" -c $BUILD_CONFIGURATION -o /app/build
 
WORKDIR "/src/src/Microservices/Portal/SpotLights.Portal.Application"
RUN dotnet build "./SpotLights.Portal.Application.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SpotLights.Portal.Application.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Use .NET 8.0 ASP.NET Core runtime with Alpine for the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final

# Install ICU libraries for globalization support
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "SpotLights.Portal.Application.dll"]
