FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["JudgeService/JudgeService/JudgeService.csproj", "JudgeService/JudgeService/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "JudgeService/JudgeService/JudgeService.csproj"
COPY . .
WORKDIR "/src/JudgeService/JudgeService"
RUN dotnet build "JudgeService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JudgeService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JudgeService.dll"]
