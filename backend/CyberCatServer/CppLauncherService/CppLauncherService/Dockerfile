FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CppLauncherService/CppLauncherService/CppLauncherService.csproj", "CppLauncherService/CppLauncherService/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "CppLauncherService/CppLauncherService/CppLauncherService.csproj"
COPY . .
WORKDIR "/src/CppLauncherService/CppLauncherService"
RUN dotnet build "CppLauncherService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CppLauncherService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CppLauncherService.dll"]

FROM final AS gpp-install
RUN apt-get update
RUN apt-get -y install g++
