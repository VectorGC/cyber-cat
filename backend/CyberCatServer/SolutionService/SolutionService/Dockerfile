FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

# https://learn.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-7.0&tabs=visual-studio%2Clinux-ubuntu#:~:text=https%20%2D%2Dhelp-,Warning,-Do%20not%20create
ENV DOTNET_GENERATE_ASPNET_CERTIFICATE=false

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS copy
WORKDIR /src
COPY SolutionService SolutionService/
COPY SharedProjects SharedProjects/

FROM copy AS publish
WORKDIR /src/SolutionService/SolutionService
RUN dotnet publish -c Release -o /app/publish -p:UseAppHost=false --verbosity detailed

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SolutionService.dll"]
