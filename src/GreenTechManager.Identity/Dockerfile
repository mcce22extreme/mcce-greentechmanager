FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY ["src/GreenTechManager.Identity/.", "GreenTechManager.Identity"]
COPY ["src/GreenTechManager.Core/.", "GreenTechManager.Core"]

RUN dotnet restore "GreenTechManager.Identity/GreenTechManager.Identity.csproj"

RUN dotnet publish "GreenTechManager.Identity/GreenTechManager.Identity.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "GreenTechManager.Identity.dll"]