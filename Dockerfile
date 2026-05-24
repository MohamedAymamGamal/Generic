
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine

# ── Alpine extras ────────────────────────────────────────────────
RUN apk add --no-cache \
    icu-libs \
    tzdata \
    curl \
    bash

# Required on Alpine — SQL Server & EF Core break without this
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV TZ=UTC

# ── EF Core CLI ──────────────────────────────────────────────────
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# ── EF Core CLI tool ─────────────────────────────────────────────
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# ── Dev environment flags ────────────────────────────────────────
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_USE_POLLING_FILE_WATCHER=true    
# ↑ Needed inside Docker — inotify doesn't always work in containers

ENV DOTNET_WATCH_RESTART_ON_RUDE_EDIT=true  
# ↑ Auto-restart even on "rude edits" (adding methods, changing signatures)

WORKDIR /src
# Source code is bind-mounted here from docker-compose — no COPY needed

EXPOSE 8080

# dotnet watch runs from the API project folder
WORKDIR /src/Ecom.API

# Restore on container start (picks up any new packages),
# then launch hot-reload dev server
CMD dotnet restore && dotnet watch run --no-launch-profile