FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY ["src/GreenTechManager.ApiGateway/.", "GreenTechManager.ApiGateway"]

RUN dotnet restore "GreenTechManager.ApiGateway/GreenTechManager.ApiGateway.csproj"

RUN dotnet publish "GreenTechManager.ApiGateway/GreenTechManager.ApiGateway.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "GreenTechManager.ApiGateway.dll"]