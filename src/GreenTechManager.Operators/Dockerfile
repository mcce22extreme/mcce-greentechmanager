FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY ["src/GreenTechManager.Operators/.", "GreenTechManager.Operators"]
COPY ["src/GreenTechManager.Core/.", "GreenTechManager.Core"]

RUN dotnet restore "GreenTechManager.Operators/GreenTechManager.Operators.csproj"

RUN dotnet publish "GreenTechManager.Operators/GreenTechManager.Operators.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "GreenTechManager.Operators.dll"]