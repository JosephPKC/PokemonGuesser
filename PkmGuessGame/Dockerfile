FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8081

# Copy files and build project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ./PkmGuessGame/PkmGuessGame/PkmGuessGame.csproj .
RUN dotnet restore ./PkmGuessGame.csproj
COPY ./PkmGuessGame/PkmGuessGame/ .
WORKDIR /src/.
RUN dotnet build ./PkmGuessGame.csproj -c "$BUILD_CONFIGURATION" -o /app/build

# Publish the build
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish ./PkmGuessGame.csproj -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

# Set entrypoint
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PkmGuessGame_Service.dll"]