FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
ARG EXPOSE_PORT

USER $APP_UID

WORKDIR /app

EXPOSE "$EXPOSE_PORT"

# Copy files and build project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG PROJ_NAME
ARG FULL_PROJ_PATH=./"$PROJ_NAME"/"$PROJ_NAME"/"$PROJ_NAME".csproj
ARG DIR_PROJ_PATH=./"$PROJ_NAME"/"$PROJ_NAME"/
ENV BUILD_CONFIG=Release
ENV REL_PROJ_PATH=./"$PROJ_NAME".csproj

WORKDIR /src

COPY "$FULL_PROJ_PATH" .

RUN dotnet restore $"REL_PROJ_PATH"

COPY "$DIR_PROJ_PATH" .

WORKDIR /src/.

RUN dotnet build "$REL_PROJ_PATH" -c "$BUILD_CONFIG" -o /app/build

# Publish the build
FROM build AS publish
ARG BUILD_CONFIG=Release

RUN dotnet publish "$REL_PROJ_PATH" -c "$BUILD_CONFIG" -o /app/publish /p:UseAppHost=false

# Set entrypoint
FROM base AS final
ARG DLL_NAME

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "$DLL_NAME"]