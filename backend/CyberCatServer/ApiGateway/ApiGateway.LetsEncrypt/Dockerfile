FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS copy
WORKDIR /src
COPY . .

FROM copy AS publish
WORKDIR "/src"
RUN dotnet publish "ApiGateway.LetsEncrypt.csproj" -c Release -o /app/publish /p:UseAppHost=false --verbosity detailed

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.LetsEncrypt.dll"]

