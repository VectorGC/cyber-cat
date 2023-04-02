#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 443
EXPOSE 80

EXPOSE $TASK_SERVICE_API_PORT

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TaskServiceAPI.csproj", "TaskServiceAPI/"]
RUN dotnet restore "TaskServiceAPI/TaskServiceAPI.csproj"

WORKDIR "/src/TaskServiceAPI"
COPY . .

RUN dotnet build "TaskServiceAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskServiceAPI.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskServiceAPI.dll"]