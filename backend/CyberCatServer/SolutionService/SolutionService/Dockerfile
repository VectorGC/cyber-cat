FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/nightly/sdk:6.0-bullseye-slim-amd64 AS copy
WORKDIR /src
COPY ["SolutionService", "SolutionService/"]
COPY ["SharedProjects", "SharedProjects/"]

FROM copy AS publish
WORKDIR "/src/SolutionService/SolutionService"
RUN dotnet publish "SolutionService.csproj" -c Release -o /app/publish -v d -r linux-x64

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SolutionService.dll"]
