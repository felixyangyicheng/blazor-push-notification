#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["ServiceMonitor/Server/ServiceMonitor.Server.csproj", "ServiceMonitor/Server/"]
COPY ["ServiceMonitor/Client/ServiceMonitor.Client.csproj", "ServiceMonitor/Client/"]
COPY ["ServiceMonitor/Shared/ServiceMonitor.Shared.csproj", "ServiceMonitor/Shared/"]
RUN dotnet restore "./ServiceMonitor/Server/ServiceMonitor.Server.csproj"
COPY . .
WORKDIR "/src/ServiceMonitor/Server"
RUN dotnet build "./ServiceMonitor.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "./ServiceMonitor.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceMonitor.Server.dll"]