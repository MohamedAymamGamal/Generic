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

# ── Dev environment flags ────────────────────────────────────────
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:8080
ENV DOTNET_USE_POLLING_FILE_WATCHER=true    
ENV DOTNET_WATCH_RESTART_ON_RUDE_EDIT=true  

# Keep WORKDIR at the base root so VS Code maps it seamlessly
WORKDIR /src

EXPOSE 8080

# Execute using explicit bash array syntax and point directly to the project file
CMD ["/bin/bash", "-c", "dotnet restore Ecom.slnx && dotnet watch run --project Ecom.Api/Ecom.Api.csproj --no-launch-profile"]