# ===== Build stage =====
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy only project files first (faster restore)
COPY KQRegistration/*.csproj KQRegistration/
COPY KQGeneral/*.csproj KQGeneral/

# If your API csproj has a different name (e.g., KQApi.csproj),
# change the path below accordingly.
RUN dotnet restore KQRegistration/KQApi.csproj

# Now copy the rest of the source
COPY . .

# Publish the API
WORKDIR /src/KQRegistration
RUN dotnet publish KQApi.csproj -c Release -o /app/publish /p:UseAppHost=false
# ===== Runtime stage =====
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .

# If your project name is not KQRegistration, replace the DLL name to match the *.csproj
ENTRYPOINT ["dotnet", "KQApi.dll"]