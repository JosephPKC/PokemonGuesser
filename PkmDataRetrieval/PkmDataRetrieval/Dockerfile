FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copy files and build project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PkmDataRetrieval/PkmDataRetrieval/PkmDataRetrieval.csproj", "."]
RUN dotnet restore "./PkmDataRetrieval.csproj"
COPY ./PkmDataRetrieval/PkmDataRetrieval/ .
WORKDIR "/src/."
RUN dotnet build "./PkmDataRetrieval.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the build
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PkmDataRetrieval.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Set entrypoint
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PkmDataRetrieval_Service.dll"]