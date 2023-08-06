FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/nightly/sdk:6.0-bullseye-slim-amd64 AS copy
WORKDIR /src
COPY ["CppLauncherService", "CppLauncherService/"]
COPY ["SharedProjects", "SharedProjects/"]

FROM copy AS publish
WORKDIR "/src/CppLauncherService/CppLauncherService"
RUN dotnet publish "CppLauncherService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CppLauncherService.dll"]

FROM final AS gpp-install
RUN apt-get update
RUN apt-get -y install g++
