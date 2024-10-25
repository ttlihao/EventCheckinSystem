# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.


# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


FROM mcr.microsoft.com/mssql/server:2022-latest

# Switch to root to install tools
USER root

RUN apt-get update && \
    apt-get install -y curl apt-transport-https && \
    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y msodbcsql17 mssql-tools && \
    echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc && \
    source ~/.bashrc

# Switch back to mssql user
USER mssql

WORKDIR /var/opt/mssql

COPY ./sql-scripts /var/opt/mssql/scripts


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EventCheckinSystem.API/EventCheckinSystem.API.csproj", "EventCheckinSystem.API/"]
COPY ["EventCheckinSystem.Services/EventCheckinSystem.Services.csproj", "EventCheckinSystem.Services/"]
COPY ["EventCheckinSystem.Repo/EventCheckinSystem.Repo.csproj", "EventCheckinSystem.Repo/"]
RUN dotnet restore "./EventCheckinSystem.API/EventCheckinSystem.API.csproj"
COPY . .
WORKDIR "/src/EventCheckinSystem.API"
RUN dotnet build "./EventCheckinSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EventCheckinSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventCheckinSystem.API.dll"]