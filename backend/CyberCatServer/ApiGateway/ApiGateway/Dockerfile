FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApiGateway/ApiGateway/ApiGateway.csproj", "ApiGateway/ApiGateway/"]
COPY ["AuthService/AuthService.JwtValidation/AuthService.JwtValidation.csproj", "AuthService/AuthService.JwtValidation/"]
COPY ["SharedProjects/Shared.Server/Shared.Server.csproj", "SharedProjects/Shared.Server/"]
COPY ["SharedProjects/Shared.Models/Shared.Models.csproj", "SharedProjects/Shared.Models/"]
RUN dotnet restore "ApiGateway/ApiGateway/ApiGateway.csproj"
COPY . .
WORKDIR "/src/ApiGateway/ApiGateway"
RUN dotnet build "ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.dll"]
