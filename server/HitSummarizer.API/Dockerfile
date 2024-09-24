FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HitSummarizer.API/HitSummarizer.API.csproj", "HitSummarizer.API/"]
RUN dotnet restore "./HitSummarizer.API/HitSummarizer.API.csproj"
COPY . .
WORKDIR "/src/HitSummarizer.API"
RUN dotnet build "./HitSummarizer.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HitSummarizer.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HitSummarizer.API.dll"]