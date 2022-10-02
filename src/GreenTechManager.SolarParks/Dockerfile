FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY ["src/GreenTechManager.SolarParks/.", "GreenTechManager.SolarParks"]
COPY ["src/GreenTechManager.Core/.", "GreenTechManager.Core"]

RUN dotnet restore "GreenTechManager.SolarParks/GreenTechManager.SolarParks.csproj"

RUN dotnet publish "GreenTechManager.SolarParks/GreenTechManager.SolarParks.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "GreenTechManager.SolarParks.dll"]