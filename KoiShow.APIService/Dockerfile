# ========== BUILD STAGE ==========
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the entire solution
COPY . .

# Restore using the correct project path
RUN dotnet restore KoiShow.APIService/KoiShow.APIService.csproj

# Publish the application
RUN dotnet publish KoiShow.APIService/KoiShow.APIService.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# ========== RUNTIME STAGE ==========
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "KoiShow.APIService.dll"]
