FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish
COPY . .
RUN dotnet publish "/TaskService/TaskService/TaskService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskService.dll"]