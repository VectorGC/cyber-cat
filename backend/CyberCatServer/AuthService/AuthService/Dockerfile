FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS copy
WORKDIR /src
COPY ["AuthService", "AuthService/"]
COPY ["SharedProjects", "SharedProjects/"]

FROM copy AS publish
WORKDIR "/src/AuthService/AuthService"
RUN dotnet publish "AuthService.csproj" -c Release -o /app/publish /p:UseAppHost=false -v d

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthService.dll"]
