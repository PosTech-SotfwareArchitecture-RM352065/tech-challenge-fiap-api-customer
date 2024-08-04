#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/azure-functions/dotnet-isolated:4-dotnet-isolated8.0 AS base
WORKDIR /home/site/wwwroot
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Sanduba.Cloud.Funtion/Sanduba.Cloud.Funtion.csproj", "Sanduba.Cloud.Funtion/"]
COPY ["Sanduba.Adapter.Mvc/Sanduba.Adapter.Mvc.csproj", "Sanduba.Adapter.Mvc/"]
COPY ["Sanduba.Core.Application.Abstraction/Sanduba.Core.Application.Abstraction.csproj", "Sanduba.Core.Application.Abstraction/"]
COPY ["Sanduba.Core.Domain/Sanduba.Core.Domain.csproj", "Sanduba.Core.Domain/"]
COPY ["Sanduba.Core.Application/Sanduba.Core.Application.csproj", "Sanduba.Core.Application/"]
COPY ["Sanduba.Infrastructure.Persistence.SqlServer/Sanduba.Infrastructure.Persistence.SqlServer.csproj", "Sanduba.Infrastructure.Persistence.SqlServer/"]
COPY ["Sanduba.Infrastructure.Broker.ServiceBus/Sanduba.Infrastructure.Persistence.SqlServer.csproj", "Sanduba.Infrastructure.Persistence.SqlServer/"]
RUN dotnet restore "./Sanduba.Cloud.Funtion/Sanduba.Cloud.Funtion.csproj"
COPY . .
WORKDIR "/src/Sanduba.Cloud.Funtion"
RUN dotnet build "./Sanduba.Cloud.Funtion.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Sanduba.Cloud.Funtion.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /home/site/wwwroot
COPY --from=publish /app/publish .
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true