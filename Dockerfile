#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

# Base Image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build Image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["knowbe4_api/knowbe4_api.csproj", "knowbe4_api/"]
RUN dotnet restore "knowbe4_api/knowbe4_api.csproj"
COPY . .
WORKDIR "/src/knowbe4_api"
RUN dotnet build "knowbe4_api.csproj" -c Release -o /app/build

# Publish Image
FROM build AS publish
RUN dotnet publish "knowbe4_api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final Image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Add your SSL certificate
FROM mcr.microsoft.com/windows/servercore:ltsc2019

WORKDIR /app

# Copie tous les fichiers .crt du répertoire de travail local vers le répertoire de travail du conteneur
COPY ./certificates/*.crt ./

# Installe chaque certificat .crt
RUN powershell -Command \
    Get-ChildItem -Path ./ -Filter *.crt | ForEach-Object { \
        $cert = New-Object System.Security.Cryptography.X509Certificates.X509Certificate2($_.FullName); \
        $store = New-Object System.Security.Cryptography.X509Certificates.X509Store('Root', 'LocalMachine'); \
        $store.Open('ReadWrite'); \
        $store.Add($cert); \
        $store.Close(); \
    }

# Entry Point
ENTRYPOINT ["dotnet", "knowbe4_api.dll"]
