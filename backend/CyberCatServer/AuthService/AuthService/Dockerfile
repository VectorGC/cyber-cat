FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["AuthService/AuthService/AuthService.csproj", "AuthService/AuthService/"]
COPY ["SharedProjects/Shared.Server/Shared.Server.csproj", "SharedProjects/Shared.Server/"]
COPY ["SharedProjects/Shared.Models/Shared.Models.csproj", "SharedProjects/Shared.Models/"]
COPY ["AuthService/AuthService.JwtValidation/AuthService.JwtValidation.csproj", "AuthService/AuthService.JwtValidation/"]
RUN dotnet restore "AuthService/AuthService/AuthService.csproj"
COPY . .
WORKDIR "/src/AuthService/AuthService"
RUN dotnet build "AuthService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthService.dll"]
