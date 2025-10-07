# ===== build =====
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# copy csproj files first (faster restore)
COPY KQRegistrationWeb/*.csproj KQRegistrationWeb/
COPY KQGeneral/*.csproj       KQGeneral/

RUN dotnet restore KQRegistrationWeb/KQRegistrationWeb.csproj

# copy the rest and publish
COPY . .
WORKDIR /src/KQRegistrationWeb
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# ===== runtime =====
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
EXPOSE 8081
ENV ASPNETCORE_URLS=http://+:8081
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "KQRegistrationWeb.dll"]
