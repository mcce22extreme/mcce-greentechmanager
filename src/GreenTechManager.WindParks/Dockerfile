FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY ["src/GreenTechManager.WindParks/.", "GreenTechManager.WindParks"]
COPY ["src/GreenTechManager.Core/.", "GreenTechManager.Core"]

RUN dotnet restore "GreenTechManager.WindParks/GreenTechManager.WindParks.csproj"

RUN dotnet publish "GreenTechManager.WindParks/GreenTechManager.WindParks.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "GreenTechManager.WindParks.dll"]