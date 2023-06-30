FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SolutionService/SolutionService/SolutionService.csproj", "SolutionService/SolutionService/"]
COPY ["SharedProjects/Shared.Server/Shared.Server.csproj", "SharedProjects/Shared.Server/"]
COPY ["SharedProjects/Shared.Models/Shared.Models.csproj", "SharedProjects/Shared.Models/"]
RUN dotnet restore "SolutionService/SolutionService/SolutionService.csproj"
COPY . .
WORKDIR "/src/SolutionService/SolutionService"
RUN dotnet build "SolutionService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SolutionService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SolutionService.dll"]
